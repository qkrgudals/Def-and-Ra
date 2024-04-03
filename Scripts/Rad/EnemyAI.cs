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

            // ���� ������ �ð������� ��Ÿ����
            ShowAttackRange(isWithinAttackRange);

            // ���� ����
            if (isWithinAttackRange) {
                anim.SetBool("Attacking", true);
                anim.SetBool("Walking", false);
                AttackPlayer();
            }
        }
        else {
            anim.SetBool("Attacking", false);
            anim.SetBool("Walking", false);
            // ���� ���� �� ���� ���� ǥ�� ����
            navMeshAgent.ResetPath();
            HideAttackRange();
        }
    }

    void ShowAttackRange(bool isWithinAttackRange) {
        if (attackRangeIndicator != null) {
            attackRangeIndicator.SetActive(true);

            // ��ä���� ����� ũ�� ����
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer <= attackAngle * 0.5f && directionToPlayer.magnitude <= attackRange && isWithinAttackRange) {
                // ��ä�� �ȿ� ������ �� �޽ø� ȸ�� �� ũ�� �����Ͽ� ��Ÿ����
                attackRangeIndicator.transform.rotation = Quaternion.LookRotation(directionToPlayer.normalized, Vector3.up);
                attackRangeIndicator.transform.localScale = new Vector3(attackRange, 1f, attackRange);
            }
            else {
                // ��ä�� �ۿ� ���� �� �����
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
        // ��ä�� �޽ø� �����Ͽ� ��Ÿ�� GameObject ����
        attackRangeIndicator = new GameObject("AttackRangeIndicator");
        attackRangeIndicator.transform.parent = transform;
        attackRangeIndicator.transform.localPosition = Vector3.zero;

        // MeshFilter �� MeshRenderer �߰�
        meshFilter = attackRangeIndicator.AddComponent<MeshFilter>();
        meshRenderer = attackRangeIndicator.AddComponent<MeshRenderer>();

        // ��ä�� �޽� ���� �� ����
        Mesh mesh = (Mesh)CreateWedgeMesh();
        meshFilter.sharedMesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material.color = new Color(1f, 0f, 0f, 0.2f);

        // �ʱ⿡�� �����
        attackRangeIndicator.SetActive(false);
    }

    Mesh CreateWedgeMesh() {
     
        // ��ä�� �޽� ����
        Mesh mesh = new Mesh();
        int segments = 20; // ��ä���� ���׸�Ʈ ��
        float angleIncrement = attackAngle / segments;
        float currentAngle = -attackAngle * 0.5f;

        // ������ �迭 ����
        Vector3[] vertices = new Vector3[(segments + 1) * 2];
        vertices[0] = Vector3.zero;

        // ��ä���� ������ ���
        for (int i = 1; i <= segments * 2; i += 2) {
           
            float x = Mathf.Cos(Mathf.Deg2Rad * currentAngle) * attackRange;
            float z = Mathf.Sin(Mathf.Deg2Rad * currentAngle) * attackRange;
            vertices[i] = new Vector3(x, 0f, z);
            vertices[i + 1] = Vector3.zero; // �߽���
            currentAngle = Mathf.Clamp(currentAngle, -attackAngle * 0.5f, attackAngle * 0.5f);
            currentAngle += angleIncrement;
        }

        // �ﰢ�� �迭 ����
        int[] triangles = new int[segments * 3];
        int vertexIndex = 1;
        for (int i = 0; i < triangles.Length; i += 3) {
           
            triangles[i] = 0;
            triangles[i + 1] = vertexIndex;
            triangles[i + 2] = (vertexIndex + 1 > segments * 2) ? 1 : vertexIndex + 1;

            vertexIndex += 2;
        }

        // Mesh�� �������� �ﰢ�� ����
        mesh.vertices = vertices;
        mesh.triangles = triangles;
       //Debug.Log(mesh.ToString());
        return mesh;
    }
}
