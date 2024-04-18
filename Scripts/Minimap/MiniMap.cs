using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public RawImage miniMapImage;
    public Camera miniMapCamera;

    void Start()
    {
        if (miniMapCamera.targetTexture != null)
            miniMapCamera.targetTexture.Release();

        RenderTexture rt = new RenderTexture(256, 256, 24);
        miniMapCamera.targetTexture = rt;
        miniMapImage.texture = rt;
    }
}
