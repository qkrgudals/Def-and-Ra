using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public int damageAmount = 10; // 데미지 양
    public float damageInterval = 1f; // 데미지 간격
    private float nextDamageTime; // 다음 데미지 시간

    public Transform target; // 목표물의 Transform
    public GameObject targetGameObject;
    public float speed = 1.0f; // 이동 속도
    GameManager manager;
    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }
    void Update() {
        
        if (target != null && manager.reg == "raid2") // 목표물이 설정되어 있을 때
        {
            // 목표물 쪽으로 이동 벡터 계산
            Vector3 direction = target.position - transform.position;
            direction.Normalize(); // 이동 방향을 정규화

            // 목표물 쪽으로 이동
            transform.position += direction * speed * Time.deltaTime;
        }
        if(!targetGameObject.activeSelf)
        {
            Debug.Log("삭제");
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other) {
            //Debug.Log("부딫힘");
        // 충돌한 객체가 적이면
        if (other.CompareTag("Player")) {
            // 다음 데미지 시간이 되었을 때
            if (Time.time >= nextDamageTime) {
                // 데미지를 입힘
                DealDamage(other.gameObject);
                // 다음 데미지 시간 갱신
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
    void DealDamage(GameObject player) {
        // 적의 체력을 감소시킴
        // 예시로 간단히 적의 Health 스크립트의 TakeDamage 함수 호출
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null) {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
