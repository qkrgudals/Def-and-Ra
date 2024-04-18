using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeMaterialOnMouseOver : MonoBehaviour
{
    public string targetTag = "wall"; // �±װ� "wall"�� ������Ʈ�� ������� �մϴ�.
    public Material highlightMaterial; // ���콺�� �÷��� �� ������ ����

    private Material originalMaterial; // ������ ������Ʈ ����
    private Renderer rend;
    private bool isMouseOver = false;
    void Start() {
        rend = GetComponent<Renderer>(); // Renderer ������Ʈ ��������
        originalMaterial = rend.material; // ������ ���� ����
    }
    void Update() {
        if (isMouseOver) {
            // ���콺 ��ġ���� ���� ���
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            bool hitTarget = false; // Ÿ�ٿ� �ε��� ���θ� ��Ÿ���� ����

            // ���̰� �ε��� ��� ������Ʈ�� ���� ó��
            foreach (RaycastHit hit in hits) {
                GameObject hitObject = hit.collider.gameObject;

                // �ε��� ������Ʈ�� Ÿ�� �±׸� ���� ��쿡�� ó��
                if (hitObject.CompareTag(targetTag)) {
                    hitObject.GetComponent<Renderer>().material = highlightMaterial;  // ���� ����
                   hitTarget = true;
                    break; // ���̶���Ʈ�� ������Ʈ�� ã�����Ƿ� �� �̻��� �˻�� ����
                }
            }

            // ���콺�� ������Ʈ ���� ���ų� Ÿ�ٿ� �ε����� ���� ��� ������ ������ ����
            if (!hitTarget) {
                rend.material = originalMaterial;
                isMouseOver = false; // ���콺�� ������Ʈ ���� ������ ǥ��
            }
        }
    }

    void OnMouseEnter() {
        isMouseOver = true; // ���콺�� ������Ʈ ���� ������ ǥ��
    }

    void OnMouseExit() {
        isMouseOver = false; // ���콺�� ������Ʈ ���� ������ ǥ��
        rend.material = originalMaterial; // ������ ������ ����
    }
}
