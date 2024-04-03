using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOff : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    private Material particleMaterial;
    private bool fadingOut = false;

    public float fadeSpeed = 0.5f;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleMaterial = particleSystem.GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (fadingOut)
        {
            // ���� ���� ������ ���ҽ��� ��ƼŬ�� ������ ������� �մϴ�.
            float newAlpha = Mathf.Lerp(particleMaterial.color.a, 0f, fadeSpeed * Time.deltaTime);
            particleMaterial.color = new Color(particleMaterial.color.r, particleMaterial.color.g, particleMaterial.color.b, newAlpha);

            // ���� ���� 0�� ��������� �� ��ƼŬ �ý����� �����ϰ� �����մϴ�.
            if (newAlpha <= 0.01f)
            {
                particleSystem.Stop();
                Destroy(gameObject, 0.5f); // ��ƼŬ�� ������ ����� �� �� �� �Ŀ� ���� ������Ʈ�� �����մϴ�.
            }
        }
    }

    // ��ƼŬ �ý����� ������ ������� �����մϴ�.
    public void StartFadeOut()
    {
        fadingOut = true;
    }
}
