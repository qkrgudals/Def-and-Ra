using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public FogOff fogOff;
    private void OnTriggerEnter(Collider other)
    {
        fogOff.StartFadeOut();
    }
}
