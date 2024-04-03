using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    private NavMeshAgent agent;
    private Transform[] doors;
    private Transform[] players;
    public bool isAttackingPlayer;
    private Enemy enemy;
    public float attackRange = 5.0f;
    //private Animator animator;
    //EnemyData enemyData;
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        doors = GameObject.FindGameObjectsWithTag("Door").Select(d => d.transform).ToArray();
        players = GameObject.FindGameObjectsWithTag("Player").Select(p => p.transform).ToArray();
        enemy = GetComponent<Enemy>();
        //animator = GetComponent<Animator>();
    }

    void Update() {
        Transform target = FindClosestTarget();

        if (target != null) {
            if (Vector3.Distance(transform.position, target.position) > attackRange) {
 
                agent.SetDestination(ResetPosition(target.position));
                isAttackingPlayer = false;
             
            }

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
        
        }
        else {
            agent.isStopped = true;
        }
    }

    Vector3 ResetPosition(Vector3 targetposition) {
        float dist = targetposition.y - transform.position.y;
        //float k = target.position.y - 8f;
        //Debug.Log(dist);
        if (Mathf.Abs(dist) >= 8f) {
            return new Vector3(targetposition.x, transform.position.y+ 6f, targetposition.z);
            
        }
        else {
            return targetposition;
        }
    }

    public Transform FindClosestTarget() {
        Transform closestDoor = null;
        float closestDoorDistance = float.MaxValue;

        foreach (Transform door in doors) {
            if (door.gameObject.activeSelf) {
                float distance = Vector3.Distance(transform.position, door.position);
                if (distance < closestDoorDistance) {
                    closestDoorDistance = distance;
                    closestDoor = door;
                }
            }
        }
        Transform closestPlayer = null;
        float closestPlayerDistance = float.MaxValue;

        foreach (Transform player in players) {
            if (player.gameObject.activeSelf) {
                float distance = Vector3.Distance(transform.position, player.position);
                if (distance < closestPlayerDistance) {
                    closestPlayerDistance = distance;
                    closestPlayer = player;
                }
            }
        }
        // Compare distances and return the overall closest target
        if (closestPlayer != null && closestPlayerDistance < closestDoorDistance && closestPlayer.gameObject.activeSelf) {
            return closestPlayer;
            
        }
        else {
            return closestDoor;
        }
    }
}