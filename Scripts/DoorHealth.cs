using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHealth : MonoBehaviour {
    public float maxHealth = 100;  // �ִ� ü��
    public float currentHealth;
    private GameObject door;
    void Start() {
        currentHealth = maxHealth;  // ���� ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
        door = GameObject.Find("Door");
    }
    public void TakeDamage(float damage) {
        currentHealth -= damage;
        //Debug.Log(damage);
        // ü���� 0 ���Ϸ� �������� ���� ó��
        if (currentHealth <= 0 ) {
           //gameObject.SetActive(false);
           door.SetActive(false);
            Debug.Log("Game Over");
        }
    }
}
