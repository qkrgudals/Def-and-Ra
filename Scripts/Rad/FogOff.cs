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
            // 알파 값을 서서히 감소시켜 파티클이 서서히 사라지게 합니다.
            float newAlpha = Mathf.Lerp(particleMaterial.color.a, 0f, fadeSpeed * Time.deltaTime);
            particleMaterial.color = new Color(particleMaterial.color.r, particleMaterial.color.g, particleMaterial.color.b, newAlpha);

            // 알파 값이 0에 가까워졌을 때 파티클 시스템을 중지하고 삭제합니다.
            if (newAlpha <= 0.01f)
            {
                particleSystem.Stop();
                Destroy(gameObject, 0.5f); // 파티클이 완전히 사라진 후 몇 초 후에 게임 오브젝트를 삭제합니다.
            }
        }
    }

    // 파티클 시스템을 서서히 사라지게 시작합니다.
    public void StartFadeOut()
    {
        fadingOut = true;
    }
}
