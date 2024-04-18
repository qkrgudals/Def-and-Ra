using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fogout : MonoBehaviour
{
    public FogOff fogOff;
    public FogOff fogOff1;
    public FogOff fogOff2;
    public FogOff fogOff3;
    public FogOff fogOff4;
    public FogOff fogOff5;

    GameObject raid1Parent;
    GameObject raid3Parent;

    private void Awake() {
        raid1Parent = GameObject.Find("raid1");
        raid3Parent = GameObject.Find("raid3");

    }
    private void Update() {
        
        //GameObject raid2Parent = GameObject.Find("raid1");
        bool has2_JobMom = false; // 2_잡몹을 포함하는 객체가 있는지 여부를 저장하는 변수
        bool has2_JobMom1 = false; // 2_정예을 포함하는 객체가 있는지 여부를 저장하는 변수
        bool has2_JobMom2 = false; // 2_보스을 포함하는 객체가 있는지 여부를 저장하는 변수
        bool has2_JobMom3 = false; // 2_잡몹을 포함하는 객체가 있는지 여부를 저장하는 변수
        bool has2_JobMom4 = false; // 2_정예을 포함하는 객체가 있는지 여부를 저장하는 변수
        bool has2_JobMom5 = false; // 2_보스을 포함하는 객체가 있는지 여부를 저장하는 변수
        GameObject[] allObjects = raid1Parent.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();
        GameObject[] allObjects1 = raid3Parent.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();

        // 모든 게임 오브젝트를 순회하면서 2_잡몹을 포함하는 객체가 있는지 확인
        foreach (GameObject obj in allObjects) {
            if (obj.name.Contains("2_잡몹"))
            {
                has2_JobMom = true;
                //break;
            }
            if (obj.name.Contains("2_정예"))
            {
                has2_JobMom1 = true;
                //break;

            }
            if (obj.name.Contains("2_보스"))
            {
                has2_JobMom2 = true;
                //break;

            }

            
            if (has2_JobMom && has2_JobMom1 && has2_JobMom2)
            {
                break;
            }
          
        }
        foreach (GameObject obj in allObjects1) {
            if (obj.name.Contains("4_잡몹")) {
                has2_JobMom3 = true;
                //break;
            }
            if (obj.name.Contains("4_정예")) {
                has2_JobMom4 = true;
                //break;

            }
            if (obj.name.Contains("4_보스")) {
                has2_JobMom5 = true;
                //break;

            }


            if (has2_JobMom3 && has2_JobMom4 && has2_JobMom5) {
                break;
            }

        }
        if (!has2_JobMom)
        {
            fogOff.StartFadeOut();
           
        }
        if (!has2_JobMom1) {
            fogOff1.StartFadeOut();
        }
        if(!has2_JobMom2) {
            fogOff2.StartFadeOut();
        }
        if (!has2_JobMom3) {
            fogOff3.StartFadeOut();

        }
        if (!has2_JobMom4) {
            fogOff4.StartFadeOut();
        }
        if (!has2_JobMom5) {
            fogOff5.StartFadeOut();
        }
    }
    
    
}
