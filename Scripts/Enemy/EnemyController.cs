using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    private NavMeshAgent agent;
    private Transform[] doors;
    private Transform[] players;
    private Enemy enemy;
    public float attackRange = 5.0f;
    public bool isAttackingPlayer;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        doors = GameObject.FindGameObjectsWithTag("Door").Select(d => d.transform).ToArray();
        players = GameObject.FindGameObjectsWithTag("Player").Select(p => p.transform).ToArray();

        enemy = GetComponent<Enemy>();

        // 코루틴 시작
        StartCoroutine(UpdateTargetCoroutine());
    }
    private void Update() {
        players = GameObject.FindGameObjectsWithTag("Player").Select(p => p.transform).ToArray();
    }
    IEnumerator UpdateTargetCoroutine() {
        while (true) {
            // 타겟 갱신
            Transform target = FindClosestTarget();

            // 타겟이 존재할 때
            if (target != null) {
                // 공격 범위 안에 있을 때
                if (Vector3.Distance(transform.position, target.position) <= attackRange) {
                    if (target.CompareTag("Player") || target.CompareTag("Door")) {
                        isAttackingPlayer = true;
                        transform.LookAt(target);
                    }
                    else {
                        isAttackingPlayer = false;
                    }

                    if (isAttackingPlayer) {
                        enemy.Attack();
                    }
                }
                // 공격 범위 밖에 있을 때
                else {
                    isAttackingPlayer = false;
                    agent.SetDestination(target.position);
                }
            }
            // 타겟이 없을 때
            else {
                isAttackingPlayer = false;
                agent.isStopped = true;
            }

            // 다음 프레임까지 대기
            yield return null;
        }
    }

    // 가장 가까운 타겟을 찾는 메서드
    public Transform FindClosestTarget() {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform door in doors) {
            if (door.gameObject.activeSelf) {
                float distance = Vector3.Distance(transform.position, door.position);

                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestTarget = door;
                }
            }
        }

        foreach (Transform player in players) {
            if (player.gameObject.activeSelf) {
                float distance = Vector3.Distance(transform.position, player.position);

                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestTarget = player;
                }
            }
        }

        return closestTarget;
    }
}