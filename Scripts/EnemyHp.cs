using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour {
    public int maxHealth = 100; // �ִ� ü��
    public int currentHealth;    // ���� ü��
    public enum EnemyStatus { Normal, Stunned, Poisoned }; // ���÷� ������ �������ͽ� ����
    public EnemyStatus status;   // ���� �������ͽ�

    // Start �޼ҵ忡�� �ʱ�ȭ�� �����մϴ�.
    void Start() {
        currentHealth = maxHealth; // ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
        status = EnemyStatus.Normal; // ���� �� �⺻ �������ͽ��� Normal�� �ʱ�ȭ
    }

    // �� �޼ҵ�� �������� �����µ� ���˴ϴ�.
    public void TakeDamage(float damageAmount) {
        currentHealth -= Mathf.FloorToInt(damageAmount); // ��������ŭ ü�� ����

        if (currentHealth <= 0) {
            Die(); // ü���� 0 ���Ϸ� �������� Die �޼ҵ� ȣ��
        }
    }
    // �� �޼ҵ�� ���� ���� �� ȣ��˴ϴ�.
    public void Die() {
        // ���⿡�� ���� ���� ���� ������ �߰��մϴ�.
        Debug.Log("Enemy died!");
        // ���� ���, ���� ��Ȱ��ȭ�ϰų� �ı��ϴ� ���� ������ ������ �� �ֽ��ϴ�.
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}