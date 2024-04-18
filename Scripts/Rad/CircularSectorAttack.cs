using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CircularSectorAttack : MonoBehaviour
{
    public float attackRange; // Adjust the attack range as needed
    public int damageAmount = 10; // Amount of damage dealt to the player
    public float damageRate = 1.0f; // Rate at which damage is dealt (damage per second)
    public string playerTag = "Player"; // Tag to identify the player GameObject

    private GameObject player;
    private NavMeshAgent navMeshAgent;
    private CircularSectorMeshRenderer circularSectorMeshGenerator;
    private Animator anim;
    private bool isAttacking = false;
    MeshRenderer meshRenderer;
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        circularSectorMeshGenerator = GetComponent<CircularSectorMeshRenderer>();
        anim = GetComponent<Animator>();
        FindActivePlayer();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update() {
        if (player == null) {
            FindActivePlayer();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if the player is within the attack range
        if (distanceToPlayer <= attackRange ) {

            // Attack logic here
            if (!isAttacking) {
                meshRenderer.enabled = true;
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack01")
                && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
                    StartCoroutine(DealDamageOverTime());
                    anim.Play("attack01", 0, 0f);
                    meshRenderer.enabled = true;
                  
                    Debug.Log("Attacking player!");
                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack01")
                && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.0f
                && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) {
                    meshRenderer.enabled = false;
                }
            }
             
           
            Debug.Log("Player in attack range!");
            anim.SetBool("Attacking", true);
            anim.SetBool("Walking", false);

            // Example: Rotate the Circular Sector Mesh towards the player
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(directionToPlayer);

            // Update the Circular Sector Mesh Generator's properties
            //circularSectorMeshGenerator.degree = 180f; // Adjust the degree as needed
            circularSectorMeshGenerator.radius = attackRange;

            // Example: Stop the NavMeshAgent when attacking
            navMeshAgent.isStopped = true;
        }
        else {
            // Player is out of attack range, resume normal behavior

            // Example: Continue moving towards the player using NavMeshAgent
            navMeshAgent.SetDestination(player.transform.position);
            anim.SetBool("Attacking", false);
            anim.SetBool("Walking", true);

            meshRenderer.enabled = false;

            // Example: Reset Circular Sector Mesh Generator's properties
            /*
            circularSectorMeshGenerator.degree = 180f;
            circularSectorMeshGenerator.radius = 10f;
            */

            // Example: Resume NavMeshAgent movement
            navMeshAgent.isStopped = false;
        }
    }

    IEnumerator DealDamageOverTime() {
        isAttacking = true;
        
        // Deal damage to the player
        DealDamage();

        // Wait for damage rate interval
        yield return new WaitForSeconds(1 / damageRate);
        //break;
        isAttacking = false;
       
    }

    void DealDamage() {
        // Implement your logic to deal damage to the player here
        if (player.CompareTag(playerTag)) {
            Debug.Log("Dealing damage to the player: " + damageAmount);
            // Example: Damage the player's health
            player.GetComponent<Health>().TakeDamage(damageAmount);
        }
    }

    void FindActivePlayer() {
        // Find all GameObjects with the specified tag
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);

        // Loop through all found GameObjects and check if they are active
        foreach (GameObject playerObject in players) {
            if (playerObject.activeInHierarchy) {
                player = playerObject;
                Debug.Log("Active player found!");
                //break;
            }
        }
    }
    void OnDrawGizmosSelected() {
        // Draw the attack range gizmo for visualization in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
