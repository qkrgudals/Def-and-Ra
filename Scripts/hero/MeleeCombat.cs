using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(UnitController))]

public class MeleeCombat : MonoBehaviour
{
    private UnitController controller;
    private BasicStats basicStats;
    private Animator anim;

    [Header("Target")]
    public GameObject targetEnemy;

    [Header("Melee Attack Variables")]
    public bool performMeleeAttack = true;
    private float attackInterval;
    private float nextAttackTime = 0;

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

        if (targetEnemy != null && performMeleeAttack && Time.time > nextAttackTime)
        {
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= controller.stoppingDistance)
            {
                StartCoroutine(MeleeAttackInterval());
            }
        }

        if (targetEnemy == null ||!targetEnemy.activeSelf)
        {
            anim.SetBool("isAttacking", false);
        }
    }

    private IEnumerator MeleeAttackInterval()
    {
        performMeleeAttack = false;

        anim.SetBool("isAttacking", true);

        yield return new WaitForSeconds(attackInterval);

        if (targetEnemy == null || !targetEnemy.activeSelf)
        {
            anim.SetBool("isAttacking", false);
            performMeleeAttack = true;
        }
    }


    private void MeleeAttack()
    {
        Enemy targetEnemy = this.targetEnemy.gameObject.GetComponent<Enemy>();
        targetEnemy?.HeroTakeDamage(basicStats.attackdamage, gameObject);

        nextAttackTime = Time.time + attackInterval;
        performMeleeAttack = true;

        anim.SetBool("isAttacking", false);
    }
}
