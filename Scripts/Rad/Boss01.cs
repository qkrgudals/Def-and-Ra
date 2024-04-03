using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss01 : MonoBehaviour {
    [SerializeField] private GameObject skillPrefab1;
    [SerializeField] private GameObject skillPrefab2;
    [SerializeField] private GameObject skillPrefab3;
    private GameObject clone;
    private GameObject clone2;
    private GameObject clone3;

    public float maxHealth = 100f;
    public float currentHealth;
    public float attack = 10f;
    NavMeshAgent navMeshAgent;

    Enemy enemy;
    EnemyController enemyController;
    bool isAttacking;
    public float pattern1Cooldown = 10f;
    public float pattern2Cooldown = 15f;
    public float pattern3Cooldown = 20f;

    private void Start() {
        enemy = GetComponent<Enemy>();
        enemyController = GetComponent<EnemyController>();
        currentHealth = maxHealth;
        clone = Instantiate(skillPrefab1, transform.position, skillPrefab1.transform.rotation);
        clone2 = Instantiate(skillPrefab2, transform.position, skillPrefab2.transform.rotation);
        clone3 = Instantiate(skillPrefab3, transform.position, Quaternion.identity);
        clone.SetActive(false);
        clone2.SetActive(false);
        clone3.SetActive(false);
    }

    private void Update() {
        if (!isAttacking && enemyController.isAttackingPlayer) {
            //enemy.Attack();
            float healthPercentage = (currentHealth / maxHealth) * 100f;
            if (healthPercentage <= 75f && pattern1Cooldown <= 0f) {
                StartCoroutine(AttackPattern75());
                pattern1Cooldown = 10f; // Reset cooldown
            }
            else if (healthPercentage <= 50f && pattern2Cooldown <= 0f) {
                StartCoroutine(AttackPattern50());
                pattern2Cooldown = 15f; // Reset cooldown
            }
            else if (healthPercentage <= 25f && pattern3Cooldown <= 0f) {
                StartCoroutine(AttackPattern25());
                pattern3Cooldown = 20f; // Reset cooldown
            }
        }

        // Update cooldowns
        pattern1Cooldown -= Time.deltaTime;
        pattern2Cooldown -= Time.deltaTime;
        pattern3Cooldown -= Time.deltaTime;
    }

    IEnumerator AttackPattern75() {
        isAttacking = true;
        clone.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        clone.SetActive(false);
        isAttacking = false;
    }

    IEnumerator AttackPattern50() {
        isAttacking = true;
        clone2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        clone2.SetActive(false);
        isAttacking = false;
    }

    IEnumerator AttackPattern25() {
        isAttacking = true;
        clone3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        clone3.SetActive(false);
        isAttacking = false;
    }

}
