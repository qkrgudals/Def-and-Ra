using UnityEngine;
using UnityEngine.UIElements;

public class EnemyInfo : MonoBehaviour {
    VisualElement enemy_Alien_Window; // ���ϸ���(1-���) UIâ
    VisualElement enemy_Drake_Window; // ���ϸ���(1-����) UIâ
    VisualElement enemy_Clawzombie_Window; // Ŭ������(1-����) UIâ
    VisualElement enemy_Wolf_Window; // ����(2-���) UIâ
    VisualElement enemy_Mutant_Window; // ����Ʈ(2-����) UIâ
    VisualElement enemy_Bestia_Window; // ����Ƽ��(2-����) UIâ
    VisualElement enemy_GreenGoblin_Window; // �׸� ���(3-���) UIâ
    VisualElement enemy_BlueGoblin_Window; // ��� ���(3-����) UIâ
    VisualElement enemy_KingGoblin_Window; // ŷ���(3-����) UIâ
    VisualElement enemy_BlockGolem_Window; // ��� ��(4-���) UIâ
    VisualElement enemy_StoneGolem_Window; // ���� ��(4-����) UIâ
    VisualElement enemy_GiantGolem_Window; // ���̾�Ʈ ��(4-����) UIâ


    VisualElement soldier_SpawnWindow;

    Label enemy_Alien_info; // ���ϸ����� ���� 
    VisualElement enemy_Alien_Img; // ���ϸ��� �̹���
    Label enemy_Drake_info;
    VisualElement enemy_Drake_Img;
    Label enemy_Clawzombie_info;
    VisualElement enemy_Clawzombie_Img;
    Label enemy_Wolf_info;
    VisualElement enemy_Wolf_Img;
    Label enemy_Mutant_info;
    VisualElement enemy_Mutant_Img;
    Label enemy_Bestia_info;
    VisualElement enemy_Bestia_Img;
    Label enemy_GreenGoblin_info;
    VisualElement enemy_GreenGoblin_Img;
    Label enemy_BlueGoblin_info;
    VisualElement enemy_BlueGoblin_Img;
    Label enemy_KingGoblin_info;
    VisualElement enemy_KingGoblin_Img;
    Label enemy_BlockGolem_info;
    VisualElement enemy_BlockGolem_Img;
    Label enemy_StoneGolem_info;
    VisualElement enemy_StoneGolem_Img;
    Label enemy_GiantGolem_info;
    VisualElement enemy_GiantGolem_Img;

    VisualElement enemy_Alien_info2; // ���ϸ����� �������� (�ִ� HP, ���� HP, �����Ŀ�, ���ǵ�)
    VisualElement archer_SpawnWindow;
    Label ail_MaxHP; // ���ϸ��� �ִ� HP ��
    Label ail_CurHP; // ���ϸ��� ���� HP ��
    Label ail_AttPW; // ���ϸ��� �����Ŀ� ��
    Label ail_Speed; // ���ϸ��� ���ǵ� ��
    Enemy enemy;
    private RTSUnitController rtsUnitController;
    private UnitInfo unitInfo;

    public bool ClickUnitImage = false; // ���� �̹��� Ŭ�� ����

