using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoAttack : MonoBehaviour {
    public float detectionRadius = 10f;
    public LayerMask targetLayer;
    public float fireRate = 1f;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float bulletForce = 10f;
    private float nextFireTime = 2f;
    private float rotationSpeed = 5f;
    private Animator animator;

    private bool isAttacking = false;

    private Archer_UnitController unitController;  // UnitController 참조 추가
    Enemy hp;

    void Start() {
        animator = GetComponent<Animator>();
        hp = FindObjectOfType<Enemy>();

        unitController = GetComponent<Archer_UnitController>();  // UnitController 참조 초기화

        // Health 스크립트의 이벤트에 대한 리스너 등록
        Health healthScript = GetComponent<Health>();
        if (healthScript != null) {
            healthScript.OnCharacterDeath += StopAttackingOnDeath;
        }
        /*
        if (hp == null) {
            Debug.LogError("Enemy object not found!");
        }
        */
    }

    void StopAttackingOnDeath() {
        // 캐릭터가 죽으면 공격 중지
        isAttacking = false;
        unitController.StopMoving();  // 캐릭터가 죽으면 이동도 중지
        animator.SetBool("Attack", false);
        StopCoroutine(AttackRoutine());
       

    }

    void Update() {
        DetectAndAttack();
        /*
        if (!hp.gameObject) {
            animator.SetBool("Attack", false);
            animator.SetBool("Run", false);
        }
        */
    }

    void DetectAndAttack() {
        Collider[] targets = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        if (targets.Length > 0) {
            Transform nearestTarget = GetNearestTarget(targets);

            // 몸통 방향 설정
            Vector3 directionToTarget = (nearestTarget.position - firePoint.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // 이동 중이 아니고, 일정 시간 간격으로만 발사
            if (!unitController.IsMoving() && Time.time >= nextFireTime) {
                
                if (!isAttacking) {
                    isAttacking = true;
                    ShootProjectile(directionToTarget);
                    StartCoroutine(AttackRoutine());
                }
                nextFireTime = Time.time + 1.9f / fireRate;
            }
        }
        else {
            isAttacking = false;
        }

        if (isAttacking) {
            Invoke(nameof(ResetAttackFlag), 0.1f);
        }
    }

    Transform GetNearestTarget(Collider[] targets) {
        Transform nearestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (var target in targets) {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                nearestTarget = target.transform;
            }
        }

        return nearestTarget;
    }
    
    void ResetAttackFlag() {
        isAttacking = false;
    }
    
    public void ShootProjectile(Vector3 direction) {
        // projectilePrefab이 null인지 확인 후 인스턴스화
        if (projectilePrefab != null) {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            ProjectileBehavior projectileBehavior = projectile.GetComponent<ProjectileBehavior>();
            if (projectileBehavior != null) {
                projectileBehavior.SetOwner(this);
              
                //projectileBehavior.Fire(direction, bulletForce);
            }

            if (animator != null) {
                animator.SetBool("Attack", true);
                
             

                StartCoroutine(DelayedProjectileFire(projectile, direction, bulletForce));
            }
        }
        else {
            Debug.LogError("projectilePrefab이 null입니다. Unity Inspector에서 올바른 발사체 프리팹을 할당해주세요.");
        }
    }
    IEnumerator AttackRoutine() {
        while (true) {
       
        // Idle 상태로 전환
        animator.SetBool("Attack", false);
          
        yield return new WaitForSeconds(0.5f); // 2.5초 동안 대기
        animator.SetBool("Attack", true);

        // 2.5초 동안 공격 지속
        yield return new WaitForSeconds(1.0f);
            
            if (!isAttacking ) {
                animator.SetBool("Attack", false);
                animator.SetBool("Run", false);
                break;
            }
            
        }
    }

    private IEnumerator DelayedProjectileFire(GameObject projectile, Vector3 direction, float force) {
        // 애니메이션의 길이에 따라 대기
        yield return new WaitForSeconds(1.0f);

        if (projectile == null) {
            yield break;
        }
        // 발사체 발사
        ProjectileBehavior projectileBehavior = projectile.GetComponent<ProjectileBehavior>();
        if (projectileBehavior != null) {
                

            projectileBehavior.Fire(direction, force);
           // Debug.Log("공격 중: 발사체가 생성되었습니다.");
        }

        // 애니메이터의 "Attack" 불리언 매개변수를 false로 설정하여 애니메이션 종료
        if (animator != null) {
            animator.SetBool("Attack", false);
        }
    }

}