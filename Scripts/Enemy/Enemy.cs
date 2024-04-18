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
   
    private Rigidbody rb;
    private Animator anim;
    public float attackRange;
    public LayerMask layerMask;
    
    private int coin;
    public int exp;
    private bool canDealDamage = true;
  
    EnemyController enemyController;
    EnemyManager manager;
    NavMeshAgent navMeshAgent;
    UnitInfo unitInfo;
    Health health;
    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
       
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetBool("Attacking", false);
      
        anim.SetBool("Walking", true);
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyController = GetComponent<EnemyController>();
        unitInfo = FindObjectOfType<UnitInfo>();
        health = FindObjectOfType<Health>();
    }

    private void Update() {
      

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
          
        }
        else if(IsPlayerInRange()) {
            anim.SetBool("Attacking", true);
            anim.SetBool("Walking", false);
          
        }

        // Update the time since the last attack
        timeSinceLastAttack += Time.deltaTime;
   
        }

    public void Attack() {
        Transform target = enemyController.FindClosestTarget();
     
        if (canDealDamage) {
           
            
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.forward, attackRange,layerMask);
            //Debug.Log(hits.Length);            
            if(hits.Length == 0) {
               
                timeSinceLastAttack = 0;
              
            }
           
       
            for (int i = 0; i < hits.Length; i++) {
 
                RaycastHit hit = hits[i];


                if (hit.collider.gameObject.CompareTag("Player")) {
                 
                    hit.collider.gameObject.GetComponent<Health>().TakeDamage(damage);
                    timeSinceLastAttack = 0;
                    if (hit.collider.gameObject.name.Equals("Soldier_01(Clone)")) {
                        
                      hit.collider.gameObject.GetComponent<Health>().sol();
                    }
                    else if(hit.collider.gameObject.name.Equals("Erika Archer With Bow Arrow(Clone)")) {
                       
                        hit.collider.gameObject.GetComponent<Health>().archer();

                    }
                }
                    if (hit.collider.gameObject.CompareTag("Door")) {
                      
                        hit.collider.gameObject.GetComponent<DoorHealth>().TakeDamage(damage);
                        timeSinceLastAttack = 0;
                    }
                
            }
           
           
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
  
}
