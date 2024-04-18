using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownAttackProjectile : MonoBehaviour
{
    public int damage;

    private GameObject caster;
    void Update()
    {
        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    public void SetCaster(GameObject caster)
    {
        this.caster = caster;
    }

    private List<Collider> targetEnemy = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 충돌한 오브젝트를 리스트에 추가
            targetEnemy.Add(other);
        }
    }

    private void LateUpdate()
    {
        // 모든 충돌에 대해 작업을 수행
        foreach (var other in targetEnemy)
        {
            Enemy targetEnemy = other.gameObject.GetComponent<Enemy>();
            targetEnemy?.HeroTakeDamage(damage, caster);
        }

        // 리스트 초기화
        targetEnemy.Clear();
    }
}
