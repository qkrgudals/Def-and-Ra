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

    private Archer_UnitController unitController;  // UnitController ���� �߰�
    Enemy hp;

    void Start() {
        animator = GetComponent<Animator>();
        hp = FindObjectOfType<Enemy>();

        unitController = GetComponent<Archer_UnitController>();  // UnitController ���� �ʱ�ȭ

        // Health ��ũ��Ʈ�� �̺�Ʈ�� ���� ������ ���
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
        // ĳ���Ͱ� ������ ���� ����
        isAttacking = false;
        unitController.StopMoving();  // ĳ���Ͱ� ������ �̵��� ����
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

            // ���� ���� ����
            Vector3 directionToTarget = (nearestTarget.position - firePoint.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // �̵� ���� �ƴϰ�, ���� �ð� �������θ� �߻�
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
        // projectilePrefab�� null���� Ȯ�� �� �ν��Ͻ�ȭ
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
            Debug.LogError("projectilePrefab�� null�Դϴ�. Unity Inspector���� �ùٸ� �߻�ü �������� �Ҵ����ּ���.");
        }
    }
    IEnumerator AttackRoutine() {
        while (true) {
       
        // Idle ���·� ��ȯ
        animator.SetBool("Attack", false);
          
        yield return new WaitForSeconds(0.5f); // 2.5�� ���� ���
        animator.SetBool("Attack", true);

        // 2.5�� ���� ���� ����
        yield return new WaitForSeconds(1.0f);
            
            if (!isAttacking ) {
                animator.SetBool("Attack", false);
                animator.SetBool("Run", false);
                break;
            }
            
        }
    }

    private IEnumerator DelayedProjectileFire(GameObject projectile, Vector3 direction, float force) {
        // �ִϸ��̼��� ���̿� ���� ���
        yield return new WaitForSeconds(1.0f);

        if (projectile == null) {
            yield break;
        }
        // �߻�ü �߻�
        ProjectileBehavior projectileBehavior = projectile.GetComponent<ProjectileBehavior>();
        if (projectileBehavior != null) {
                

            projectileBehavior.Fire(direction, force);
           // Debug.Log("���� ��: �߻�ü�� �����Ǿ����ϴ�.");
        }

        // �ִϸ������� "Attack" �Ҹ��� �Ű������� false�� �����Ͽ� �ִϸ��̼� ����
        if (animator != null) {
            animator.SetBool("Attack", false);
        }
    }

}