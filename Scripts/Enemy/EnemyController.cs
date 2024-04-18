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

        // �ڷ�ƾ ����
        StartCoroutine(UpdateTargetCoroutine());
    }
    private void Update() {
        players = GameObject.FindGameObjectsWithTag("Player").Select(p => p.transform).ToArray();
    }
    IEnumerator UpdateTargetCoroutine() {
        while (true) {
            // Ÿ�� ����
            Transform target = FindClosestTarget();

            // Ÿ���� ������ ��
            if (target != null) {
                // ���� ���� �ȿ� ���� ��
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
                // ���� ���� �ۿ� ���� ��
                else {
                    isAttackingPlayer = false;
                    agent.SetDestination(target.position);
                }
            }
            // Ÿ���� ���� ��
            else {
                isAttackingPlayer = false;
                agent.isStopped = true;
            }

            // ���� �����ӱ��� ���
            yield return null;
        }
    }

    // ���� ����� Ÿ���� ã�� �޼���
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