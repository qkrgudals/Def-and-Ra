using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    private Transform target;
    private Transform originalTarget;
    private Rigidbody rb;

    private BasicStats basicStats;
    private GameObject caster;

    public float projectileSpeed;

    void Start()
    {
        originalTarget = target;
        basicStats = GameObject.FindGameObjectWithTag("Unit").GetComponent<BasicStats>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position) + new Vector3(0, 1, 0);
            rb.velocity = direction.normalized * projectileSpeed;
        }
        else if (originalTarget != null)
        {
            Vector3 direction = (originalTarget.position - transform.position) + new Vector3(0, 1, 0);
            rb.velocity = direction.normalized * projectileSpeed;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetCaster(GameObject caster)
    {
        this.caster = caster;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target != null && ReferenceEquals(other.gameObject, target.gameObject))
        {
            Enemy targetStats = target.gameObject.GetComponent<Enemy>();
            targetStats?.HeroTakeDamage(basicStats.attackdamage, caster);
            Destroy(gameObject);
        }
        else if (originalTarget != null && ReferenceEquals(other.gameObject, originalTarget.gameObject))
        {
            Enemy originalTargetEnemy = originalTarget.gameObject.GetComponent<Enemy>();
            originalTargetEnemy?.HeroTakeDamage(basicStats.attackdamage, caster);
            Destroy(gameObject);
        }
    }
}
