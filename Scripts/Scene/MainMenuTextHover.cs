using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Button buttonComponent;
    private Color normalColor;
    private Vector3 normalScale;
    public Color hoverColor = Color.yellow; // 호버 효과 색상
    public float hoverScaleMultiplier = 1.3f; // 호버 효과 시 크기 배수

    void Start() {
        // 버튼 컴포넌트 가져오기
        buttonComponent = GetComponent<Button>();

        if (buttonComponent != null) {
            // 기본 색상 저장
            normalColor = buttonComponent.colors.normalColor;
            // 기본 크기 저장
            normalScale = transform.localScale;
        }
        else {
            Debug.LogError("Button component is not found!");
        }
    }

    // 마우스를 가져다 놓았을 때 호출되는 함수
    public void OnPointerEnter(PointerEventData eventData) {
        // 버튼의 색상을 hoverColor로 변경
        ColorBlock colors = buttonComponent.colors;
        colors.normalColor = hoverColor;
        buttonComponent.colors = colors;
        // 버튼의 크기를 조절하여 호버 효과 적용
        transform.localScale = normalScale * hoverScaleMultiplier;
    }

    // 마우스를 떼었을 때 호출되는 함수
    public void OnPointerExit(PointerEventData eventData) {
        // 버튼의 색상을 원래 색상으로 변경
        ColorBlock colors = buttonComponent.colors;
        colors.normalColor = normalColor;
        buttonComponent.colors = colors;
        // 버튼의 크기를 원래 크기로 되돌림
        transform.localScale = normalScale;
    }
}