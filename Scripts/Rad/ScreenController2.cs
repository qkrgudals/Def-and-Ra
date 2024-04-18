using UnityEngine;
using System.Collections;

public class ScreenController2 : MonoBehaviour {
    public float zoomSpeed = 70f;
    public float moveSpeed = 20f;
    public float minZoom = 20f;
    public float maxZoom = 60f;
    public float followHeight = 18f; // 카메라가 따라다닐 높이
    public float minHeight = 13f; // 카메라의 최소 높이

    private RaycastHit hit_wall;

    void Update() {
        // 마우스 휠로 카메라 줌인
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scrollWheel);

        // W, A, S, D 키로 화면 이동
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MoveCamera(horizontal, vertical);

        // 카메라와 그라운드가 일정 높이로 이동하고, 카메라가 그라운드 밖으로 나가면 카메라를 고정시킵니다.
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

        // 아래쪽으로 레이를 쏘아 충돌하는 지점을 찾습니다. "Ground" 레이어에만 충돌하도록 설정합니다.
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layerMask)) {
            targetPosition = hit.point + Vector3.up * followHeight;
        }

        // 카메라의 이동 방향을 정의합니다.
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // 카메라의 이동 방향으로 레이를 쏴서 장애물을 감지합니다.
        RaycastHit obstacleHit;
        if (Physics.Raycast(transform.position, moveDirection, out obstacleHit, moveSpeed * Time.deltaTime)) {
            // 만약 장애물과 충돌한다면, 카메라를 고정시킵니다.
            return;
        }
        else {
            // 장애물이 없으면 카메라를 타겟 위치까지 이동시킵니다.
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2);
        }

        // 최소 높이 제한
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