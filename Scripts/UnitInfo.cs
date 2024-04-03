using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class UnitInfo : MonoBehaviour {
    int soldier_count; // �˻� ���ü� üũ
    int archer_count; // �ü� ���ü� üũ

    VisualElement soldier_Window; // �˻� UIâ
    Label soldier_info; // �˻��� ���� (���õ� �˻� ��)
    VisualElement soldier_Img; // �˻� ���� �̹���
    VisualElement soldier_info2; // �˻��� �������� (�ִ� HP, ���� HP, �����Ŀ�, ���ǵ�)
    Label sol_MaxHP; // �˻� �ִ� HP ��
    Label sol_CurHP; // �˻� ���� HP ��
    int soldier_currentHP;
    Label sol_AttPW; // �˻� �����Ŀ� ��
    Label sol_Speed; // �˻� ���ǵ� ��
    Soldier_UnitController soldier_UnitCtrInfo; // �˻� ������Ʈ�ѷ� ������ ã������ ���� (�˻� �����Ŀ�)
    VisualElement soldier_SpawnWindow;
    VisualElement soldier_SpawnImg;
    VisualElement spawn_1SoldierIcon;
    VisualElement spawn_2SoldierIcon;
    VisualElement spawn_3SoldierIcon;
    VisualElement spawn_4SoldierIcon;
    VisualElement spawn_5SoldierIcon;
    VisualElement spawn_6SoldierIcon;

    VisualElement archer_Window; // �ü� UIâ
    Label archer_info; // �ü��� ���� (���õ� �ü��� ��)
    VisualElement archer_Img; // �ü� ���� �̹���
    VisualElement archer_info2; // �ü��� �������� (�ִ� HP, ���� HP, �����Ŀ�, ���ǵ�)
    Label arc_MaxHP; // �ü� �ִ� HP ��
    Label arc_CurHP; // �ü� ���� HP ��
    int archer_currentHP;
    Label arc_AttPW; // �ü� �����Ŀ� ��
    Label arc_Speed; // �ü� ���ǵ� ��
    Archer_UnitController archer_UnitCtrInfo; // �ü� ������Ʈ�ѷ� ������ ã������ ���� (�ü� �����Ŀ�)
    VisualElement archer_SpawnWindow;
    VisualElement archer_SpawnImg;
    VisualElement spawn_1ArcherIcon;
    VisualElement spawn_2ArcherIcon;
    VisualElement spawn_3ArcherIcon;
    VisualElement spawn_4ArcherIcon;
    VisualElement spawn_5ArcherIcon;
    VisualElement spawn_6ArcherIcon;

    UnitHP_Soldier unitHPInfo_Soldier; // �˻� HP������ ã������ ���� (���� �ִ� HP, ���� HP)
    UnitHP_Archer unitHPInfo_Archer; // �˻� HP������ ã������ ���� (���� �ִ� HP, ���� HP)
    NavMeshAgent unitSpeedInfo; // NavMeshAgent�� ���ǵ� ������ ã�� ���� ���� (���� ���ǵ�)
    Health health;

    VisualElement swordMasterIcon;
    public VisualElement swordMaster_Window;
    VisualElement swordMaster_Img;
    public VisualElement swordMaster_info2;
    Label sMa_CurLv;
    int swordMaster_CurrentLv; // �ҵ帶���� ���� Lv ��
    Label sMa_Exp;
    int swordMaster_CurrentExp; // �ҵ帶���� ���� ����ġ ��
    int swordMaster_MaxExp;
    Label sMa_CurHP; // �ҵ帶���� ���� HP ��
    int swordMaster_CurrentHP;
    int swordMaster_MaxHP;
    Label sMa_CurMP; // �ҵ帶���� ���� MP ��
    int swordMaster_CurrentMP;
    int swordMaster_MaxMP;
    Label sMa_AttPW; // �ҵ帶���� �����Ŀ� ��
    int swordMaster_AttackPower;
    Label sMa_Speed; // �ҵ帶���� ���ǵ� ��
    float swordMaster_Speed;

    VisualElement sorceressIcon;
    VisualElement sorceressIcon_Lock;
    public VisualElement sorceress_Window;
    VisualElement sorceress_Img;
    public VisualElement sorceress_info2;
    Label scr_CurLv; //�Ҽ����� ���� Lv ��
    int sorceress_CurrentLv;
    Label scr_Exp; // �Ҽ����� ���� ����ġ ��
    int sorceress_CurrentExp;
    int sorceress_MaxExp;
    Label scr_CurHP; // �Ҽ����� ���� HP ��
    int sorceress_CurrentHP;
    int sorceress_MaxHP;
    Label scr_CurMP; // �Ҽ����� ���� MP ��
    int sorceress_CurrentMP;
    int sorceress_MaxMP;
    Label scr_AttPW; // �Ҽ����� �����Ŀ� ��
    int sorceress_AttackPower;
    Label scr_Speed; // �Ҽ����� ���ǵ� ��
    float sorceress_Speed;

    VisualElement priestIcon;
    VisualElement priestIcon_Lock;
    public VisualElement priest_Window;
    VisualElement priest_Img;
    public VisualElement priest_info2;
    Label pri_CurLv; // ������Ʈ ���� Lv ��
    int priest_CurrentLv;
    Label pri_Exp; // ������Ʈ ���� ����ġ ��
    int priest_CurrentExp;
    int priest_MaxExp;
    Label pri_CurHP; // ������Ʈ ���� HP ��
    int priest_CurrentHP;
    int priest_MaxHP;
    Label pri_CurMP; // ������Ʈ ���� MP ��
    int priest_CurrentMP;
    int priest_MaxMP;
    Label pri_AttPW; // ������Ʈ �����Ŀ� ��
    int priest_AttackPower;
    Label pri_Speed; // ������Ʈ ���ǵ� ��
    float priest_Speed;


    bool allowDisplaySoldierInfo = true; // �˻��� ���� ������ ���÷��̻� ��Ÿ���°� �������� �����ϴ� �Ҹ��� ��
    bool allowDisplayArcherInfo = true;

    private RTSUnitController rtsUnitController;
    private EnemyInfo enemyInfo;

    public bool ClickUnitImage = false; // ���� �̹��� Ŭ�� ����

    bool soldier_SpawnImgClick = false; // �˻� ���� �̹��� Ŭ������ üũ
    bool archer_SpawnImgClick = false; // �ü� ���� �̹��� Ŭ������ üũ

    public float focusCameraSpeed = 10f;

    ScreenController screenController;

    UnitSpawnLoadingBar loadingBar;

    EnemyManager enemyManager;


    void Awake() {
        var root = GetComponent<UIDocument>().rootVisualElement; //UIDocment ������Ʈ�� ã��
        soldier_Window = root.Q<VisualElement>("Soldier_Window"); //Info ���� ���� ������ VisualElement ������Ʈ�� ã��
        soldier_info = root.Q<Label>("Soldier_Info"); // info ���� ã�� ������ ����
        soldier_Img = root.Q<VisualElement>("Soldier_Img");
        soldier_info2 = root.Q<VisualElement>("Soldier_Info2");
        sol_MaxHP = root.Q<Label>("Sol_MaxHP");
        sol_CurHP = root.Q<Label>("Sol_CurHP");
        sol_AttPW = root.Q<Label>("Sol_AttPW");
        sol_Speed = root.Q<Label>("Sol_Speed");

        soldier_SpawnWindow = root.Q<VisualElement>("Soldier_SpawnWindow");
        soldier_SpawnImg = root.Q<VisualElement>("Soldier_SpawnImg");

        spawn_1SoldierIcon = root.Q<VisualElement>("Spawn_1SoldierIcon");
        spawn_2SoldierIcon = root.Q<VisualElement>("Spawn_2SoldierIcon");
        spawn_3SoldierIcon = root.Q<VisualElement>("Spawn_3SoldierIcon");
        spawn_4SoldierIcon = root.Q<VisualElement>("Spawn_4SoldierIcon");
        spawn_5SoldierIcon = root.Q<VisualElement>("Spawn_5SoldierIcon");
        spawn_6SoldierIcon = root.Q<VisualElement>("Spawn_6SoldierIcon");

        archer_Window = root.Q<VisualElement>("Archer_Window");
        archer_info = root.Q<Label>("Archer_Info");
        archer_Img = root.Q<VisualElement>("Archer_Img");
        archer_info2 = root.Q<VisualElement>("Archer_Info2");
        arc_MaxHP = root.Q<Label>("Arc_MaxHP");
        arc_CurHP = root.Q<Label>("Arc_CurHP");
        arc_AttPW = root.Q<Label>("Arc_AttPW");
        arc_Speed = root.Q<Label>("Arc_Speed");

        archer_SpawnWindow = root.Q<VisualElement>("Archer_SpawnWindow");
        archer_SpawnImg = root.Q<VisualElement>("Archer_SpawnImg");

        spawn_1ArcherIcon = root.Q<VisualElement>("Spawn_1ArcherIcon");
        spawn_2ArcherIcon = root.Q<VisualElement>("Spawn_2ArcherIcon");
        spawn_3ArcherIcon = root.Q<VisualElement>("Spawn_3ArcherIcon");
        spawn_4ArcherIcon = root.Q<VisualElement>("Spawn_4ArcherIcon");
        spawn_5ArcherIcon = root.Q<VisualElement>("Spawn_5ArcherIcon");
        spawn_6ArcherIcon = root.Q<VisualElement>("Spawn_6ArcherIcon");

        swordMasterIcon = root.Q<VisualElement>("SwordMasterIcon");
        swordMaster_Window = root.Q<VisualElement>("SwordMaster_Window");
        swordMaster_Img = root.Q<VisualElement>("SwordMaster_Img");
        swordMaster_info2 = root.Q<VisualElement>("SwordMaster_Info2");
        sMa_CurLv = root.Q<Label>("SMa_CurLv");
        sMa_Exp = root.Q<Label>("SMa_Exp");
        sMa_CurHP = root.Q<Label>("SMa_CurHP");
        sMa_CurMP = root.Q<Label>("SMa_CurMP");
        sMa_AttPW = root.Q<Label>("SMa_AttPW");
        sMa_Speed = root.Q<Label>("SMa_Speed");

        sorceressIcon = root.Q<VisualElement>("SorceressIcon");
        sorceressIcon_Lock = root.Q<VisualElement>("SorceressIcon_Lock");
        sorceress_Window = root.Q<VisualElement>("Sorceress_Window");
        sorceress_Img = root.Q<VisualElement>("Sorceress_Img");
        sorceress_info2 = root.Q<VisualElement>("Sorceress_Info2");
        scr_CurLv = root.Q<Label>("Scr_CurLv");
        scr_Exp = root.Q<Label>("Scr_Exp");
        scr_CurHP = root.Q<Label>("Scr_CurHP");
        scr_CurMP = root.Q<Label>("Scr_CurMP");
        scr_AttPW = root.Q<Label>("Scr_AttPW");
        scr_Speed = root.Q<Label>("Scr_Speed");

        priestIcon = root.Q<VisualElement>("PriestIcon");
        priestIcon_Lock = root.Q<VisualElement>("PriestIcon_Lock");
        priest_Window = root.Q<VisualElement>("Priest_Window");
        priest_Img = root.Q<VisualElement>("Priest_Img");
        priest_info2 = root.Q<VisualElement>("Priest_Info2");
        pri_CurLv = root.Q<Label>("Pri_CurLv");
        pri_Exp = root.Q<Label>("Pri_Exp");
        pri_CurHP = root.Q<Label>("Pri_CurHP");
        pri_CurMP = root.Q<Label>("Pri_CurMP");
        pri_AttPW = root.Q<Label>("Pri_AttPW");
        pri_Speed = root.Q<Label>("Pri_Speed");

        soldier_UnitCtrInfo = FindObjectOfType<Soldier_UnitController>();
        archer_UnitCtrInfo = FindObjectOfType<Archer_UnitController>();
        unitHPInfo_Soldier = FindObjectOfType<UnitHP_Soldier>();
        unitHPInfo_Archer = FindObjectOfType<UnitHP_Archer>();
        health = FindObjectOfType<Health>();
        unitSpeedInfo = FindObjectOfType<NavMeshAgent>();

        soldier_Img.RegisterCallback<MouseDownEvent>(evt => OnSoldierImageClick()); // �˻� ���� �̹����� Ŭ������ �� 
        archer_Img.RegisterCallback<MouseDownEvent>(evt => OnArcherImageClick());

        soldier_SpawnImg.RegisterCallback<MouseDownEvent>(evt => OnSoldierSpawnImageClick()); // �˻� ���� �̹��� Ŭ��
        soldier_SpawnImg.RegisterCallback<MouseEnterEvent>(evt => OnSoldierSpawnImageMouseHover());
        soldier_SpawnImg.RegisterCallback<MouseOutEvent>(evt => OnSoldierSpawnImageMouseOut());

        archer_SpawnImg.RegisterCallback<MouseDownEvent>(evt => OnArcherSpawnImageClick()); // �ü� ���� �̹��� Ŭ��
        archer_SpawnImg.RegisterCallback<MouseEnterEvent>(evt => OnArcherSpawnImageMouseHover());
        archer_SpawnImg.RegisterCallback<MouseOutEvent>(evt => OnArcherSpawnImageMouseOut());

        spawn_1SoldierIcon.RegisterCallback<MouseDownEvent>(evt => OnSoldierSpawnIconImageClick(1));
        spawn_2SoldierIcon.RegisterCallback<MouseDownEvent>(evt => OnSoldierSpawnIconImageClick(2));
        spawn_3SoldierIcon.RegisterCallback<MouseDownEvent>(evt => OnSoldierSpawnIconImageClick(3));
        spawn_4SoldierIcon.RegisterCallback<MouseDownEvent>(evt => OnSoldierSpawnIconImageClick(4));
        spawn_5SoldierIcon.RegisterCallback<MouseDownEvent>(evt => OnSoldierSpawnIconImageClick(5));
        spawn_6SoldierIcon.RegisterCallback<MouseDownEvent>(evt => OnSoldierSpawnIconImageClick(6));

        spawn_1ArcherIcon.RegisterCallback<MouseDownEvent>(evt => OnArcherSpawnIconImageClick(1));
        spawn_2ArcherIcon.RegisterCallback<MouseDownEvent>(evt => OnArcherSpawnIconImageClick(2));
        spawn_3ArcherIcon.RegisterCallback<MouseDownEvent>(evt => OnArcherSpawnIconImageClick(3));
        spawn_4ArcherIcon.RegisterCallback<MouseDownEvent>(evt => OnArcherSpawnIconImageClick(4));
        spawn_5ArcherIcon.RegisterCallback<MouseDownEvent>(evt => OnArcherSpawnIconImageClick(5));
        spawn_6ArcherIcon.RegisterCallback<MouseDownEvent>(evt => OnArcherSpawnIconImageClick(6));

        swordMasterIcon.RegisterCallback<MouseDownEvent>(evt => SwordMasterWindow(true, swordMaster_CurrentLv, swordMaster_CurrentExp, swordMaster_MaxExp, swordMaster_CurrentHP, swordMaster_MaxHP, swordMaster_CurrentMP, swordMaster_MaxMP, swordMaster_AttackPower, swordMaster_Speed));
        swordMaster_Img.RegisterCallback<MouseDownEvent>(evt => OnSwordMasterImageClick());

        sorceressIcon.RegisterCallback<MouseDownEvent>(evt => SorceressWindow(true, sorceress_CurrentLv, sorceress_CurrentExp, sorceress_MaxExp, sorceress_CurrentHP, sorceress_MaxHP, sorceress_CurrentMP, sorceress_MaxMP, sorceress_AttackPower, sorceress_Speed));
        sorceress_Img.RegisterCallback<MouseDownEvent>(evt => OnSorceressImageClick());

        priestIcon.RegisterCallback<MouseDownEvent>(evt => PriestWindow(true, priest_CurrentLv, priest_CurrentExp, priest_MaxExp, priest_CurrentHP, priest_MaxHP, priest_CurrentMP, priest_MaxMP, priest_AttackPower, priest_Speed));
        priest_Img.RegisterCallback<MouseDownEvent>(evt => OnPriestImageClick());

        rtsUnitController = FindObjectOfType<RTSUnitController>();
        enemyInfo = FindObjectOfType<EnemyInfo>();

        loadingBar = FindObjectOfType<UnitSpawnLoadingBar>();
        screenController = FindObjectOfType<ScreenController>();
        enemyManager = FindAnyObjectByType<EnemyManager>();
    }

    private void FixedUpdate()
    {
        /*
        switch (enemyManager.currentStage)
        {
            case 0: sorceressIcon_Lock.style.display = DisplayStyle.Flex; priestIcon_Lock.style.display = DisplayStyle.Flex; break; // �������� 1
            case <= 1: sorceressIcon_Lock.style.display = DisplayStyle.None; sorceressIcon.style.display = DisplayStyle.Flex; break; // �������� 2 �̻�
            case <= 3: priestIcon_Lock.style.display = DisplayStyle.None; priestIcon.style.display = DisplayStyle.Flex; break; // �������� 4 
        }
        */
    }

    public void ShowSelectionInfo_Soldier(int count) // �˻� ���� ����
    {
        soldier_count = count; // �˻� ���ü� ������ ����
        if (count > 0) {
            allowDisplayArcherInfo = false; // �ü� ����ǥ�� ��Ÿ���� ���
            soldier_Window.style.display = DisplayStyle.Flex; // �˻� ����â�� ���÷��̻󿡼� ��Ÿ��

            archer_info2.style.display = DisplayStyle.None;
            enemyInfo.DeselectEnemy_Alien(); // �� ���ϸ��� ���� �����

            // �˻��� ���õ� ������ ���ڿ��� �����Ͽ� ����
            string infoText = "Soldier: " + count;
            // UI Toolkit�� �󺧿� �������� ������ ���ڿ� ��� (���� �˻� ��)
            soldier_info.text = infoText;

            if (allowDisplaySoldierInfo == true) {
                ShowStatusInfo_Soldier(soldier_currentHP); // �˻� �������� ǥ��
            }
        }
        else {
            allowDisplayArcherInfo = true; // �ü� ����ǥ�� ��Ÿ���� ����
            soldier_Window.style.display = DisplayStyle.None;
            soldier_info2.style.display = DisplayStyle.None;
        }
        soldier_SpawnWindow.style.display = DisplayStyle.None; // �˻� ����â�� ����
        archer_SpawnWindow.style.display = DisplayStyle.None; // �ü� ����â�� ����

        swordMaster_Window.style.display = DisplayStyle.None;
        swordMaster_info2.style.display = DisplayStyle.None;
    }

    public void ShowSelectionInfo_Archer(int count) // �ü� ���� ����
    {
        archer_count = count; // �ü� ���ü� ������ ����

        if (count > 0) {
            allowDisplaySoldierInfo = false; // �˻� ����ǥ�� ��Ÿ���� ���
            archer_Window.style.display = DisplayStyle.Flex; // �ü� ����â�� ���÷��̻󿡼� ��Ÿ��

            soldier_info2.style.display = DisplayStyle.None;
            enemyInfo.DeselectEnemy_Alien(); // �� ���ϸ��� ���� �����

            // �ü��� ���õ� ������ ���ڿ��� �����Ͽ� ����
            string infoText = "Archer: " + count;

            archer_info.text = infoText;

            if (allowDisplayArcherInfo == true) {
                ShowStatusInfo_Archer(archer_currentHP); // �ü� �������� ǥ��
            }
        }
        else {
            allowDisplaySoldierInfo = true; // �˻� ����ǥ�� ��Ÿ���� ����
            archer_Window.style.display = DisplayStyle.None;
            archer_info2.style.display = DisplayStyle.None;
        }
        soldier_SpawnWindow.style.display = DisplayStyle.None; // �˻� ����â�� ����
        archer_SpawnWindow.style.display = DisplayStyle.None; // �ü� ����â�� ����

        swordMaster_Window.style.display = DisplayStyle.None;
        swordMaster_info2.style.display = DisplayStyle.None;
    }
    private void OnSoldierImageClick() // �˻� ���� �̹����� Ŭ������ ��
    {
        ClickUnitImage = true; // �̹��� Ŭ�� ���� true
        rtsUnitController.DeselectAll2(); // �ü� ���� ���� ���� ����

        ShowStatusInfo_Soldier(soldier_currentHP);

        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }

    private void OnArcherImageClick() // �ü� ���� �̹����� Ŭ������ ��
    {
        ClickUnitImage = true; // �̹��� Ŭ�� ���� true
        rtsUnitController.DeselectAll(); // �˻� ���� ���� ���� ����

        ShowStatusInfo_Archer(archer_currentHP);

        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }

    public void ShowStatusInfo_Soldier(int currentHP) // �˻� ��������
    {
        soldier_currentHP = currentHP;

        if (soldier_count == 1) // ������ �˻簡 �Ѹ��� ����
        {
            sol_CurHP.style.display = DisplayStyle.Flex; // ���� HP ���� �����ֱ�
        }
        else {
            sol_CurHP.style.display = DisplayStyle.None;
        }

        soldier_info2.style.display = DisplayStyle.Flex; // ���� ���������� ���÷��̻󿡼� ��Ÿ�� (soldier_info2)

        string maxHPText = "Max HP: " + unitHPInfo_Soldier.MaxHP; // (�˻� �ִ� HP)
        sol_MaxHP.text = maxHPText;

        string curHPText = "Current HP: " + currentHP; // (�˻� ���� HP)
        sol_CurHP.text = curHPText;

        string attPWText = "Attack Power: " + soldier_UnitCtrInfo.damageAmount; // (�˻� ���ݷ�)
        sol_AttPW.text = attPWText;

        string speedText = "Speed: " + unitSpeedInfo.speed * 10; // ���� ���ǵ忡 10�� ���� // (�˻� ���ǵ�)
        sol_Speed.text = speedText;

        archer_Window.style.display = DisplayStyle.None; // �ü� â�� ����
        soldier_SpawnWindow.style.display = DisplayStyle.None; // �˻� ����â�� ����
        archer_SpawnWindow.style.display = DisplayStyle.None; // �ü� ����â�� ����
    }
    public void CurrentHP_Soldier(int currentHP) {
        string curHPText = "Current HP: " + currentHP;
        sol_CurHP.text = curHPText;
    }

    public void ShowStatusInfo_Archer(int currentHP) // �ü� ��������
    {
        archer_currentHP = currentHP;
        if (archer_count == 1) // ������ �ü��� �Ѹ��� ����
        {
            arc_CurHP.style.display = DisplayStyle.Flex; // ���� HP ���� �����ֱ�
        }
        else {
            arc_CurHP.style.display = DisplayStyle.None;
        }

        archer_info2.style.display = DisplayStyle.Flex; // ���� ���������� ���÷��̻󿡼� ��Ÿ�� (soldier_info2)

        string maxHPText = "Max HP: " + unitHPInfo_Archer.MaxHP;
        arc_MaxHP.text = maxHPText;

        string curHPText = "Current HP: " + currentHP;
        arc_CurHP.text = curHPText;

        string attPWText = "Attack Power: " + archer_UnitCtrInfo.damageAmount;
        arc_AttPW.text = attPWText;

        string speedText = "Speed: " + unitSpeedInfo.speed * 10; // ���� ���ǵ忡 10�� ����
        arc_Speed.text = speedText;

        soldier_Window.style.display = DisplayStyle.None; // �˻� â�� ����
        soldier_SpawnWindow.style.display = DisplayStyle.None;
        archer_SpawnWindow.style.display = DisplayStyle.None;

        swordMaster_Window.style.display = DisplayStyle.None;
        swordMaster_info2.style.display = DisplayStyle.None;
    }
    public void CurrentHP_Archer(int currentHP) {
        string curHPText = "Current HP: " + currentHP;
        arc_CurHP.text = curHPText;
    }

    private void ClickUnitImage_DelayTime() // 0.02�� �� ���� �̹��� Ŭ������ ����
    {
        ClickUnitImage = false;
    }

    public void UnitSponerWindow(bool active) // ���� ����â
    {
        if (active == true) // ���� ����â Ȱ��ȭ
        {
            ClickUnitImage = true; // �̹��� Ŭ�� ���� true

            soldier_SpawnWindow.style.display = DisplayStyle.Flex; // �˻� ����â�� ���÷��̻󿡼� ��Ÿ��
            soldier_Window.style.display = DisplayStyle.None;
            soldier_info2.style.display = DisplayStyle.None;

            archer_SpawnWindow.style.display = DisplayStyle.Flex; // �ü� ����â�� ���÷��̻󿡼� ��Ÿ��
            archer_Window.style.display = DisplayStyle.None;
            archer_info2.style.display = DisplayStyle.None;

            swordMaster_Window.style.display = DisplayStyle.None;
            swordMaster_info2.style.display = DisplayStyle.None;

            sorceress_Window.style.display = DisplayStyle.None;
            sorceress_info2.style.display = DisplayStyle.None;

            priest_Window.style.display = DisplayStyle.None;
            priest_info2.style.display = DisplayStyle.None;

            enemyInfo.DeselectEnemy_Alien(); // �� ���ϸ��� ���� �����

            Invoke("ClickUnitImage_DelayTime", 0.02f);
        }
        else // ���� ����â ��Ȱ��ȭ
        {
            DeselectUnitHouse();
        }
    }

    public void DeselectUnitHouse() // ���� ������ ���� ������
    {
        soldier_SpawnWindow.style.display = DisplayStyle.None;
        archer_SpawnWindow.style.display = DisplayStyle.None;

        spawn_1SoldierIcon.style.display = DisplayStyle.None;
        spawn_2SoldierIcon.style.display = DisplayStyle.None;
        spawn_3SoldierIcon.style.display = DisplayStyle.None;
        spawn_4SoldierIcon.style.display = DisplayStyle.None;
        spawn_5SoldierIcon.style.display = DisplayStyle.None;
        spawn_6SoldierIcon.style.display = DisplayStyle.None;

        spawn_1ArcherIcon.style.display = DisplayStyle.None;
        spawn_2ArcherIcon.style.display = DisplayStyle.None;
        spawn_3ArcherIcon.style.display = DisplayStyle.None;
        spawn_4ArcherIcon.style.display = DisplayStyle.None;
        spawn_5ArcherIcon.style.display = DisplayStyle.None;
        spawn_6ArcherIcon.style.display = DisplayStyle.None;

        swordMasterIcon.style.display = DisplayStyle.Flex;

        switch (enemyManager.currentStage) // ���������� ���� ��� ����
        {
            case 0: sorceressIcon_Lock.style.display = DisplayStyle.Flex; priestIcon_Lock.style.display = DisplayStyle.Flex; break; // �������� 1
            case <=1 : sorceressIcon_Lock.style.display = DisplayStyle.None; sorceressIcon.style.display = DisplayStyle.Flex; break; // �������� 2 �̻�
            case <=3: priestIcon_Lock.style.display = DisplayStyle.None; priestIcon.style.display = DisplayStyle.Flex; break; // �������� 4 
        }
    }

    private void OnSoldierSpawnImageClick() // �˻� ���� �̹����� Ŭ������ ��
    {
        ClickUnitImage = true; // �̹��� Ŭ�� ���� true

        soldier_SpawnImgClick = true;
        archer_SpawnImgClick = false;

        soldier_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(1f, 1f, 1f, 1f)); // �˻� ���� �� ����
        archer_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(0f, 0f, 0f, 1f));

        spawn_1SoldierIcon.style.display = DisplayStyle.Flex;
        spawn_2SoldierIcon.style.display = DisplayStyle.Flex;
        spawn_3SoldierIcon.style.display = DisplayStyle.Flex;
        spawn_4SoldierIcon.style.display = DisplayStyle.Flex;
        spawn_5SoldierIcon.style.display = DisplayStyle.Flex;
        spawn_6SoldierIcon.style.display = DisplayStyle.Flex;

        spawn_1ArcherIcon.style.display = DisplayStyle.None;
        spawn_2ArcherIcon.style.display = DisplayStyle.None;
        spawn_3ArcherIcon.style.display = DisplayStyle.None;
        spawn_4ArcherIcon.style.display = DisplayStyle.None;
        spawn_5ArcherIcon.style.display = DisplayStyle.None;
        spawn_6ArcherIcon.style.display = DisplayStyle.None;
        Invoke("ClickUnitImage_DelayTime", 0.02f);

        swordMasterIcon.style.display = DisplayStyle.None;
        sorceressIcon.style.display = DisplayStyle.None;
        sorceressIcon_Lock.style.display = DisplayStyle.None;
        priestIcon.style.display = DisplayStyle.None;
        priestIcon_Lock.style.display = DisplayStyle.None;
    }
    private void OnSoldierSpawnImageMouseHover() {
        soldier_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(1f, 1f, 1f, 1f));
    }
    private void OnSoldierSpawnImageMouseOut() {
        if (soldier_SpawnImgClick == false) {
            soldier_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(0f, 0f, 0f, 1f));
        }
    }

    private void OnArcherSpawnImageClick() // �ü� ���� �̹����� Ŭ������ ��
    {
        ClickUnitImage = true; // �̹��� Ŭ�� ���� true

        archer_SpawnImgClick = true;
        soldier_SpawnImgClick = false;

        archer_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(1f, 1f, 1f, 1f)); // �ü� ���� �� ����
        soldier_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(0f, 0f, 0f, 1f));

        spawn_1ArcherIcon.style.display = DisplayStyle.Flex;
        spawn_2ArcherIcon.style.display = DisplayStyle.Flex;
        spawn_3ArcherIcon.style.display = DisplayStyle.Flex;
        spawn_4ArcherIcon.style.display = DisplayStyle.Flex;
        spawn_5ArcherIcon.style.display = DisplayStyle.Flex;
        spawn_6ArcherIcon.style.display = DisplayStyle.Flex;

        spawn_1SoldierIcon.style.display = DisplayStyle.None;
        spawn_2SoldierIcon.style.display = DisplayStyle.None;
        spawn_3SoldierIcon.style.display = DisplayStyle.None;
        spawn_4SoldierIcon.style.display = DisplayStyle.None;
        spawn_5SoldierIcon.style.display = DisplayStyle.None;
        spawn_6SoldierIcon.style.display = DisplayStyle.None;

        Invoke("ClickUnitImage_DelayTime", 0.02f);

        swordMasterIcon.style.display = DisplayStyle.None;
        sorceressIcon.style.display = DisplayStyle.None;
        sorceressIcon_Lock.style.display = DisplayStyle.None;
        priestIcon.style.display = DisplayStyle.None;
        priestIcon_Lock.style.display = DisplayStyle.None;
    }
    private void OnArcherSpawnImageMouseHover() {
        archer_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(1f, 1f, 1f, 1f));
    }
    private void OnArcherSpawnImageMouseOut() {
        if (archer_SpawnImgClick == false) {
            archer_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(0f, 0f, 0f, 1f));
        }
    }

    public void SpawnImageMouseOut() // ���õ� �˻� ���� �̹����� �ʱ�ȭ ��Ŵ (���� ����â ��Ȱ��ȭ�� ���)
    {
        soldier_SpawnImgClick = false;
        archer_SpawnImgClick = false;
        soldier_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(0f, 0f, 0f, 1f));
        archer_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(0f, 0f, 0f, 1f));
    }

    private void OnSoldierSpawnIconImageClick(int spawnCnt) // �˻� ���� ���� �������� Ŭ������ �� 
    {
        ClickUnitImage = true; // �̹��� Ŭ�� ���� true
        if (loadingBar.spawnSoldier == false && loadingBar.spawnArcher == false) // ���� �������� �ƴ� ��
        {
            loadingBar.spawnSoldier = true; // ���� ������ �˻�� ����
        }
        loadingBar.StartLoading_SpawnSoldier(spawnCnt); // �˻� ���� �ε�
        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }

    private void OnArcherSpawnIconImageClick(int spawnCnt) // �ü� ���� ���� �������� Ŭ������ �� 
    {
        ClickUnitImage = true; // �̹��� Ŭ�� ���� true
        if (loadingBar.spawnSoldier == false && loadingBar.spawnArcher == false) // ���� �������� �ƴ� ��
        {
            loadingBar.spawnArcher = true; // ���� ������ �ü��� ����
        }
        loadingBar.StartLoading_SpawnArcher(spawnCnt); // �ü� ���� �ε�
        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }

    public void SwordMasterWindow(bool active, int currentLv, int currentExp, int maxExp, int currentHP, int maxHP, int currentMP, int maxMP, int attackPower, float speed) // �ҵ帶���� ���� ����
    { // �ҵ帶���� ���� ���� (�������� unitInfo.cs, SwordStats.cs, MouseClick.cs, UnitController.cs)
        swordMaster_CurrentLv = currentLv;
        swordMaster_CurrentExp = currentExp;
        swordMaster_MaxExp = maxExp;
        swordMaster_CurrentHP = currentHP;
        swordMaster_MaxHP = maxHP;
        swordMaster_CurrentMP = currentMP;
        swordMaster_MaxMP = maxMP;
        swordMaster_AttackPower = attackPower;
        swordMaster_Speed = speed;

        ClickUnitImage = true; // �̹��� Ŭ�� ���� true
        if (active == true) {
            swordMaster_Window.style.display = DisplayStyle.Flex;
            swordMaster_info2.style.display = DisplayStyle.Flex;

            soldier_Window.style.display = DisplayStyle.None;
            soldier_info2.style.display = DisplayStyle.None;
            soldier_SpawnWindow.style.display = DisplayStyle.None;

            archer_Window.style.display = DisplayStyle.None;
            archer_info2.style.display = DisplayStyle.None;
            archer_SpawnWindow.style.display = DisplayStyle.None;

            sorceress_Window.style.display = DisplayStyle.None;
            sorceress_info2.style.display = DisplayStyle.None;

            priest_Window.style.display = DisplayStyle.None;
            priest_info2.style.display = DisplayStyle.None;

            enemyInfo.DeselectEnemy_Alien();

            string curLv = "Level: " + currentLv;
            sMa_CurLv.text = curLv;

            string curExp = "Exp: " + currentExp + " / " + maxExp;
            sMa_Exp.text = curExp;

            string curHP = "HP: " + currentHP + " / " + maxHP;
            sMa_CurHP.text = curHP;

            string curMP = "MP: " + currentMP + " / " + maxMP;
            sMa_CurMP.text = curMP;

            string attPW = "Attack Power: " + attackPower;
            sMa_AttPW.text = attPW;

            string curSpeed = "Speed: " + 10 * speed;
            sMa_Speed.text = curSpeed;
        }
        else {
            swordMaster_Window.style.display = DisplayStyle.None;
            swordMaster_info2.style.display = DisplayStyle.None;
        }
        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }

    public void SorceressWindow(bool active, int currentLv, int currentExp, int maxExp, int currentHP, int maxHP, int currentMP, int maxMP, int attackPower, float speed) { // �Ҽ����� ���� ���� (�������� unitInfo.cs, SorceressStats.cs, MouseClick.cs, UnitController.cs)
        sorceress_CurrentLv = currentLv;
        sorceress_CurrentExp = currentExp;
        sorceress_MaxExp = maxExp;
        sorceress_CurrentHP = currentHP;
        sorceress_MaxHP = maxHP;
        sorceress_CurrentMP = currentMP;
        sorceress_MaxMP = maxMP;
        sorceress_AttackPower = attackPower;
        sorceress_Speed = speed;

        ClickUnitImage = true; // �̹��� Ŭ�� ���� true
        if (active == true) {
            sorceress_Window.style.display = DisplayStyle.Flex;
            sorceress_info2.style.display = DisplayStyle.Flex;

            soldier_Window.style.display = DisplayStyle.None;
            soldier_info2.style.display = DisplayStyle.None;
            soldier_SpawnWindow.style.display = DisplayStyle.None;

            archer_Window.style.display = DisplayStyle.None;
            archer_info2.style.display = DisplayStyle.None;
            archer_SpawnWindow.style.display = DisplayStyle.None;

            swordMaster_Window.style.display = DisplayStyle.None;
            swordMaster_info2.style.display = DisplayStyle.None;

            priest_Window.style.display = DisplayStyle.None;
            priest_info2.style.display = DisplayStyle.None;

            enemyInfo.DeselectEnemy_Alien();

            string curLv = "Level: " + currentLv;
            scr_CurLv.text = curLv;

            string curExp = "Exp: " + currentExp + " / " + maxExp;
            scr_Exp.text = curExp;

            string curHP = "HP: " + currentHP + " / " + maxHP;
            scr_CurHP.text = curHP;

            string curMP = "MP: " + currentMP + " / " + maxMP;
            scr_CurMP.text = curMP;

            string attPW = "Attack Power: " + attackPower;
            scr_AttPW.text = attPW;

            string curSpeed = "Speed: " + 10 * speed;
            scr_Speed.text = curSpeed;
        }
        else {
            sorceress_Window.style.display = DisplayStyle.None;
            sorceress_info2.style.display = DisplayStyle.None;
        }
        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }

    public void PriestWindow(bool active, int currentLv, int currentExp, int maxExp, int currentHP, int maxHP, int currentMP, int maxMP, int attackPower, float speed) // ������Ʈ ���� ����
    { // ������Ʈ ���� ���� (�������� unitInfo.cs, PreistStats.cs, MouseClick.cs, UnitController.cs)
        priest_CurrentLv = currentLv;
        priest_CurrentExp = currentExp;
        priest_MaxExp = maxExp;
        priest_CurrentHP = currentHP;
        priest_MaxHP = maxHP;
        priest_CurrentMP = currentMP;
        priest_MaxMP = maxMP;
        priest_AttackPower = attackPower;
        priest_Speed = speed;

        ClickUnitImage = true; // �̹��� Ŭ�� ���� true
        if (active == true) {
            priest_Window.style.display = DisplayStyle.Flex;
            priest_info2.style.display = DisplayStyle.Flex;

            soldier_Window.style.display = DisplayStyle.None;
            soldier_info2.style.display = DisplayStyle.None;
            soldier_SpawnWindow.style.display = DisplayStyle.None;

            archer_Window.style.display = DisplayStyle.None;
            archer_info2.style.display = DisplayStyle.None;
            archer_SpawnWindow.style.display = DisplayStyle.None;

            swordMaster_Window.style.display = DisplayStyle.None;
            swordMaster_info2.style.display = DisplayStyle.None;

            sorceress_Window.style.display = DisplayStyle.None;
            sorceress_info2.style.display = DisplayStyle.None;

            enemyInfo.DeselectEnemy_Alien();

            string curLv = "Level: " + currentLv;
            pri_CurLv.text = curLv;

            string curExp = "Exp: " + currentExp + " / " + maxExp;
            pri_Exp.text = curExp;

            string curHP = "HP: " + currentHP + " / " + maxHP;
            pri_CurHP.text = curHP;

            string curMP = "MP: " + currentMP + " / " + maxMP;
            pri_CurMP.text = curMP;

            string attPW = "Attack Power: " + attackPower;
            pri_AttPW.text = attPW;

            string curSpeed = "Speed: " + 10 * speed;
            pri_Speed.text = curSpeed;

        }
        else {
            priest_Window.style.display = DisplayStyle.None;
            priest_info2.style.display = DisplayStyle.None;
        }
        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }

    public void OnSwordMasterImageClick() {
        ClickUnitImage = true;

        SwordStats swordStats = FindObjectOfType<SwordStats>();
        if (swordStats != null) {
            GameObject focusOnSwordMaster = swordStats.gameObject;
            // Master Object�� ��ġ�� ������
            Vector3 swordMasterPosition = focusOnSwordMaster.transform.position;

            // ���� ī�޶��� ��ġ�� ���� (y �������� 70���� ����)
            Camera.main.transform.position = new Vector3(swordMasterPosition.x, 70f, swordMasterPosition.z - 40);

            screenController.ZoomCamera(20);

            Invoke("ClickUnitImage_DelayTime", 0.02f);
        }
    }

    public void OnSorceressImageClick() {
        ClickUnitImage = true;

        SorceressStats sorceressStats = FindObjectOfType<SorceressStats>();

        if (sorceressStats != null) {
            GameObject focusOnSwordMaster = sorceressStats.gameObject;
            // Master Object�� ��ġ�� ������
            Vector3 swordMasterPosition = focusOnSwordMaster.transform.position;

            // ���� ī�޶��� ��ġ�� ���� (y �������� 70���� ����)
            Camera.main.transform.position = new Vector3(swordMasterPosition.x, 70f, swordMasterPosition.z - 40);

            screenController.ZoomCamera(20);

            Invoke("ClickUnitImage_DelayTime", 0.02f);
        }
    }

    public void OnPriestImageClick() {
        ClickUnitImage = true;

        PriestStats priestStats = FindObjectOfType<PriestStats>();

        if (priestStats != null) {
            GameObject focusOnSwordMaster = priestStats.gameObject;
            // Master Object�� ��ġ�� ������
            Vector3 swordMasterPosition = focusOnSwordMaster.transform.position;

            // ���� ī�޶��� ��ġ�� ���� (y �������� 70���� ����)
            Camera.main.transform.position = new Vector3(swordMasterPosition.x, 70f, swordMasterPosition.z - 40);

            screenController.ZoomCamera(20);

            Invoke("ClickUnitImage_DelayTime", 0.02f);
        }
    }
}