using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTranslucentEvent : MonoBehaviour // 마우스 커서를 특정 오브젝트에 옮기면 반투명화

    // 적용방법: 해당 오브젝트에 콜라이더, 매쉬 랜더러, 머터리얼 컴포넌트가 있어야 하고 Transparency 태그를 가지고 있어야 함
{
    private Material[] originalMaterials;
    private bool isMouseOver = false;

    void Start()
    {
        // 모든 Material을 가져오기
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
                        // 마우스가 오브젝트 위에 있을 때
                        isMouseOver = true;

                        // 모든 Material에 대해 반복
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
                    // 마우스가 오브젝트에서 벗어날 때
                    isMouseOver = false;

                    // 모든 Material에 대해 반복
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
        newColor.a = 0.3f; // 투명도 (0: 완전 투명, 1: 완전 불투명)
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
        newColor.a = 1.0f; // 투명도를 원래대로 복원
        material.color = newColor;
    }
}
