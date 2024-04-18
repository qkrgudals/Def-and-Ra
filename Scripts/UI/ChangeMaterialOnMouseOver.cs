using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeMaterialOnMouseOver : MonoBehaviour
{
    public string targetTag = "wall"; // 태그가 "wall"인 오브젝트를 대상으로 합니다.
    public Material highlightMaterial; // 마우스를 올렸을 때 적용할 재질

    private Material originalMaterial; // 원래의 오브젝트 재질
    private Renderer rend;
    private bool isMouseOver = false;
    void Start() {
        rend = GetComponent<Renderer>(); // Renderer 컴포넌트 가져오기
        originalMaterial = rend.material; // 원래의 재질 저장
    }
    void Update() {
        if (isMouseOver) {
            // 마우스 위치에서 레이 쏘기
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            bool hitTarget = false; // 타겟에 부딪힌 여부를 나타내는 변수

            // 레이가 부딪힌 모든 오브젝트에 대해 처리
            foreach (RaycastHit hit in hits) {
                GameObject hitObject = hit.collider.gameObject;

                // 부딪힌 오브젝트가 타겟 태그를 가진 경우에만 처리
                if (hitObject.CompareTag(targetTag)) {
                    hitObject.GetComponent<Renderer>().material = highlightMaterial;  // 재질 변경
                   hitTarget = true;
                    break; // 하이라이트된 오브젝트를 찾았으므로 더 이상의 검사는 중지
                }
            }

            // 마우스가 오브젝트 위에 없거나 타겟에 부딪히지 않은 경우 원래의 재질로 복원
            if (!hitTarget) {
                rend.material = originalMaterial;
                isMouseOver = false; // 마우스가 오브젝트 위에 없음을 표시
            }
        }
    }

    void OnMouseEnter() {
        isMouseOver = true; // 마우스가 오브젝트 위에 있음을 표시
    }

    void OnMouseExit() {
        isMouseOver = false; // 마우스가 오브젝트 위에 없음을 표시
        rend.material = originalMaterial; // 원래의 재질로 복원
    }
}