    void Awake() {
        var root = GetComponent<UIDocument>().rootVisualElement; //UIDocment ������Ʈ�� ã��
        soldier_SpawnWindow = root.Q<VisualElement>("Soldier_SpawnWindow");
        enemy = FindObjectOfType<Enemy>();

        enemy_Alien_Window = root.Q<VisualElement>("Enemy-Alien_Window");
        enemy_Alien_info = root.Q<Label>("Enemy-Alien_Info");
        enemy_Alien_Img = root.Q<VisualElement>("Enemy-Alien_Img");

        enemy_Drake_Window = root.Q<VisualElement>("Enemy-Drake_Window");
        enemy_Drake_info = root.Q<Label>("Enemy-Drake_Info");
        enemy_Drake_Img = root.Q<VisualElement>("Enemy-Drake_Img");

        enemy_Clawzombie_Window = root.Q<VisualElement>("Enemy-Clawzombie_Window");
        enemy_Clawzombie_info = root.Q<Label>("Enemy-Clawzombie_Info");
        enemy_Clawzombie_Img = root.Q<VisualElement>("Enemy-Clawzombie_Img");

        enemy_Wolf_Window = root.Q<VisualElement>("Enemy-Wolf_Window");
        enemy_Wolf_info = root.Q<Label>("Enemy-Wolf_Info");
        enemy_Wolf_Img = root.Q<VisualElement>("Enemy-Wolf_Img");

        enemy_Mutant_Window = root.Q<VisualElement>("Enemy-Mutant_Window");
        enemy_Mutant_info = root.Q<Label>("Enemy-Mutant_Info");
        enemy_Mutant_Img = root.Q<VisualElement>("Enemy-Mutant_Img");

        enemy_Bestia_Window = root.Q<VisualElement>("Enemy-Bestia_Window");
        enemy_Bestia_info = root.Q<Label>("Enemy-Bestia_Info");
        enemy_Bestia_Img = root.Q<VisualElement>("Enemy-Bestia_Img");

        enemy_GreenGoblin_Window = root.Q<VisualElement>("Enemy-GreenGoblin_Window");
        enemy_GreenGoblin_info = root.Q<Label>("Enemy-GreenGoblin_Info");
        enemy_GreenGoblin_Img = root.Q<VisualElement>("Enemy-GreenGoblin_Img");

        enemy_BlueGoblin_Window = root.Q<VisualElement>("Enemy-BlueGoblin_Window");
        enemy_BlueGoblin_info = root.Q<Label>("Enemy-BlueGoblin_Info");
        enemy_BlueGoblin_Img = root.Q<VisualElement>("Enemy-BlueGoblin_Img");

        enemy_KingGoblin_Window = root.Q<VisualElement>("Enemy-KingGoblin_Window");
        enemy_KingGoblin_info = root.Q<Label>("Enemy-KingGoblin_Info");
        enemy_KingGoblin_Img = root.Q<VisualElement>("Enemy-KingGoblin_Img");

        enemy_BlockGolem_Window = root.Q<VisualElement>("Enemy-BlockGolem_Window");
        enemy_BlockGolem_info = root.Q<Label>("Enemy-BlockGolem_Info");
        enemy_BlockGolem_Img = root.Q<VisualElement>("Enemy-BlockGolem_Img");

        enemy_StoneGolem_Window = root.Q<VisualElement>("Enemy-StoneGolem_Window");
        enemy_StoneGolem_info = root.Q<Label>("Enemy-StoneGolem_Info");
        enemy_StoneGolem_Img = root.Q<VisualElement>("Enemy-StoneGolem_Img");

        enemy_GiantGolem_Window = root.Q<VisualElement>("Enemy-GiantGolem_Window");
        enemy_GiantGolem_info = root.Q<Label>("Enemy-GiantGolem_Info");
        enemy_GiantGolem_Img = root.Q<VisualElement>("Enemy-GiantGolem_Img");

        enemy_Alien_info2 = root.Q<VisualElement>("Enemy-Alien_Info2");
        ail_MaxHP = root.Q<Label>("Ail_MaxHP");
        ail_CurHP = root.Q<Label>("Ail_CurHP");
        ail_AttPW = root.Q<Label>("Ail_AttPW");
        ail_Speed = root.Q<Label>("Ail_Speed");
        archer_SpawnWindow = root.Q<VisualElement>("Archer_SpawnWindow");

        enemy_Alien_Img.RegisterCallback<MouseDownEvent>(evt => OnEnemy_AlienImageClick());
        enemy_Drake_Img.RegisterCallback<MouseDownEvent>(evt => OnEnemy_AlienImageClick());
        enemy_Clawzombie_Img.RegisterCallback<MouseDownEvent>(evt => OnEnemy_AlienImageClick());
        enemy_Wolf_Img.RegisterCallback<MouseDownEvent>(evt => OnEnemy_AlienImageClick());
        enemy_Mutant_Img.RegisterCallback<MouseDownEvent>(evt => OnEnemy_AlienImageClick());
        enemy_Bestia_Img.RegisterCallback<MouseDownEvent>(evt => OnEnemy_AlienImageClick());
        enemy_GreenGoblin_Img.RegisterCallback<MouseDownEvent>(evt => OnEnemy_AlienImageClick());
        enemy_BlueGoblin_Img.RegisterCallback<MouseDownEvent>(evt => OnEnemy_AlienImageClick());
        enemy_KingGoblin_Img.RegisterCallback<MouseDownEvent>(evt => OnEnemy_AlienImageClick());
        enemy_BlockGolem_Img.RegisterCallback<MouseDownEvent>(evt => OnEnemy_AlienImageClick());
        enemy_StoneGolem_Img.RegisterCallback<MouseDownEvent>(evt => OnEnemy_AlienImageClick());
        enemy_GiantGolem_Img.RegisterCallback<MouseDownEvent>(evt => OnEnemy_AlienImageClick());


        rtsUnitController = FindObjectOfType<RTSUnitController>();
        unitInfo = FindAnyObjectByType<UnitInfo>();

    }
    public void OnEnemy_AlienImageClick() // ���ϸ��� ���� �̹����� Ŭ������ ��
    {
        ClickUnitImage = true; // �̹��� Ŭ�� ���� true
                               // ���콺 Ŭ�� ��ġ���� �� ��ü ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject.CompareTag("Enemy")) {
                Enemy enemyClicked = hit.collider.GetComponent<Enemy>(); // �΋H�� ��ü�� Enemy ������Ʈ�� ������ �ִ��� Ȯ��
                
                if (enemyClicked != null) {
                    // Ŭ���� ���� ������ ǥ��
                    ShowStatusInfo_Enemy(enemyClicked);
                }
                if (hit.collider.gameObject.name.Equals("1_���(Clone)")) {
                    WindowHide();
                    enemy_Alien_Window.style.display = DisplayStyle.Flex; // ���ϸ��� ���� ǥ��
                    enemy_Alien_info2.style.display = DisplayStyle.Flex; // ���� ���������� ���÷��̻󿡼� ��Ÿ�� (soldier_info2)

                }
                else if (hit.collider.gameObject.name.Equals("1_����(Clone)")) {
                    WindowHide();
                    enemy_Drake_Window.style.display = DisplayStyle.Flex; // �巹��ũ ���� ǥ��
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("1_����(Clone)")) {
                    WindowHide();
                    enemy_Clawzombie_Window.style.display = DisplayStyle.Flex; // Ŭ�� ���� ���� ǥ��
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("2_���(Clone)")) {
                    WindowHide();
                    enemy_Wolf_Window.style.display = DisplayStyle.Flex; // ���� ���� ǥ��
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("2_����(Clone)")) {
                    WindowHide();
                    enemy_Mutant_Window.style.display = DisplayStyle.Flex; // ����Ʈ ���� ǥ��
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("2_����(Clone)")) {
                    WindowHide();
                    enemy_Bestia_Window.style.display = DisplayStyle.Flex; // ����Ƽ�� ���� ǥ��
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("3_���(Clone)")) {
                    WindowHide();
                    enemy_GreenGoblin_Window.style.display = DisplayStyle.Flex; // �׸� ��� ���� ǥ��
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("3_����(Clone)")) {
                    WindowHide();
                    enemy_BlueGoblin_Window.style.display = DisplayStyle.Flex; // ��� ��� ���� ǥ��
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("3_����(Clone)")) {
                    WindowHide();
                    enemy_KingGoblin_Window.style.display = DisplayStyle.Flex; // ŷ��� ���� ǥ��
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("4_���(Clone)")) {
                    WindowHide();
                    enemy_BlockGolem_Window.style.display = DisplayStyle.Flex; // ��� �� ���� ǥ��
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("4_����(Clone)")) {
                    WindowHide();
                    enemy_StoneGolem_Window.style.display = DisplayStyle.Flex; // ���� �� ���� ǥ��
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("4_����(Clone)")) {
                    WindowHide();
                    enemy_GiantGolem_Window.style.display = DisplayStyle.Flex; // ���̾�Ʈ �� ���� ǥ��
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
            }

        }

