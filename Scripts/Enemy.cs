using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

public class Enemy : MonoBehaviour {
    public enum EnemyType { Normal, Elite, Boss }

    public EnemyType enemyType;
    GameManager gameManager;
    public float moveSpeed;
    public int damage;
    public int hp;
    public float attackDelay;
    public float timeSinceLastAttack;
    //private Transform[] player;
    private Rigidbody rb;
    private Animator anim;
    private float attackRange;
    public LayerMask layerMask;
    //public LayerMask layerMask2;
    private int coin;
    public int exp;
    private bool canDealDamage = true;
    //public float damageCooldown = 1.0f;
    EnemyController enemyController;
    EnemyManager manager;
    NavMeshAgent navMeshAgent;
    UnitInfo unitInfo;
    Health health;
    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        //player = GameObject.FindGameObjectsWithTag("Player").;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetBool("Attacking", false);
        //timeSinceLastAttack = attackDelay;
        anim.SetBool("Walking", true);
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyController = GetComponent<EnemyController>();
        unitInfo = FindObjectOfType<UnitInfo>();
        health = FindObjectOfType<Health>();
    }

    private void Update() {
        //Transform target = enemyController.FindClosestTarget();
        // Move towards the player
        //Vector3 direction = (player.position - transform.position).normalized;
        //rb.velocity = direction * moveSpeed;

        /*
        // Attack the player if the attack delay has passed and the player is in range
        if (timeSinceLastAttack >= attackDelay && IsPlayerInRange()) {
            //anim.SetBool("Walking", false);
            //navMeshAgent.isStopped = true;
            Attack();
            //Debug.Log("//");
        }
        */

        if (timeSinceLastAttack >= attackDelay) {
            canDealDamage = true;
            
        }
      
        if (timeSinceLastAttack < attackDelay ) {
            anim.SetBool("Attacking", false);
            canDealDamage = false;
           

        }
        
        if (!IsPlayerInRange()) {
            anim.SetBool("Attacking", false);
            anim.SetBool("Walking", true);
            //Debug.Log("거리x");
            //navMeshAgent.isStopped = false;
        }
        else if(IsPlayerInRange()) {
            anim.SetBool("Attacking", true);
            anim.SetBool("Walking", false);
            //Debug.Log("거리o");
            //navMeshAgent.isStopped = true;
        }

        // Update the time since the last attack
        timeSinceLastAttack += Time.deltaTime;
   
        }
    /*
    private void FindNewTarget() {
        GameObject[] activePlayers = GameObject.FindGameObjectsWithTag("Player");

        // Find the nearest active player
        float closestDistance = float.MaxValue;
        foreach (GameObject activePlayer in activePlayers) {
            float distance = Vector3.Distance(transform.position, activePlayer.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                player = activePlayer.transform;
            }
        }
    }
    */
    public void Attack() {
        Transform target = enemyController.FindClosestTarget();
       // Debug.Log(canDealDamage);
        if (canDealDamage) {
            anim.SetBool("Attacking", true);
            anim.SetBool("Walking", false);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.forward, attackRange,layerMask);
            //Debug.Log(hits.Length);
            
            if(hits.Length == 0) {
               
                timeSinceLastAttack = 0;
                //navMeshAgent.SetDestination(target.transform.position);
               // navMeshAgent.isStopped = true;
            }
           
            /*
            if(hits.Length >= 1) {
                navMeshAgent.SetDestination(target.transform.position);
                navMeshAgent.isStopped = false;
            }
           */
            for (int i = 0; i < hits.Length; i++) {
 
                RaycastHit hit = hits[i];

                /*
                Debug.Log("Hit Object: " + hit.collider.gameObject.name);
                Debug.Log("Tag: " + hit.collider.gameObject.tag);
                Debug.Log("Layer: " + hit.collider.gameObject.layer);
                */
                if (hit.collider.gameObject.CompareTag("Player")) {
                    //Debug.Log("//");
                    hit.collider.gameObject.GetComponent<Health>().TakeDamage(damage);
                    timeSinceLastAttack = 0;
                    if (hit.collider.gameObject.name.Equals("Soldier_01(Clone)")) {
                        
                      hit.collider.gameObject.GetComponent<Health>().sol();
                    }
                    else if(hit.collider.gameObject.name.Equals("Erika Archer With Bow Arrow(Clone)")) {
                        //Debug.Log("///");
                        hit.collider.gameObject.GetComponent<Health>().archer();

                    }
                }
                    if (hit.collider.gameObject.CompareTag("Door")) {
                        //Debug.Log("//");
                        hit.collider.gameObject.GetComponent<DoorHealth>().TakeDamage(damage);
                        timeSinceLastAttack = 0;
                    }
                
            }
            //StartCoroutine(DamageCooldown());
           
        }
    }

    public void Initialize(EnemyData enemyData) {
        moveSpeed = enemyData.moveSpeed;
        damage = enemyData.damage;
        hp = enemyData.hp;
        attackDelay = enemyData.attackDelay;
        attackRange = enemyData.attackRange;
        coin = enemyData.Coin;
        exp = enemyData.EXP;
    }

    public void TakeDamage(int damage) {
        hp -= damage;
        if (hp <= 0) {
            //Destroy(gameObject);    
            hp = 0;
            gameObject.SetActive(false);
            gameManager.EnemyKilled(coin);
        }
    }

    public void HeroTakeDamage(int damage, GameObject caster)
    {
        hp -= damage;
        if (hp <= 0)
        {
            //Destroy(gameObject);    
            hp = 0;
            if (caster != null)
            {
                caster.GetComponent<BasicStats>().TakeExp(exp); // 경험치 추가
            }
            gameObject.SetActive(false);
            gameManager.EnemyKilled(coin);
        }
    }
    private bool IsPlayerInRange() {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, attackRange);
        for (int i = 0; i < hits.Length; i++) {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Door")) {
                return true;
            }
        }
        return false;
    }
    /*
    private IEnumerator DamageCooldown() {
        canDealDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDealDamage = true;
    }
    */
}
