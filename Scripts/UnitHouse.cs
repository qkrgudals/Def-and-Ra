using UnityEngine;
using UnityEngine.UIElements;

public class UnitHouse : MonoBehaviour
{
    UnitInfo unitInfo;

    void Awake()
    {
        unitInfo = FindObjectOfType<UnitInfo>();
    }
    void Update()
    {  
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject == gameObject && Input.GetMouseButtonDown(0)) // ���� ������ Ŭ���� ���� ����â ��
            {
                Debug.Log("���� ������");
                unitInfo.UnitSponerWindow(true); // ���� ����â Ȱ��ȭ
            }
        }
        */
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject == gameObject && Input.GetMouseButtonDown(0)) // ���� ������ Ŭ���� ���� ����â ��
            {
                Debug.Log("���� ������");
                unitInfo.UnitSponerWindow(true); // ���� ����â Ȱ��ȭ
                break; // �� ���� Ŭ���� ���ؼ��� ó���ϵ��� ����
            }
        }
    }
}
