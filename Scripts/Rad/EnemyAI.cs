using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    public float detectionRange = 10f;
    public float attackRange = 5f;
    public float attackAngle = 45f;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private GameObject attackRangeIndicator;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Animator anim;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        CreateAttackRangeIndicator();
    }

    void Update() {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange) {
            navMeshAgent.SetDestination(player.position);
            anim.SetBool("Attacking", false);
            anim.SetBool("Walking", true);
            bool isWithinAttackRange = IsPlayerWithinAttackRange();

            // 공격 범위를 시각적으로 나타내기
            ShowAttackRange(isWithinAttackRange);

            // 공격 실행
            if (isWithinAttackRange) {
                anim.SetBool("Attacking", true);
                anim.SetBool("Walking", false);
                AttackPlayer();
            }
        }
        else {
            anim.SetBool("Attacking", false);
            anim.SetBool("Walking", false);
            // 추적 중지 및 공격 범위 표시 중지
            navMeshAgent.ResetPath();
            HideAttackRange();
        }
    }

    void ShowAttackRange(bool isWithinAttackRange) {
        if (attackRangeIndicator != null) {
            attackRangeIndicator.SetActive(true);

            // 부채꼴의 방향과 크기 설정
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer <= attackAngle * 0.5f && directionToPlayer.magnitude <= attackRange && isWithinAttackRange) {
                // 부채꼴 안에 들어왔을 때 메시를 회전 및 크기 조절하여 나타내기
                attackRangeIndicator.transform.rotation = Quaternion.LookRotation(directionToPlayer.normalized, Vector3.up);
                attackRangeIndicator.transform.localScale = new Vector3(attackRange, 1f, attackRange);
            }
            else {
                // 부채꼴 밖에 있을 때 숨기기
                attackRangeIndicator.SetActive(false);
            }
        }
    }

    void HideAttackRange() {
        if (attackRangeIndicator != null) {
            attackRangeIndicator.SetActive(false);
        }
    }

    void AttackPlayer() {
        Debug.Log("Enemy is attacking the player!");
    }

    bool IsPlayerWithinAttackRange() {
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        return angleToPlayer <= attackAngle * 0.5f && directionToPlayer.magnitude <= attackRange;
    }

    void CreateAttackRangeIndicator() {
        // 부채꼴 메시를 생성하여 나타낼 GameObject 생성
        attackRangeIndicator = new GameObject("AttackRangeIndicator");
        attackRangeIndicator.transform.parent = transform;
        attackRangeIndicator.transform.localPosition = Vector3.zero;

        // MeshFilter 및 MeshRenderer 추가
        meshFilter = attackRangeIndicator.AddComponent<MeshFilter>();
        meshRenderer = attackRangeIndicator.AddComponent<MeshRenderer>();

        // 부채꼴 메시 생성 및 설정
        Mesh mesh = (Mesh)CreateWedgeMesh();
        meshFilter.sharedMesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material.color = new Color(1f, 0f, 0f, 0.2f);

        // 초기에는 숨기기
        attackRangeIndicator.SetActive(false);
    }

    Mesh CreateWedgeMesh() {
     
        // 부채꼴 메시 생성
        Mesh mesh = new Mesh();
        int segments = 20; // 부채꼴의 세그먼트 수
        float angleIncrement = attackAngle / segments;
        float currentAngle = -attackAngle * 0.5f;

        // 꼭짓점 배열 생성
        Vector3[] vertices = new Vector3[(segments + 1) * 2];
        vertices[0] = Vector3.zero;

        // 부채꼴의 꼭짓점 계산
        for (int i = 1; i <= segments * 2; i += 2) {
           
            float x = Mathf.Cos(Mathf.Deg2Rad * currentAngle) * attackRange;
            float z = Mathf.Sin(Mathf.Deg2Rad * currentAngle) * attackRange;
            vertices[i] = new Vector3(x, 0f, z);
            vertices[i + 1] = Vector3.zero; // 중심점
            currentAngle = Mathf.Clamp(currentAngle, -attackAngle * 0.5f, attackAngle * 0.5f);
            currentAngle += angleIncrement;
        }

        // 삼각형 배열 생성
        int[] triangles = new int[segments * 3];
        int vertexIndex = 1;
        for (int i = 0; i < triangles.Length; i += 3) {
           
            triangles[i] = 0;
            triangles[i + 1] = vertexIndex;
            triangles[i + 2] = (vertexIndex + 1 > segments * 2) ? 1 : vertexIndex + 1;

            vertexIndex += 2;
        }

        // Mesh에 꼭짓점과 삼각형 설정
        mesh.vertices = vertices;
        mesh.triangles = triangles;
       //Debug.Log(mesh.ToString());
        return mesh;
    }
}
