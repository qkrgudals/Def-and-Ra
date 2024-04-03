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
            if (hit.collider.gameObject == gameObject && Input.GetMouseButtonDown(0)) // 유닛 생성소 클릭시 유닛 생성창 뜸
            {
                Debug.Log("유닛 생성소");
                unitInfo.UnitSponerWindow(true); // 유닛 생성창 활성화
            }
        }
        */
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject == gameObject && Input.GetMouseButtonDown(0)) // 유닛 생성소 클릭시 유닛 생성창 뜸
            {
                Debug.Log("유닛 생성소");
                unitInfo.UnitSponerWindow(true); // 유닛 생성창 활성화
                break; // 한 번의 클릭에 대해서만 처리하도록 중지
            }
        }
    }
}