        void WindowHide()
        {
            enemy_Alien_Window.style.display = DisplayStyle.None; 
            enemy_Drake_Window.style.display = DisplayStyle.None;
            enemy_Clawzombie_Window.style.display = DisplayStyle.None;
            enemy_Wolf_Window.style.display = DisplayStyle.None;
            enemy_Mutant_Window.style.display = DisplayStyle.None;
            enemy_Bestia_Window.style.display = DisplayStyle.None;
            enemy_GreenGoblin_Window.style.display = DisplayStyle.None;
            enemy_BlueGoblin_Window.style.display = DisplayStyle.None;
            enemy_KingGoblin_Window.style.display = DisplayStyle.None;
            enemy_BlockGolem_Window.style.display = DisplayStyle.None;
            enemy_StoneGolem_Window.style.display = DisplayStyle.None;
            enemy_GiantGolem_Window.style.display = DisplayStyle.None;
            unitInfo.sorceress_Window.style.display = DisplayStyle.None;
            unitInfo.sorceress_info2.style.display = DisplayStyle.None;
            unitInfo.swordMaster_Window.style.display = DisplayStyle.None;
            unitInfo.swordMaster_info2.style.display = DisplayStyle.None;
            unitInfo.priest_Window.style.display = DisplayStyle.None;
            unitInfo.priest_info2.style.display = DisplayStyle.None;
        }

