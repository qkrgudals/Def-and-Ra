using TMPro;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Soldier_UnitController : MonoBehaviour
{
    [SerializeField]
    private GameObject unitMarker;

    [SerializeField]
    private GameObject unitHPBar;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Health healthScript;
    RTSUnitController rtsCt;
    private bool isDead = false;

    private float distanceToEnemy;
    public float attackRange = 3f;
    public int damageAmount = 20;
    public bool startAttack = false;
    public bool startWalking = false;
    public bool hasAttacked = false;

    public string soldierCount = "SoldierMarker"; // 솔져유닛 마커 오브젝트의 "SoldierMarker"태그를 찾아 유닛을 카운트
    public GameObject[] objectsWithTag; // 특정 오브젝트의 태그를 찾음 ("SoldierMarker")
    UnitInfo unitInfo;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rtsCt = FindObjectOfType<RTSUnitController>();
        healthScript = GetComponent<Health>();
        unitInfo = FindObjectOfType<UnitInfo>();
    }

    void Update() {
        // Check if the unit is moving and adjust animations accordingly
        if (navMeshAgent.velocity.magnitude >= 0.01f) {
            animator.SetBool("SwordAttack", false);
            animator.SetBool("Walking", true);
        }
        else {
            animator.SetBool("SwordAttack", false);
            animator.SetBool("Walking", false);
        }

        // Check for enemies in the attack range and initiate attack if conditions are met
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider collider in colliders) {
            if (collider.CompareTag("Enemy")) {
                distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);

                if (distanceToEnemy <= attackRange) {
                    if (!hasAttacked) {
                        animator.SetBool("SwordAttack", true);
                        animator.SetBool("Walking", false);
                        //navMeshAgent.isStopped = true;

                        // Assuming that the enemy script has a TakeDamage method
                        Enemy enemyScript = collider.GetComponent<Enemy>();
                        if (enemyScript != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Sword Attack")
                            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
                            transform.LookAt(enemyScript.transform.position);
                            enemyScript.TakeDamage(damageAmount);
                            animator.Play("Sword Attack", 0, 0f);
                            hasAttacked = true;
                           
                        }
                    }
                }
                else {
                    navMeshAgent.speed = 3.5f;
                    animator.SetBool("SwordAttack", false);
                    hasAttacked = false;
                }
            }
            else {
                //navMeshAgent.isStopped = false;
                hasAttacked = false;
            }
        }

        // Additional conditions for movement and animations
        if (!isDead && !healthScript.IsDying() && navMeshAgent.velocity.magnitude >= 0.5f) {
            animator.SetBool("Run", true);
        }
        else {
            //animator.SetBool("Run", false);
        }
    }

    // Methods for unit selection, deselection, and movement
    public void SelectUnit() {
        unitMarker.SetActive(true);
        unitHPBar.SetActive(true);
        animator.SetBool("Preparation", true);
        objectsWithTag = GameObject.FindGameObjectsWithTag(soldierCount);
        unitInfo.ShowSelectionInfo_Soldier(objectsWithTag.Length);
    }

    public void DeselectUnit() {
        unitMarker.SetActive(false);
        unitHPBar.SetActive(false);
        animator.SetBool("SwordAttack", false);
        animator.SetBool("Preparation", false);
        objectsWithTag = GameObject.FindGameObjectsWithTag(soldierCount);
        unitInfo.ShowSelectionInfo_Soldier(objectsWithTag.Length);
    }

    public void MoveTo(Vector3 end) {
        end = end + Random.insideUnitSphere * (rtsCt.selectedUnitList.Count) * 0.6f;
        navMeshAgent.SetDestination(end);
        animator.SetBool("SwordAttack", false);
        animator.SetBool("Walking", true);
        
    }

    // Method to stop the unit from moving
    public void StopMoving() {
        navMeshAgent.isStopped = true;
    }

    // Method to check if the unit is currently moving
    public bool IsMoving() {
        return navMeshAgent.velocity.magnitude >= 0.5f;
    }


    // Method to reset the attack flag

    public void ResetAttackFlag() {
        hasAttacked = false;
    }

}

