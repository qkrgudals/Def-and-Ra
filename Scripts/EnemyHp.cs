using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour {
    public int maxHealth = 100; // 최대 체력
    public int currentHealth;    // 현재 체력
    public enum EnemyStatus { Normal, Stunned, Poisoned }; // 예시로 정의한 스테이터스 종류
    public EnemyStatus status;   // 현재 스테이터스

    // Start 메소드에서 초기화를 수행합니다.
    void Start() {
        currentHealth = maxHealth; // 시작 시 현재 체력을 최대 체력으로 초기화
        status = EnemyStatus.Normal; // 시작 시 기본 스테이터스를 Normal로 초기화
    }

    // 이 메소드는 데미지를 입히는데 사용됩니다.
    public void TakeDamage(float damageAmount) {
        currentHealth -= Mathf.FloorToInt(damageAmount); // 데미지만큼 체력 감소

        if (currentHealth <= 0) {
            Die(); // 체력이 0 이하로 떨어지면 Die 메소드 호출
        }
    }
    // 이 메소드는 적이 죽을 때 호출됩니다.
    public void Die() {
        // 여기에는 적이 죽을 때의 동작을 추가합니다.
        Debug.Log("Enemy died!");
        // 예를 들어, 적을 비활성화하거나 파괴하는 등의 동작을 수행할 수 있습니다.
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}