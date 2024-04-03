using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class UnitInfo : MonoBehaviour {
    int soldier_count; // 검사 선택수 체크
    int archer_count; // 궁수 선택수 체크

    VisualElement soldier_Window; // 검사 UI창
    Label soldier_info; // 검사의 정보 (선택된 검사 수)
    VisualElement soldier_Img; // 검사 유닛 이미지
    VisualElement soldier_info2; // 검사의 세부정보 (최대 HP, 현재 HP, 공격파워, 스피드)
    Label sol_MaxHP; // 검사 최대 HP 라벨
    Label sol_CurHP; // 검사 현재 HP 라벨
    int soldier_currentHP;
    Label sol_AttPW; // 검사 공격파워 라벨
    Label sol_Speed; // 검사 스피드 라벨
    Soldier_UnitController soldier_UnitCtrInfo; // 검사 유닛컨트롤러 정보를 찾기위한 변수 (검사 공격파워)
    VisualElement soldier_SpawnWindow;
    VisualElement soldier_SpawnImg;
    VisualElement spawn_1SoldierIcon;
    VisualElement spawn_2SoldierIcon;
    VisualElement spawn_3SoldierIcon;
    VisualElement spawn_4SoldierIcon;
    VisualElement spawn_5SoldierIcon;
    VisualElement spawn_6SoldierIcon;

    VisualElement archer_Window; // 궁수 UI창
    Label archer_info; // 궁수의 정보 (선택된 궁수의 수)
    VisualElement archer_Img; // 궁수 유닛 이미지
    VisualElement archer_info2; // 궁수의 세부정보 (최대 HP, 현재 HP, 공격파워, 스피드)
    Label arc_MaxHP; // 궁수 최대 HP 라벨
    Label arc_CurHP; // 궁수 현재 HP 라벨
    int archer_currentHP;
    Label arc_AttPW; // 궁수 공격파워 라벨
    Label arc_Speed; // 궁수 스피드 라벨
    Archer_UnitController archer_UnitCtrInfo; // 궁수 유닛컨트롤러 정보를 찾기위한 변수 (궁수 공격파워)
    VisualElement archer_SpawnWindow;
    VisualElement archer_SpawnImg;
    VisualElement spawn_1ArcherIcon;
    VisualElement spawn_2ArcherIcon;
    VisualElement spawn_3ArcherIcon;
    VisualElement spawn_4ArcherIcon;
    VisualElement spawn_5ArcherIcon;
    VisualElement spawn_6ArcherIcon;

    UnitHP_Soldier unitHPInfo_Soldier; // 검사 HP정보를 찾기위한 변수 (유닛 최대 HP, 현재 HP)
    UnitHP_Archer unitHPInfo_Archer; // 검사 HP정보를 찾기위한 변수 (유닛 최대 HP, 현재 HP)
    NavMeshAgent unitSpeedInfo; // NavMeshAgent의 스피드 정보를 찾기 위한 변수 (유닛 스피드)
    Health health;

    VisualElement swordMasterIcon;
    public VisualElement swordMaster_Window;
    VisualElement swordMaster_Img;
    public VisualElement swordMaster_info2;
    Label sMa_CurLv;
    int swordMaster_CurrentLv; // 소드마스터 현재 Lv 라벨
    Label sMa_Exp;
    int swordMaster_CurrentExp; // 소드마스터 현재 경험치 라벨
    int swordMaster_MaxExp;
    Label sMa_CurHP; // 소드마스터 현재 HP 라벨
    int swordMaster_CurrentHP;
    int swordMaster_MaxHP;
    Label sMa_CurMP; // 소드마스터 현재 MP 라벨
    int swordMaster_CurrentMP;
    int swordMaster_MaxMP;
    Label sMa_AttPW; // 소드마스터 공격파워 라벨
    int swordMaster_AttackPower;
    Label sMa_Speed; // 소드마스터 스피드 라벨
    float swordMaster_Speed;

    VisualElement sorceressIcon;
    VisualElement sorceressIcon_Lock;
    public VisualElement sorceress_Window;
    VisualElement sorceress_Img;
    public VisualElement sorceress_info2;
    Label scr_CurLv; //소서리스 현재 Lv 라벨
    int sorceress_CurrentLv;
    Label scr_Exp; // 소서리스 현재 경험치 라벨
    int sorceress_CurrentExp;
    int sorceress_MaxExp;
    Label scr_CurHP; // 소서리스 현재 HP 라벨
    int sorceress_CurrentHP;
    int sorceress_MaxHP;
    Label scr_CurMP; // 소서리스 현재 MP 라벨
    int sorceress_CurrentMP;
    int sorceress_MaxMP;
    Label scr_AttPW; // 소서리스 공격파워 라벨
    int sorceress_AttackPower;
    Label scr_Speed; // 소서리스 스피드 라벨
    float sorceress_Speed;

    VisualElement priestIcon;
    VisualElement priestIcon_Lock;
    public VisualElement priest_Window;
    VisualElement priest_Img;
    public VisualElement priest_info2;
    Label pri_CurLv; // 프리스트 현재 Lv 라벨
    int priest_CurrentLv;
    Label pri_Exp; // 프리스트 현재 경험치 라벨
    int priest_CurrentExp;
    int priest_MaxExp;
    Label pri_CurHP; // 프리스트 현재 HP 라벨
    int priest_CurrentHP;
    int priest_MaxHP;
    Label pri_CurMP; // 프리스트 현재 MP 라벨
    int priest_CurrentMP;
    int priest_MaxMP;
    Label pri_AttPW; // 프리스트 공격파워 라벨
    int priest_AttackPower;
    Label pri_Speed; // 프리스트 스피드 라벨
    float priest_Speed;


    bool allowDisplaySoldierInfo = true; // 검사의 세부 정보를 디스플레이상에 나타내는걸 수락할지 결정하는 불리언 값
    bool allowDisplayArcherInfo = true;

    private RTSUnitController rtsUnitController;
    private EnemyInfo enemyInfo;

    public bool ClickUnitImage = false; // 유닛 이미지 클릭 상태

    bool soldier_SpawnImgClick = false; // 검사 스폰 이미지 클릭상태 체크
    bool archer_SpawnImgClick = false; // 궁수 스폰 이미지 클릭상태 체크

    public float focusCameraSpeed = 10f;

    ScreenController screenController;

    UnitSpawnLoadingBar loadingBar;

    EnemyManager enemyManager;


    void Awake() {
        var root = GetComponent<UIDocument>().rootVisualElement; //UIDocment 컴포넌트를 찾음
        soldier_Window = root.Q<VisualElement>("Soldier_Window"); //Info 라벨의 상위 계층인 VisualElement 오브젝트를 찾음
        soldier_info = root.Q<Label>("Soldier_Info"); // info 라벨을 찾아 정보를 참조
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

        soldier_Img.RegisterCallback<MouseDownEvent>(evt => OnSoldierImageClick()); // 검사 유닛 이미지를 클릭했을 때 
        archer_Img.RegisterCallback<MouseDownEvent>(evt => OnArcherImageClick());

        soldier_SpawnImg.RegisterCallback<MouseDownEvent>(evt => OnSoldierSpawnImageClick()); // 검사 스폰 이미지 클릭
        soldier_SpawnImg.RegisterCallback<MouseEnterEvent>(evt => OnSoldierSpawnImageMouseHover());
        soldier_SpawnImg.RegisterCallback<MouseOutEvent>(evt => OnSoldierSpawnImageMouseOut());

        archer_SpawnImg.RegisterCallback<MouseDownEvent>(evt => OnArcherSpawnImageClick()); // 궁수 스폰 이미지 클릭
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
            case 0: sorceressIcon_Lock.style.display = DisplayStyle.Flex; priestIcon_Lock.style.display = DisplayStyle.Flex; break; // 스테이지 1
            case <= 1: sorceressIcon_Lock.style.display = DisplayStyle.None; sorceressIcon.style.display = DisplayStyle.Flex; break; // 스테이지 2 이상
            case <= 3: priestIcon_Lock.style.display = DisplayStyle.None; priestIcon.style.display = DisplayStyle.Flex; break; // 스테이지 4 
        }
        */
    }

    public void ShowSelectionInfo_Soldier(int count) // 검사 선택 정보
    {
        soldier_count = count; // 검사 선택수 정보를 담음
        if (count > 0) {
            allowDisplayArcherInfo = false; // 궁수 정보표시 나타내기 잠금
            soldier_Window.style.display = DisplayStyle.Flex; // 검사 정보창을 디스플레이상에서 나타냄

            archer_info2.style.display = DisplayStyle.None;
            enemyInfo.DeselectEnemy_Alien(); // 적 에일리언 정보 숨기기

            // 검사의 선택된 갯수를 문자열에 포함하여 생성
            string infoText = "Soldier: " + count;
            // UI Toolkit의 라벨에 동적으로 생성된 문자열 출력 (선택 검사 수)
            soldier_info.text = infoText;

            if (allowDisplaySoldierInfo == true) {
                ShowStatusInfo_Soldier(soldier_currentHP); // 검사 상태정보 표시
            }
        }
        else {
            allowDisplayArcherInfo = true; // 궁수 정보표시 나타내기 수락
            soldier_Window.style.display = DisplayStyle.None;
            soldier_info2.style.display = DisplayStyle.None;
        }
        soldier_SpawnWindow.style.display = DisplayStyle.None; // 검사 생성창을 닫음
        archer_SpawnWindow.style.display = DisplayStyle.None; // 궁수 생성창을 닫음

        swordMaster_Window.style.display = DisplayStyle.None;
        swordMaster_info2.style.display = DisplayStyle.None;
    }

    public void ShowSelectionInfo_Archer(int count) // 궁수 선택 정보
    {
        archer_count = count; // 궁수 선택수 정보를 담음

        if (count > 0) {
            allowDisplaySoldierInfo = false; // 검사 정보표시 나타내기 잠금
            archer_Window.style.display = DisplayStyle.Flex; // 궁수 정보창을 디스플레이상에서 나타냄

            soldier_info2.style.display = DisplayStyle.None;
            enemyInfo.DeselectEnemy_Alien(); // 적 에일리언 정보 숨기기

            // 궁수의 선택된 갯수를 문자열에 포함하여 생성
            string infoText = "Archer: " + count;

            archer_info.text = infoText;

            if (allowDisplayArcherInfo == true) {
                ShowStatusInfo_Archer(archer_currentHP); // 궁수 상태정보 표시
            }
        }
        else {
            allowDisplaySoldierInfo = true; // 검사 정보표시 나타내기 수락
            archer_Window.style.display = DisplayStyle.None;
            archer_info2.style.display = DisplayStyle.None;
        }
        soldier_SpawnWindow.style.display = DisplayStyle.None; // 검사 생성창을 닫음
        archer_SpawnWindow.style.display = DisplayStyle.None; // 궁수 생성창을 닫음

        swordMaster_Window.style.display = DisplayStyle.None;
        swordMaster_info2.style.display = DisplayStyle.None;
    }
    private void OnSoldierImageClick() // 검사 유닛 이미지를 클릭했을 때
    {
        ClickUnitImage = true; // 이미지 클릭 상태 true
        rtsUnitController.DeselectAll2(); // 궁수 유닛 전부 선택 해제

        ShowStatusInfo_Soldier(soldier_currentHP);

        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }

    private void OnArcherImageClick() // 궁수 유닛 이미지를 클릭했을 때
    {
        ClickUnitImage = true; // 이미지 클릭 상태 true
        rtsUnitController.DeselectAll(); // 검사 유닛 전부 선택 해제

        ShowStatusInfo_Archer(archer_currentHP);

        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }

    public void ShowStatusInfo_Soldier(int currentHP) // 검사 상태정보
    {
        soldier_currentHP = currentHP;

        if (soldier_count == 1) // 선택한 검사가 한명일 때만
        {
            sol_CurHP.style.display = DisplayStyle.Flex; // 현재 HP 정보 보여주기
        }
        else {
            sol_CurHP.style.display = DisplayStyle.None;
        }

        soldier_info2.style.display = DisplayStyle.Flex; // 유닛 세부정보를 디스플레이상에서 나타냄 (soldier_info2)

        string maxHPText = "Max HP: " + unitHPInfo_Soldier.MaxHP; // (검사 최대 HP)
        sol_MaxHP.text = maxHPText;

        string curHPText = "Current HP: " + currentHP; // (검사 현재 HP)
        sol_CurHP.text = curHPText;

        string attPWText = "Attack Power: " + soldier_UnitCtrInfo.damageAmount; // (검사 공격력)
        sol_AttPW.text = attPWText;

        string speedText = "Speed: " + unitSpeedInfo.speed * 10; // 원래 스피드에 10을 곱함 // (검사 스피드)
        sol_Speed.text = speedText;

        archer_Window.style.display = DisplayStyle.None; // 궁수 창을 닫음
        soldier_SpawnWindow.style.display = DisplayStyle.None; // 검사 생성창을 닫음
        archer_SpawnWindow.style.display = DisplayStyle.None; // 궁수 생성창을 닫음
    }
    public void CurrentHP_Soldier(int currentHP) {
        string curHPText = "Current HP: " + currentHP;
        sol_CurHP.text = curHPText;
    }

    public void ShowStatusInfo_Archer(int currentHP) // 궁수 상태정보
    {
        archer_currentHP = currentHP;
        if (archer_count == 1) // 선택한 궁수가 한명일 때만
        {
            arc_CurHP.style.display = DisplayStyle.Flex; // 현재 HP 정보 보여주기
        }
        else {
            arc_CurHP.style.display = DisplayStyle.None;
        }

        archer_info2.style.display = DisplayStyle.Flex; // 유닛 세부정보를 디스플레이상에서 나타냄 (soldier_info2)

        string maxHPText = "Max HP: " + unitHPInfo_Archer.MaxHP;
        arc_MaxHP.text = maxHPText;

        string curHPText = "Current HP: " + currentHP;
        arc_CurHP.text = curHPText;

        string attPWText = "Attack Power: " + archer_UnitCtrInfo.damageAmount;
        arc_AttPW.text = attPWText;

        string speedText = "Speed: " + unitSpeedInfo.speed * 10; // 원래 스피드에 10을 곱함
        arc_Speed.text = speedText;

        soldier_Window.style.display = DisplayStyle.None; // 검사 창을 닫음
        soldier_SpawnWindow.style.display = DisplayStyle.None;
        archer_SpawnWindow.style.display = DisplayStyle.None;

        swordMaster_Window.style.display = DisplayStyle.None;
        swordMaster_info2.style.display = DisplayStyle.None;
    }
    public void CurrentHP_Archer(int currentHP) {
        string curHPText = "Current HP: " + currentHP;
        arc_CurHP.text = curHPText;
    }

    private void ClickUnitImage_DelayTime() // 0.02초 뒤 유닛 이미지 클릭상태 종료
    {
        ClickUnitImage = false;
    }

    public void UnitSponerWindow(bool active) // 유닛 생성창
    {
        if (active == true) // 유닛 생성창 활성화
        {
            ClickUnitImage = true; // 이미지 클릭 상태 true

            soldier_SpawnWindow.style.display = DisplayStyle.Flex; // 검사 생성창을 디스플레이상에서 나타냄
            soldier_Window.style.display = DisplayStyle.None;
            soldier_info2.style.display = DisplayStyle.None;

            archer_SpawnWindow.style.display = DisplayStyle.Flex; // 궁수 생성창을 디스플레이상에서 나타냄
            archer_Window.style.display = DisplayStyle.None;
            archer_info2.style.display = DisplayStyle.None;

            swordMaster_Window.style.display = DisplayStyle.None;
            swordMaster_info2.style.display = DisplayStyle.None;

            sorceress_Window.style.display = DisplayStyle.None;
            sorceress_info2.style.display = DisplayStyle.None;

            priest_Window.style.display = DisplayStyle.None;
            priest_info2.style.display = DisplayStyle.None;

            enemyInfo.DeselectEnemy_Alien(); // 적 에일리언 정보 숨기기

            Invoke("ClickUnitImage_DelayTime", 0.02f);
        }
        else // 유닛 생성창 비활성화
        {
            DeselectUnitHouse();
        }
    }

    public void DeselectUnitHouse() // 유닛 생성소 선택 해제시
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

        switch (enemyManager.currentStage) // 스테이지별 영웅 잠금 조건
        {
            case 0: sorceressIcon_Lock.style.display = DisplayStyle.Flex; priestIcon_Lock.style.display = DisplayStyle.Flex; break; // 스테이지 1
            case <=1 : sorceressIcon_Lock.style.display = DisplayStyle.None; sorceressIcon.style.display = DisplayStyle.Flex; break; // 스테이지 2 이상
            case <=3: priestIcon_Lock.style.display = DisplayStyle.None; priestIcon.style.display = DisplayStyle.Flex; break; // 스테이지 4 
        }
    }

    private void OnSoldierSpawnImageClick() // 검사 생성 이미지를 클릭했을 때
    {
        ClickUnitImage = true; // 이미지 클릭 상태 true

        soldier_SpawnImgClick = true;
        archer_SpawnImgClick = false;

        soldier_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(1f, 1f, 1f, 1f)); // 검사 생성 색 변경
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

    private void OnArcherSpawnImageClick() // 궁수 생성 이미지를 클릭했을 때
    {
        ClickUnitImage = true; // 이미지 클릭 상태 true

        archer_SpawnImgClick = true;
        soldier_SpawnImgClick = false;

        archer_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(1f, 1f, 1f, 1f)); // 궁수 생성 색 변경
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

    public void SpawnImageMouseOut() // 선택된 검사 생성 이미지를 초기화 시킴 (유닛 생성창 비활성화시 사용)
    {
        soldier_SpawnImgClick = false;
        archer_SpawnImgClick = false;
        soldier_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(0f, 0f, 0f, 1f));
        archer_SpawnImg.style.unityBackgroundImageTintColor = new StyleColor(new Color(0f, 0f, 0f, 1f));
    }

    private void OnSoldierSpawnIconImageClick(int spawnCnt) // 검사 생성 슬롯 아이콘을 클릭했을 때 
    {
        ClickUnitImage = true; // 이미지 클릭 상태 true
        if (loadingBar.spawnSoldier == false && loadingBar.spawnArcher == false) // 유닛 생성중이 아닐 때
        {
            loadingBar.spawnSoldier = true; // 유닛 생성시 검사로 생성
        }
        loadingBar.StartLoading_SpawnSoldier(spawnCnt); // 검사 생성 로딩
        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }

    private void OnArcherSpawnIconImageClick(int spawnCnt) // 궁수 생성 슬롯 아이콘을 클릭했을 때 
    {
        ClickUnitImage = true; // 이미지 클릭 상태 true
        if (loadingBar.spawnSoldier == false && loadingBar.spawnArcher == false) // 유닛 생성중이 아닐 때
        {
            loadingBar.spawnArcher = true; // 유닛 생성시 궁수로 생성
        }
        loadingBar.StartLoading_SpawnArcher(spawnCnt); // 궁수 생성 로딩
        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }

    public void SwordMasterWindow(bool active, int currentLv, int currentExp, int maxExp, int currentHP, int maxHP, int currentMP, int maxMP, int attackPower, float speed) // 소드마스터 선택 정보
    { // 소드마스터 선택 정보 (수정구간 unitInfo.cs, SwordStats.cs, MouseClick.cs, UnitController.cs)
        swordMaster_CurrentLv = currentLv;
        swordMaster_CurrentExp = currentExp;
        swordMaster_MaxExp = maxExp;
        swordMaster_CurrentHP = currentHP;
        swordMaster_MaxHP = maxHP;
        swordMaster_CurrentMP = currentMP;
        swordMaster_MaxMP = maxMP;
        swordMaster_AttackPower = attackPower;
        swordMaster_Speed = speed;

        ClickUnitImage = true; // 이미지 클릭 상태 true
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

    public void SorceressWindow(bool active, int currentLv, int currentExp, int maxExp, int currentHP, int maxHP, int currentMP, int maxMP, int attackPower, float speed) { // 소서리스 선택 정보 (수정구간 unitInfo.cs, SorceressStats.cs, MouseClick.cs, UnitController.cs)
        sorceress_CurrentLv = currentLv;
        sorceress_CurrentExp = currentExp;
        sorceress_MaxExp = maxExp;
        sorceress_CurrentHP = currentHP;
        sorceress_MaxHP = maxHP;
        sorceress_CurrentMP = currentMP;
        sorceress_MaxMP = maxMP;
        sorceress_AttackPower = attackPower;
        sorceress_Speed = speed;

        ClickUnitImage = true; // 이미지 클릭 상태 true
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

    public void PriestWindow(bool active, int currentLv, int currentExp, int maxExp, int currentHP, int maxHP, int currentMP, int maxMP, int attackPower, float speed) // 프리스트 선택 정보
    { // 프리스트 선택 정보 (수정구간 unitInfo.cs, PreistStats.cs, MouseClick.cs, UnitController.cs)
        priest_CurrentLv = currentLv;
        priest_CurrentExp = currentExp;
        priest_MaxExp = maxExp;
        priest_CurrentHP = currentHP;
        priest_MaxHP = maxHP;
        priest_CurrentMP = currentMP;
        priest_MaxMP = maxMP;
        priest_AttackPower = attackPower;
        priest_Speed = speed;

        ClickUnitImage = true; // 이미지 클릭 상태 true
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
            // Master Object의 위치를 가져옴
            Vector3 swordMasterPosition = focusOnSwordMaster.transform.position;

            // 메인 카메라의 위치를 설정 (y 포지션은 70으로 고정)
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
            // Master Object의 위치를 가져옴
            Vector3 swordMasterPosition = focusOnSwordMaster.transform.position;

            // 메인 카메라의 위치를 설정 (y 포지션은 70으로 고정)
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
            // Master Object의 위치를 가져옴
            Vector3 swordMasterPosition = focusOnSwordMaster.transform.position;

            // 메인 카메라의 위치를 설정 (y 포지션은 70으로 고정)
            Camera.main.transform.position = new Vector3(swordMasterPosition.x, 70f, swordMasterPosition.z - 40);

            screenController.ZoomCamera(20);

            Invoke("ClickUnitImage_DelayTime", 0.02f);
        }
    }
}