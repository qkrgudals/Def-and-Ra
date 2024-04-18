using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour {
    public float zoomSpeed = 70f; 
    public float moveSpeed = 20f;
    public float minZoom = 20f;
    public float maxZoom = 60f;

    void Update()
    {
        // 마우스 휠로 카메라 줌인
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scrollWheel);

        // W, A, S, D 키로 화면 이동
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MoveCamera(horizontal, vertical);
    }

    public void ZoomCamera(float zoomAmount) // 카메라 줌 기능
    {
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - zoomAmount * zoomSpeed, minZoom, maxZoom);
        // Mathf.Clamp: 제한범위를 설정하는 수학 함수 (제한하려는 값, 허용가능한 최솟값, 허용가능한 최댓값), Camera.main.fieldOfView: 새로 계산된 값
    }

    void MoveCamera(float horizontal, float vertical) // 카메라 이동 
    {
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);

        // 입력 방향을 카메라의 회전을 기준으로 월드 스페이스로 변환 (카메라의 로테이션 y값이 0이 아닌 경우 필요)
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);

        // 카메라의 회전에 수직인 방향은 무시 (카메라의 로테이션 x값이 0이 아닌경우 필요)
        moveDirection.y = 0;

        // 벡터의 크기가 1 이상이면 노멀라이즈를 적용
        if (moveDirection.magnitude > 1f) {
            moveDirection.Normalize();
        }
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
