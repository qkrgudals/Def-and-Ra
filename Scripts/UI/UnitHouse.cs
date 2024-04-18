using UnityEngine;
using UnityEngine.UIElements;

public class UnitHouse : MonoBehaviour
{
    UnitInfo unitInfo;
    GameManager gameManager;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        unitInfo = FindObjectOfType<UnitInfo>();
    }
    void Update()
    {  
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject == gameObject && Input.GetMouseButtonDown(0) ) // ���� ������ Ŭ���� ���� ����â ��
            {
                
                unitInfo.UnitSponerWindow(true); // ���� ����â Ȱ��ȭ
                break; // �� ���� Ŭ���� ���ؼ��� ó���ϵ��� ����
            }
        }
    }
}
