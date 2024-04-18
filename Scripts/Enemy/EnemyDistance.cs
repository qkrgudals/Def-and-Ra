using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDistance : MonoBehaviour {
    public string playerTag = "Player"; // 플레이어의 위치를 추적하기 위한 태그
    public float stoppingDistance = 5f; // NavMeshAgent를 멈출 거리

    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    private Transform[] players;
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트를 가져옴
        players = GameObject.FindGameObjectsWithTag("Player").Select(p => p.transform).ToArray();
    }

    void Update() {
        // 플레이어를 찾음
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