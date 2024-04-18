using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {
    public float lifetime = 4f; // �߻�ü�� ���� (��)
    public int damage = 10; // �߻�ü�� ������
    public AutoAttack attachedAutoAttack;
    private Rigidbody rb;
    void Start() {
        //Rigidbody rb = GetComponent<Rigidbody>();
        // �߻�ü�� ������ �� ���� �ð��� ������ �ڵ����� �ı�
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other) {
        // Null üũ �߰�
        if (other == null || other.gameObject == null) {
            return;
        }

        // �߻�ü�� �浹���� ���� ������ �����մϴ�.
        if (other.gameObject.CompareTag("Enemy")) {
            // Ÿ�ٿ� ����� ���� ������ �߰��� �� �ֽ��ϴ�.
            Debug.Log("�߻�ü�� Ÿ�ٿ� ��ҽ��ϴ�.");

            // Ÿ�ٿ� �������� ������ �ı�
            DealDamage(other.gameObject);
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Ground")
            && other.gameObject.CompareTag("Door")) {
            Destroy(gameObject);
        }
        //�߻�ü �ı�
        //
        /*
        if (attachedAutoAttack != null) {
            rb.AddForce(transform.forward*50f, ForceMode.Impulse);
        }
        */
    }

    internal void SetOwner(AutoAttack autoAttack) {
        // �߻�ü�� ������ ����
        attachedAutoAttack = autoAttack;
    }

    // �߻�ü�� Ư�� ����� ������ �߻��ϴ� �޼���
    public void Fire(Vector3 direction, float force) {
        // Rigidbody�� ���� ���� ���ϰ�, ������ ����
         rb = GetComponent<Rigidbody>();
        rb.rotation = Quaternion.LookRotation(direction);
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    // Ÿ�ٿ� �������� ������ �޼���
    void DealDamage(GameObject target) {
        // Ÿ���� EnemyHp ��ũ��Ʈ�� �ٸ� ������ ������� �������� ó���� �� �ִ� ������Ʈ�� ������ �ִٰ� ����
        Enemy targetHealth = target.GetComponent<Enemy>();

        if (targetHealth != null) {
            // Ÿ�ٿ� �������� ������
            targetHealth.TakeDamage(damage);
        }
        else {
            // ���� EnemyHp ������Ʈ�� ���ٸ� �ٸ� ������� �������� ó���ϴ� �ڵ带 �߰�
            // ���⼭�� Debug.Log�� ��� �޽����� ���
            Debug.LogWarning("EnemyHp ������Ʈ�� �߰ߵ��� �ʾҽ��ϴ�. �ٸ� ������� �������� ó���ϼ���.");
        }
    }
}