        rtsUnitController.DeselectAll(); // �˻� ���� ���� ���� ����
        rtsUnitController.DeselectAll2(); // �ü� ���� ���� ���� ����
        unitInfo.DeselectUnitHouse(); // ���� ������ ���� ����
        
        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }
    void ShowStatusInfo_Enemy(Enemy enemyClicked) // �� ��������
    {

        enemy_Alien_info2.style.display = DisplayStyle.Flex; // ���� ���������� ���÷��̻󿡼� ��Ÿ�� (soldier_info2)

        string maxHPText = "Max HP: " + enemyClicked.hp;
        
        // UI Toolkit�� �󺧿� �������� ������ ���ڿ� ��� (�� �ִ� HP)
        ail_MaxHP.text = maxHPText;

        string curHPText = "Current HP: " + enemyClicked.hp;
        //UI Toolkit�� �󺧿� �������� ������ ���ڿ� ��� (�� ���� HP)
        ail_CurHP.text = curHPText;

        string attPWText = "Attack Power: " + enemyClicked.damage;
        // UI Toolkit�� �󺧿� �������� ������ ���ڿ� ��� (�� ���ݷ�)
        ail_AttPW.text = attPWText;

        string speedText = "Speed: " + enemyClicked.moveSpeed * 10; // ���� ���ǵ忡 10�� ����
        // UI Toolkit�� �󺧿� �������� ������ ���ڿ� ��� (�� ���ǵ�)
        ail_Speed.text = speedText;

        soldier_SpawnWindow.style.display = DisplayStyle.None; // �˻� ����â�� ����
        archer_SpawnWindow.style.display = DisplayStyle.None; // �ü� ����â�� ����
    }
    public void DeselectEnemy_Alien() // ���ϸ����� �������� �ʾ��� �� 
    {
        enemy_Alien_Window.style.display = DisplayStyle.None; // ���ϸ��� ���� �����
        enemy_Drake_Window.style.display = DisplayStyle.None;
        enemy_Clawzombie_Window.style.display = DisplayStyle.None;
        enemy_Wolf_Window.style.display = DisplayStyle.None;
        enemy_Mutant_Window.style.display = DisplayStyle.None;
        enemy_Bestia_Window.style.display = DisplayStyle.None;
        enemy_GreenGoblin_Window.style.display = DisplayStyle.None;
        enemy_BlueGoblin_Window.style.display = DisplayStyle.None;
        enemy_KingGoblin_Window.style.display = DisplayStyle.None;
        enemy_BlockGolem_Window.style.display = DisplayStyle.None;
        enemy_StoneGolem_Window.style.display = DisplayStyle.None;
        enemy_GiantGolem_Window.style.display = DisplayStyle.None;

        enemy_Alien_info2.style.display = DisplayStyle.None;
    }

    private void ClickUnitImage_DelayTime() // 0.02�� �� ���� �̹��� Ŭ������ ����
    {
        ClickUnitImage = false;
    }
}
