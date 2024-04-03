using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour {
    [SerializeField]
    private GameObject unitMarker;
    public NavMeshAgent navMeshAgent;

    public float rotateSpeedMovement = 0.05f;
    private float rotateVelocity;
    UnitInfo unitInfo;

    [Header("Enemy Targeting")]
    public GameObject targetEnemy;
    public float stoppingDistance;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        unitInfo = FindAnyObjectByType<UnitInfo>();
    }

    private void Update() {
        if (targetEnemy != null) {
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) > stoppingDistance) {
                navMeshAgent.SetDestination(targetEnemy.transform.position);
            }
        }
    }

    public void SelectUnit() {
        unitMarker.SetActive(true);

        SwordStats swordStats = FindObjectOfType<SwordStats>();
        SorceressStats sorceressStats = FindObjectOfType<SorceressStats>();
        PriestStats priestStats = FindObjectOfType<PriestStats>();

        if (swordStats != null) {
            unitInfo.SwordMasterWindow(true, swordStats.nowLv, swordStats.exp, swordStats.maxExp, swordStats.currentHealth, swordStats.maxHealth, swordStats.mp, swordStats.maxMp, swordStats.attackdamage, swordStats.speed);
        }
        else if (sorceressStats != null) {
            unitInfo.SorceressWindow(true, sorceressStats.nowLv, sorceressStats.exp, sorceressStats.maxExp, sorceressStats.currentHealth, sorceressStats.maxHealth, sorceressStats.mp, sorceressStats.maxMp, sorceressStats.attackdamage, sorceressStats.speed);
        }
        else if (priestStats != null) {
            unitInfo.PriestWindow(true, priestStats.nowLv, priestStats.exp, priestStats.maxExp, priestStats.currentHealth, priestStats.maxHealth, priestStats.mp, priestStats.maxMp, priestStats.attackdamage, priestStats.speed);
        }
    }

    public void DeselectUnit() {
        unitMarker.SetActive(false);

    }

    public void MoveTo(Vector3 position) {
        navMeshAgent.SetDestination(position);
        navMeshAgent.stoppingDistance = 0;

        Rotation(position);

        if (targetEnemy != null) {
            targetEnemy = null;
        }
    }

    public void MoveTowardsEnemy(GameObject enemy) {
        targetEnemy = enemy;
        navMeshAgent.SetDestination(targetEnemy.transform.position);
        navMeshAgent.stoppingDistance = stoppingDistance;

        Rotation(targetEnemy.transform.position);
        Debug.Log("적따라감");
    }

    public void Rotation(Vector3 LockAtPosition) {
        Quaternion rotationToLookAt = Quaternion.LookRotation(LockAtPosition - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
            ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));

        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }
}

