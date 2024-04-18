using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDistance : MonoBehaviour {
    public string playerTag = "Player"; // �÷��̾��� ��ġ�� �����ϱ� ���� �±�
    public float stoppingDistance = 5f; // NavMeshAgent�� ���� �Ÿ�

    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    private Transform[] players;
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>(); // NavMeshAgent ������Ʈ�� ������
        players = GameObject.FindGameObjectsWithTag("Player").Select(p => p.transform).ToArray();
    }

    void Update() {
        // �÷��̾ ã��
        players = GameObject.FindGameObjectsWithTag("Player").Select(p => p.transform).ToArray();
        foreach (Transform player in players)
        {
            if (player.gameObject.activeSelf)
            {
                float distance = Vector3.Distance(transform.position, player.position);

                if (distance < stoppingDistance)
                {
                    navMeshAgent.isStopped = true;
                }
                else
                {
                    if (navMeshAgent != null)
                    {
                        navMeshAgent.isStopped = false;

                    }
                }
            }
        }
  
    }
}