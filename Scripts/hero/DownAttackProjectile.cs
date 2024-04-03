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
            // �浹�� ������Ʈ�� ����Ʈ�� �߰�
            targetEnemy.Add(other);
        }
    }

    private void LateUpdate()
    {
        // ��� �浹�� ���� �۾��� ����
        foreach (var other in targetEnemy)
        {
            Enemy targetEnemy = other.gameObject.GetComponent<Enemy>();
            targetEnemy?.HeroTakeDamage(damage, caster);
        }

        // ����Ʈ �ʱ�ȭ
        targetEnemy.Clear();
    }
}
