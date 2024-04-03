using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {
    public float lifetime = 4f; // 발사체의 수명 (초)
    public int damage = 10; // 발사체의 데미지
    public AutoAttack attachedAutoAttack;
    private Rigidbody rb;
    void Start() {
        //Rigidbody rb = GetComponent<Rigidbody>();
        // 발사체가 생성된 후 일정 시간이 지나면 자동으로 파괴
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other) {
        // Null 체크 추가
        if (other == null || other.gameObject == null) {
            return;
        }

        // 발사체가 충돌했을 때의 동작을 정의합니다.
        if (other.gameObject.CompareTag("Enemy")) {
            // 타겟에 닿았을 때의 동작을 추가할 수 있습니다.
            Debug.Log("발사체가 타겟에 닿았습니다.");

            // 타겟에 데미지를 입히고 파괴
            DealDamage(other.gameObject);
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Ground")
            && other.gameObject.CompareTag("Door")) {
            Destroy(gameObject);
        }
        //발사체 파괴
        //
        /*
        if (attachedAutoAttack != null) {
            rb.AddForce(transform.forward*50f, ForceMode.Impulse);
        }
        */
    }

    internal void SetOwner(AutoAttack autoAttack) {
        // 발사체의 소유자 설정
        attachedAutoAttack = autoAttack;
    }

    // 발사체를 특정 방향과 힘으로 발사하는 메서드
    public void Fire(Vector3 direction, float force) {
        // Rigidbody를 통해 힘을 가하고, 방향을 설정
         rb = GetComponent<Rigidbody>();
        rb.rotation = Quaternion.LookRotation(direction);
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    // 타겟에 데미지를 입히는 메서드
    void DealDamage(GameObject target) {
        // 타겟이 EnemyHp 스크립트나 다른 유사한 방식으로 데미지를 처리할 수 있는 컴포넌트를 가지고 있다고 가정
        Enemy targetHealth = target.GetComponent<Enemy>();

        if (targetHealth != null) {
            // 타겟에 데미지를 입히기
            targetHealth.TakeDamage(damage);
        }
        else {
            // 만약 EnemyHp 컴포넌트가 없다면 다른 방식으로 데미지를 처리하는 코드를 추가
            // 여기서는 Debug.Log로 경고 메시지만 출력
            Debug.LogWarning("EnemyHp 컴포넌트가 발견되지 않았습니다. 다른 방식으로 데미지를 처리하세요.");
        }
    }
}
