using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHealth : MonoBehaviour {
    public float maxHealth = 100;  // 최대 체력
    public float currentHealth;
    private GameObject door;
    void Start() {
        currentHealth = maxHealth;  // 게임 시작 시 현재 체력을 최대 체력으로 초기화
        door = GameObject.Find("Door");
    }
    public void TakeDamage(float damage) {
        currentHealth -= damage;
        //Debug.Log(damage);
        // 체력이 0 이하로 떨어졌을 때의 처리
        if (currentHealth <= 0 ) {
           //gameObject.SetActive(false);
           door.SetActive(false);
            Debug.Log("Game Over");
        }
    }
}
