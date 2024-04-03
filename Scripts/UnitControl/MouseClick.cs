using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseClick : MonoBehaviour {
    [SerializeField]
    private LayerMask layerUnit;

    [SerializeField]
    private LayerMask layerEnemy_Alien;

    [SerializeField]
    private LayerMask layerGround;

    private Camera mainCamera;
    private RTSUnitController rtsUnitController;
    private RTSUnitController2 rtsUnitController2;

    private UnitInfo unitInfo;
    private EnemyInfo enemyInfo;

    float detectionRadius = 5f; // 적 감지 범위

    VisualElement soldier_Img; // 검사 이미지
    VisualElement archer_Img;

    bool skillon = false;
    public GameObject targetPointerPrefab;
    private void Awake() {
        mainCamera = Camera.main;
        rtsUnitController = GetComponent<RTSUnitController>();
        rtsUnitController2 = GetComponent<RTSUnitController2>();
        unitInfo = FindObjectOfType<UnitInfo>();
        enemyInfo = FindObjectOfType<EnemyInfo>();
    }

    private void Update() {
        // 마우스 왼쪽 클릭으로 유닛 선택 or 해제
        if (!skillon && Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // 광선에 부딪히는 오브젝트가 있을 때 (=유닛을 클릭했을 때)
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerUnit)) {
                if (hit.transform.GetComponent<Soldier_UnitController>() != null) {
                    if (Input.GetKey(KeyCode.LeftShift)) {
                        rtsUnitController.ShiftClickSelectUnit(hit.transform.GetComponent<Soldier_UnitController>());
                    }
                    else {
                        rtsUnitController.ClickSelectUnit(hit.transform.GetComponent<Soldier_UnitController>());
                    }
                }

                if (hit.transform.GetComponent<Archer_UnitController>() != null) {
                    if (Input.GetKey(KeyCode.LeftShift)) {
                        rtsUnitController.ShiftClickSelectUnit(hit.transform.GetComponent<Archer_UnitController>());
                    }
                    else {
                        rtsUnitController.ClickSelectUnit(hit.transform.GetComponent<Archer_UnitController>());
                    }
                }
                if (hit.transform.GetComponent<UnitController>() == null) return;
                if (hit.transform.GetComponent<UnitController>() != null) {
                    if (Input.GetKey(KeyCode.LeftShift)) {
                        rtsUnitController2.ShiftClickSelectUnit(hit.transform.GetComponent<UnitController>());
                    }
                    else {
                        rtsUnitController2.ClickSelectUnit(hit.transform.GetComponent<UnitController>());
                    }
                }
            }
            // 광선에 부딪히는 오브젝트가 없을 때 (=유닛의 클릭을 해제할 때)
            else {
                //Debug.Log(hit.transform);
                if (!Input.GetKey(KeyCode.LeftShift)) // 왼쪽 시프트 키를 누르지 않고 유닛을 클릭하지 않을 때
                {
                    Invoke("DeselectAllUnits", 0.01f); // 함수를 0.01초 뒤 호출
                    rtsUnitController2.DeselectAll();
                }
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerEnemy_Alien)) {
                //Debug.Log("에일리언 선택");
                enemyInfo.OnEnemy_AlienImageClick(); // 에일리언 정보 불러오기
            }
        }

        // 마우스 오른쪽 클릭으로 유닛 이동
        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // 적유닛(layerEnemy_Alien)을 클릭했을 때
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerEnemy_Alien)) {
                if (hit.collider.gameObject == null) {
                    Debug.Log("오브젝트감지안됨");
                    return;
                }
                else {
                    Debug.Log("적찍음" + hit.collider.gameObject.tag);

                    rtsUnitController2.MoveSelectedUnitsEnemy(hit.collider.gameObject);
                }
            }
            // 땅(layerGround)을 클릭했을 때
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround)) {
                Vector3 targetPosition = hit.point;

                GameObject targetPointer = Instantiate(targetPointerPrefab, targetPosition, Quaternion.identity);

                Destroy(targetPointer, 3f);
                // 적이 사정거리 내에 있는지 확인
                Collider[] colliders = Physics.OverlapSphere(targetPosition, detectionRadius);
                foreach (Collider collider in colliders) {
                    if (collider.CompareTag("Enemy")) {
                        // 적이 사정거리 내에 있다면 해당 지점을 적의 위치로 변경
                        targetPosition = collider.transform.position;
                        break;
                    }
                }
                rtsUnitController2.MoveSelectedUnits(targetPosition);
                rtsUnitController.MoveSelectedUnits(targetPosition);
                rtsUnitController.MoveSelectedUnits2(targetPosition);
            }
        }
    }

    private void DeselectAllUnits() // 모든 유닛 선택 해제
    {

        // 선택 해제 전 유닛 정보창에서 유닛 이미지를 클릭 했는지 검사, 또는 적 이미지를 클릭 했는지 검사
        if (unitInfo.ClickUnitImage == false && enemyInfo.ClickUnitImage == false) {
            SwordStats swordStats = FindObjectOfType<SwordStats>();
            SorceressStats sorceressStats = FindObjectOfType<SorceressStats>();
            PriestStats priestStats = FindObjectOfType<PriestStats>();

            rtsUnitController.DeselectAll();
            rtsUnitController.DeselectAll2();
            rtsUnitController2.DeselectAll();
            enemyInfo.DeselectEnemy_Alien();

            unitInfo.UnitSponerWindow(false); // 유닛 생성창 비활성화
            unitInfo.SpawnImageMouseOut(); // 선택된 유닛  생성 이미지를 초기화 시킴

            if (swordStats != null) {
                unitInfo.SwordMasterWindow(false, swordStats.nowLv, swordStats.exp, swordStats.maxExp, swordStats.currentHealth, swordStats.maxHealth, swordStats.mp, swordStats.maxMp, swordStats.attackdamage, swordStats.speed);
            }
            if (sorceressStats != null) {
                unitInfo.SorceressWindow(false, sorceressStats.nowLv, sorceressStats.exp, sorceressStats.maxExp, sorceressStats.currentHealth, sorceressStats.maxHealth, sorceressStats.mp, sorceressStats.maxMp, sorceressStats.attackdamage, sorceressStats.speed);
            }
            if (priestStats != null) {
                unitInfo.PriestWindow(false, priestStats.nowLv, priestStats.exp, priestStats.maxExp, priestStats.currentHealth, priestStats.maxHealth, priestStats.mp, priestStats.maxMp, priestStats.attackdamage, priestStats.speed);
            }

        }
    }
    public void SkillOn(bool on) {
        skillon = on;
    }
}