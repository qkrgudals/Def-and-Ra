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
            // �浹�� ������Ʈ�� ����Ʈ�� �߰�
            targetUnit.Add(other);
        }
    }

    private void LateUpdate()
    {
        // ��� �浹�� ���� �۾��� ����
        foreach (var other in targetUnit)
        {
            Health targetStats = other.gameObject.GetComponent<Health>();
            targetStats?.TakeDamage(-heal);
        }

        // ����Ʈ �ʱ�ȭ
        targetUnit.Clear();
    }
}
