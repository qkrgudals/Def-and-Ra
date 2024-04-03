using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTranslucentEvent : MonoBehaviour // ���콺 Ŀ���� Ư�� ������Ʈ�� �ű�� ������ȭ

    // ������: �ش� ������Ʈ�� �ݶ��̴�, �Ž� ������, ���͸��� ������Ʈ�� �־�� �ϰ� Transparency �±׸� ������ �־�� ��
{
    private Material[] originalMaterials;
    private bool isMouseOver = false;

    void Start()
    {
        // ��� Material�� ��������
        originalMaterials = GetComponent<Renderer>().materials;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject == gameObject)
            { 
                if (hit.collider.gameObject.CompareTag("Transparency"))
                {
                    if (!isMouseOver)
                    {
                        // ���콺�� ������Ʈ ���� ���� ��
                        isMouseOver = true;

                        // ��� Material�� ���� �ݺ�
                        foreach (Material mat in originalMaterials)
                        {
                            SetTransparent(mat);
                        }
                    }
                }
            }
            else
            {
                if (isMouseOver)
                {
                    // ���콺�� ������Ʈ���� ��� ��
                    isMouseOver = false;

                    // ��� Material�� ���� �ݺ�
                    foreach (Material mat in originalMaterials)
                    {
                        SetOpaque(mat);
                    }
                }
            }
        }
    }

    void SetTransparent(Material material)
    {
        material.SetFloat("_Mode", 3); // Transparent rendering mode
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;

        Color newColor = material.color;
        newColor.a = 0.3f; // ���� (0: ���� ����, 1: ���� ������)
        material.color = newColor;
    }

    void SetOpaque(Material material)
    {
        material.SetFloat("_Mode", 0); // Opaque rendering mode
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;

        Color newColor = material.color;
        newColor.a = 1.0f; // ������ ������� ����
        material.color = newColor;
    }
}
