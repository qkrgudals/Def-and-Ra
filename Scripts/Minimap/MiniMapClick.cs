using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapClick : MonoBehaviour
{
    public Camera miniMapCamera;
    public Camera mainCamera;
    public LayerMask groundLayer;

    // ���� ī�޶��� ���� ��ġ ��
    public Vector3 fixedWorldOffset = new Vector3(0.0f, 13.0f, -8.0f);

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RectTransform rt = GetComponent<RectTransform>();
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, null, out localPoint);

            // Ŭ���� ��ġ�� ��ǥ�� �̴ϸ��� �ؽ��� ��ǥ�� ����
            Vector2 normalizedPosition = new Vector2(
                Mathf.InverseLerp(0, rt.rect.width, localPoint.x),
                Mathf.InverseLerp(0, rt.rect.height, localPoint.y)
            );

            // �̴ϸ� ��ǥ�� ���� ��ǥ�� ��ȯ
            Ray ray = miniMapCamera.ViewportPointToRay(normalizedPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                // ���� ī�޶� ������ ���� ��ǥ�� �̵�
                MoveMainCamera(hit.point + fixedWorldOffset);
            }
        }
    }

    void MoveMainCamera(Vector3 targetPosition)
    {
        // ���� ī�޶� ������ ���� ��ǥ�� �̵�
        mainCamera.transform.position = targetPosition;
    }
}
