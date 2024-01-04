using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerImage : MonoBehaviour {
    [SerializeField]
    private Image pointerImage; // UI 이미지를 해당 변수에 연결

    private void Update() {
        // 마우스 위치를 기준으로 UI 이미지의 위치를 업데이트
        UpdatePointerPosition();
    }

    private void UpdatePointerPosition() {
        if (pointerImage != null) {
            // 마우스의 스크린 좌표를 가져옴
            Vector3 mousePosition = Input.mousePosition;

            // UI 좌표로 변환
            RectTransform canvasRect = pointerImage.canvas.GetComponent<RectTransform>();
            Vector2 canvasSize = canvasRect.sizeDelta;
            Vector2 normalizedMousePosition = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);
            Vector2 uiPosition = new Vector2(normalizedMousePosition.x * canvasSize.x, normalizedMousePosition.y * canvasSize.y);

            // UI 이미지의 위치를 업데이트
            pointerImage.rectTransform.anchoredPosition = uiPosition;
        }
    }
}
