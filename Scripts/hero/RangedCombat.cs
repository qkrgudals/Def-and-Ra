using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitController))]

public class RangedCombat : MonoBehaviour
{
    private UnitController controller;
    private BasicStats basicStats;
    private Animator anim;

    [Header("Target")]
    public GameObject targetEnemy;

    [Header("Ranged Attack Variables")]
    public bool performRangedAttack = true;
    private float attackInterval;
    private float nextAttackTime = 0;

    [Header("Ranged Projectile Variables")]
    public GameObject attackProjectile;
    public Transform attackSpawnPoint;
    private GameObject spawnedProjectile;

    void Start()
    {
        controller = GetComponent<UnitController>();
        basicStats = GetComponent<BasicStats>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        attackInterval = basicStats.attackSpeed / ((500 + basicStats.attackSpeed) * 0.01f);

        targetEnemy = controller.targetEnemy;

        if (targetEnemy != null && performRangedAttack && Time.time > nextAttackTime)
        {
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= controller.stoppingDistance)
            {
                StartCoroutine(RangedAttackInterval());
            }
        }

        if (targetEnemy == null || !targetEnemy.activeSelf)
        {
            anim.SetBool("isAttacking", false);
        }
    }

    private IEnumerator RangedAttackInterval()
    {
        performRangedAttack = false;

        anim.SetBool("isAttacking", true);

        yield return new WaitForSeconds(attackInterval);

        performRangedAttack = true;
        if (targetEnemy == null || !targetEnemy.activeSelf)
        {
            anim.SetBool("isAttacking", false);
        }
    }


    private void RangedAttack()
    {
        spawnedProjectile = Instantiate(attackProjectile, attackSpawnPoint.transform.position, attackSpawnPoint.transform.rotation);

        TargetEnemy targetEnemyScripts = spawnedProjectile.GetComponent<TargetEnemy>();

        if (targetEnemyScripts != null)
        {
            targetEnemyScripts.SetCaster(gameObject);
            targetEnemyScripts.SetTarget(targetEnemy.transform);
        }

        nextAttackTime = Time.time + attackInterval;
        performRangedAttack = true;

        anim.SetBool("isAttacking", false);
    }
}
