using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public int damageAmount = 10; // ������ ��
    public float damageInterval = 1f; // ������ ����
    private float nextDamageTime; // ���� ������ �ð�

    public Transform target; // ��ǥ���� Transform
    public GameObject targetGameObject;
    public float speed = 1.0f; // �̵� �ӵ�
    GameManager manager;
    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }
    void Update() {
        
        if (target != null && manager.reg == "raid2") // ��ǥ���� �����Ǿ� ���� ��
        {
            // ��ǥ�� ������ �̵� ���� ���
            Vector3 direction = target.position - transform.position;
            direction.Normalize(); // �̵� ������ ����ȭ

            // ��ǥ�� ������ �̵�
            transform.position += direction * speed * Time.deltaTime;
        }
        if(!targetGameObject.activeSelf)
        {
            Debug.Log("����");
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other) {
            //Debug.Log("�΋H��");
        // �浹�� ��ü�� ���̸�
        if (other.CompareTag("Player")) {
            // ���� ������ �ð��� �Ǿ��� ��
            if (Time.time >= nextDamageTime) {
                // �������� ����
                DealDamage(other.gameObject);
                // ���� ������ �ð� ����
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
    void DealDamage(GameObject player) {
        // ���� ü���� ���ҽ�Ŵ
        // ���÷� ������ ���� Health ��ũ��Ʈ�� TakeDamage �Լ� ȣ��
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null) {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
