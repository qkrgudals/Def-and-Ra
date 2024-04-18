using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapClick : MonoBehaviour
{
    public Camera miniMapCamera;
    public Camera mainCamera;
    public LayerMask groundLayer;

    // 메인 카메라의 고정 위치 값
    public Vector3 fixedWorldOffset = new Vector3(0.0f, 13.0f, -8.0f);

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RectTransform rt = GetComponent<RectTransform>();
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, null, out localPoint);

            // 클릭한 위치의 좌표를 미니맵의 텍스쳐 좌표로 얻음
            Vector2 normalizedPosition = new Vector2(
                Mathf.InverseLerp(0, rt.rect.width, localPoint.x),
                Mathf.InverseLerp(0, rt.rect.height, localPoint.y)
            );

            // 미니맵 좌표를 월드 좌표로 변환
            Ray ray = miniMapCamera.ViewportPointToRay(normalizedPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                // 메인 카메라를 고정된 월드 좌표로 이동
                MoveMainCamera(hit.point + fixedWorldOffset);
            }
        }
    }

    void MoveMainCamera(Vector3 targetPosition)
    {
        // 메인 카메라를 고정된 월드 좌표로 이동
        mainCamera.transform.position = targetPosition;
    }
}
