using UnityEngine;
using System.Collections;

public class ScreenController2 : MonoBehaviour {
    public float zoomSpeed = 70f;
    public float moveSpeed = 20f;
    public float minZoom = 20f;
    public float maxZoom = 60f;
    public float followHeight = 18f; // ī�޶� ����ٴ� ����
    public float minHeight = 13f; // ī�޶��� �ּ� ����

    private RaycastHit hit_wall;

    void Update() {
        // ���콺 �ٷ� ī�޶� ����
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scrollWheel);

        // W, A, S, D Ű�� ȭ�� �̵�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MoveCamera(horizontal, vertical);

        // ī�޶�� �׶��尡 ���� ���̷� �̵��ϰ�, ī�޶� �׶��� ������ ������ ī�޶� ������ŵ�ϴ�.
        FollowGroundAndFixCamera();
    }

    public void ZoomCamera(float zoomAmount) {
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - zoomAmount * zoomSpeed, minZoom, maxZoom);
    }

    void MoveCamera(float horizontal, float vertical) {
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;

        if (moveDirection.magnitude > 1f) {
            moveDirection.Normalize();
        }
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    void FollowGroundAndFixCamera() {
        RaycastHit hit;
        Vector3 targetPosition = transform.position;

        // �Ʒ������� ���̸� ��� �浹�ϴ� ������ ã���ϴ�. "Ground" ���̾�� �浹�ϵ��� �����մϴ�.
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layerMask)) {
            targetPosition = hit.point + Vector3.up * followHeight;
        }

        // ī�޶��� �̵� ������ �����մϴ�.
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // ī�޶��� �̵� �������� ���̸� ���� ��ֹ��� �����մϴ�.
        RaycastHit obstacleHit;
        if (Physics.Raycast(transform.position, moveDirection, out obstacleHit, moveSpeed * Time.deltaTime)) {
            // ���� ��ֹ��� �浹�Ѵٸ�, ī�޶� ������ŵ�ϴ�.
            return;
        }
        else {
            // ��ֹ��� ������ ī�޶� Ÿ�� ��ġ���� �̵���ŵ�ϴ�.
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2);
        }

        // �ּ� ���� ����
        transform.position = new Vector3(transform.position.x, Mathf.Max(transform.position.y, minHeight), transform.position.z);
    }

    /*
    public void TF_Wall_Ray() {
        if (Physics.Raycast(transform.position, Vector3.forward, out hit_wall, 50f)) {
            if (hit_wall.transform.tag == "Wall") {
                MeshRenderer temp_mesh = hit_wall.transform.GetComponent<MeshRenderer>();
                Material[] temp_mesh_materials = temp_mesh.materials;
                for (int i = 0; i < temp_mesh_materials.Length; i++) {
                    Debug.Log("materials changeing...");
                    temp_mesh_materials[i].color = new Color(1, 1, 1, 0.3f);
                }
            }
        }
        else {
            MeshRenderer temp_mesh = hit_wall.transform.GetComponent<MeshRenderer>();
            Material[] temp_mesh_materials = temp_mesh.materials;
            for (int i = 0; i < temp_mesh_materials.Length; i++) {
                Debug.Log("materials changeing...");
                temp_mesh_materials[i].color = new Color(1, 1, 1, 1f);
            }
        }
    }
    */
}