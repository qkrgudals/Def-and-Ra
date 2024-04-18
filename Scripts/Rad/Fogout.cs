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
        bool has2_JobMom = false; // 2_����� �����ϴ� ��ü�� �ִ��� ���θ� �����ϴ� ����
        bool has2_JobMom1 = false; // 2_������ �����ϴ� ��ü�� �ִ��� ���θ� �����ϴ� ����
        bool has2_JobMom2 = false; // 2_������ �����ϴ� ��ü�� �ִ��� ���θ� �����ϴ� ����
        bool has2_JobMom3 = false; // 2_����� �����ϴ� ��ü�� �ִ��� ���θ� �����ϴ� ����
        bool has2_JobMom4 = false; // 2_������ �����ϴ� ��ü�� �ִ��� ���θ� �����ϴ� ����
        bool has2_JobMom5 = false; // 2_������ �����ϴ� ��ü�� �ִ��� ���θ� �����ϴ� ����
        GameObject[] allObjects = raid1Parent.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();
        GameObject[] allObjects1 = raid3Parent.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToArray();

        // ��� ���� ������Ʈ�� ��ȸ�ϸ鼭 2_����� �����ϴ� ��ü�� �ִ��� Ȯ��
        foreach (GameObject obj in allObjects) {
            if (obj.name.Contains("2_���"))
            {
                has2_JobMom = true;
                //break;
            }
            if (obj.name.Contains("2_����"))
            {
                has2_JobMom1 = true;
                //break;

            }
            if (obj.name.Contains("2_����"))
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
            if (obj.name.Contains("4_���")) {
                has2_JobMom3 = true;
                //break;
            }
            if (obj.name.Contains("4_����")) {
                has2_JobMom4 = true;
                //break;

            }
            if (obj.name.Contains("4_����")) {
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
