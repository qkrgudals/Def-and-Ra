using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealProjectile : MonoBehaviour
{
    public int heal = 0;

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

    private List<Collider> targetUnit = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            // 충돌한 오브젝트를 리스트에 추가
            targetUnit.Add(other);
        }
    }

    private void LateUpdate()
    {
        // 모든 충돌에 대해 작업을 수행
        foreach (var other in targetUnit)
        {
            Health targetStats = other.gameObject.GetComponent<Health>();
            targetStats?.TakeDamage(-heal);
        }

        // 리스트 초기화
        targetUnit.Clear();
    }
}
