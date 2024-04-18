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
        // ���콺 �ٷ� ī�޶� ����
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scrollWheel);

        // W, A, S, D Ű�� ȭ�� �̵�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MoveCamera(horizontal, vertical);
    }

    public void ZoomCamera(float zoomAmount) // ī�޶� �� ���
    {
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - zoomAmount * zoomSpeed, minZoom, maxZoom);
        // Mathf.Clamp: ���ѹ����� �����ϴ� ���� �Լ� (�����Ϸ��� ��, ��밡���� �ּڰ�, ��밡���� �ִ�), Camera.main.fieldOfView: ���� ���� ��
    }

    void MoveCamera(float horizontal, float vertical) // ī�޶� �̵� 
    {
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);

        // �Է� ������ ī�޶��� ȸ���� �������� ���� �����̽��� ��ȯ (ī�޶��� �����̼� y���� 0�� �ƴ� ��� �ʿ�)
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);

        // ī�޶��� ȸ���� ������ ������ ���� (ī�޶��� �����̼� x���� 0�� �ƴѰ�� �ʿ�)
        moveDirection.y = 0;

        // ������ ũ�Ⱑ 1 �̻��̸� ��ֶ���� ����
        if (moveDirection.magnitude > 1f) {
            moveDirection.Normalize();
        }
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
