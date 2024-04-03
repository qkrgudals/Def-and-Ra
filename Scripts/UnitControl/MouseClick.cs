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

    float detectionRadius = 5f; // �� ���� ����

    VisualElement soldier_Img; // �˻� �̹���
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
        // ���콺 ���� Ŭ������ ���� ���� or ����
        if (!skillon && Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // ������ �ε����� ������Ʈ�� ���� �� (=������ Ŭ������ ��)
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
            // ������ �ε����� ������Ʈ�� ���� �� (=������ Ŭ���� ������ ��)
            else {
                //Debug.Log(hit.transform);
                if (!Input.GetKey(KeyCode.LeftShift)) // ���� ����Ʈ Ű�� ������ �ʰ� ������ Ŭ������ ���� ��
                {
                    Invoke("DeselectAllUnits", 0.01f); // �Լ��� 0.01�� �� ȣ��
                    rtsUnitController2.DeselectAll();
                }
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerEnemy_Alien)) {
                //Debug.Log("���ϸ��� ����");
                enemyInfo.OnEnemy_AlienImageClick(); // ���ϸ��� ���� �ҷ�����
            }
        }

        // ���콺 ������ Ŭ������ ���� �̵�
        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // ������(layerEnemy_Alien)�� Ŭ������ ��
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerEnemy_Alien)) {
                if (hit.collider.gameObject == null) {
                    Debug.Log("������Ʈ�����ȵ�");
                    return;
                }
                else {
                    Debug.Log("������" + hit.collider.gameObject.tag);

                    rtsUnitController2.MoveSelectedUnitsEnemy(hit.collider.gameObject);
                }
            }
            // ��(layerGround)�� Ŭ������ ��
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround)) {
                Vector3 targetPosition = hit.point;

                GameObject targetPointer = Instantiate(targetPointerPrefab, targetPosition, Quaternion.identity);

                Destroy(targetPointer, 3f);
                // ���� �����Ÿ� ���� �ִ��� Ȯ��
                Collider[] colliders = Physics.OverlapSphere(targetPosition, detectionRadius);
                foreach (Collider collider in colliders) {
                    if (collider.CompareTag("Enemy")) {
                        // ���� �����Ÿ� ���� �ִٸ� �ش� ������ ���� ��ġ�� ����
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

    private void DeselectAllUnits() // ��� ���� ���� ����
    {

        // ���� ���� �� ���� ����â���� ���� �̹����� Ŭ�� �ߴ��� �˻�, �Ǵ� �� �̹����� Ŭ�� �ߴ��� �˻�
        if (unitInfo.ClickUnitImage == false && enemyInfo.ClickUnitImage == false) {
            SwordStats swordStats = FindObjectOfType<SwordStats>();
            SorceressStats sorceressStats = FindObjectOfType<SorceressStats>();
            PriestStats priestStats = FindObjectOfType<PriestStats>();

            rtsUnitController.DeselectAll();
            rtsUnitController.DeselectAll2();
            rtsUnitController2.DeselectAll();
            enemyInfo.DeselectEnemy_Alien();

            unitInfo.UnitSponerWindow(false); // ���� ����â ��Ȱ��ȭ
            unitInfo.SpawnImageMouseOut(); // ���õ� ����  ���� �̹����� �ʱ�ȭ ��Ŵ

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