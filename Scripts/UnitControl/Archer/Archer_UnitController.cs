using TMPro;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Archer_UnitController : MonoBehaviour {
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

    public string archerCount = "ArcherMarker"; // 솔져유닛 마커 오브젝트의 "SoldierMarker"태그를 찾아 유닛을 카운트
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
       
        // Additional conditions for movement and animations
        if (!isDead && !healthScript.IsDying() && navMeshAgent.velocity.magnitude >= 0.5f) {
            animator.SetBool("Run", true);
        }
        else {
            
            animator.SetBool("Run", false);
        }
    }

    // Methods for unit selection, deselection, and movement
    public void SelectUnit() {
        unitMarker.SetActive(true);
        unitHPBar.SetActive(true);
       //animator.SetBool("Preparation", true);
        objectsWithTag = GameObject.FindGameObjectsWithTag(archerCount);
        unitInfo.ShowSelectionInfo_Archer(objectsWithTag.Length);
    }

    public void DeselectUnit() {
        unitMarker.SetActive(false);
        unitHPBar.SetActive(false);
        //animator.SetBool("SwordAttack", false);
        //animator.SetBool("Preparation", false);

        objectsWithTag = GameObject.FindGameObjectsWithTag(archerCount);
        unitInfo.ShowSelectionInfo_Archer(objectsWithTag.Length);
    }

    public void MoveTo(Vector3 end) {
        end = end + Random.insideUnitSphere * (rtsCt.selectedUnitList.Count) * 0.6f;
        navMeshAgent.SetDestination(end);
    
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

