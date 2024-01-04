using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierSoldierController : MonoBehaviour {
    private bool isOutlined = false;
    private Material originalMaterial;
    public Material outlineMaterial; // 파란색 아웃라인용 재질

    void Start() {
        originalMaterial = GetComponent<Renderer>().material;
    }

    void Update() {
        // 마우스 왼쪽 버튼 클릭 시
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 레이캐스트로 오브젝트를 감지
            if (Physics.Raycast(ray, out hit)) {
                GameObject hitObject = hit.collider.gameObject;

                // 클릭된 오브젝트에 대한 아웃라인 토글
                if (hitObject == gameObject) {
                    ToggleOutline();
                }
            }
        }
    }

    void ToggleOutline() {
        if (isOutlined) {
            // 아웃라인 비활성화
            GetComponent<Renderer>().material = originalMaterial;
        }
        else {
            // 아웃라인 활성화
            GetComponent<Renderer>().material = outlineMaterial;
        }

        isOutlined = !isOutlined;
    }
}