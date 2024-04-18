using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Button buttonComponent;
    private Color normalColor;
    private Vector3 normalScale;
    public Color hoverColor = Color.yellow; // ȣ�� ȿ�� ����
    public float hoverScaleMultiplier = 1.3f; // ȣ�� ȿ�� �� ũ�� ���

    void Start() {
        // ��ư ������Ʈ ��������
        buttonComponent = GetComponent<Button>();

        if (buttonComponent != null) {
            // �⺻ ���� ����
            normalColor = buttonComponent.colors.normalColor;
            // �⺻ ũ�� ����
            normalScale = transform.localScale;
        }
        else {
            Debug.LogError("Button component is not found!");
        }
    }

    // ���콺�� ������ ������ �� ȣ��Ǵ� �Լ�
    public void OnPointerEnter(PointerEventData eventData) {
        // ��ư�� ������ hoverColor�� ����
        ColorBlock colors = buttonComponent.colors;
        colors.normalColor = hoverColor;
        buttonComponent.colors = colors;
        // ��ư�� ũ�⸦ �����Ͽ� ȣ�� ȿ�� ����
        transform.localScale = normalScale * hoverScaleMultiplier;
    }

    // ���콺�� ������ �� ȣ��Ǵ� �Լ�
    public void OnPointerExit(PointerEventData eventData) {
        // ��ư�� ������ ���� �������� ����
        ColorBlock colors = buttonComponent.colors;
        colors.normalColor = normalColor;
        buttonComponent.colors = colors;
        // ��ư�� ũ�⸦ ���� ũ��� �ǵ���
        transform.localScale = normalScale;
    }
}