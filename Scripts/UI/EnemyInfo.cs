using UnityEngine;
using UnityEngine.UIElements;

public class EnemyInfo : MonoBehaviour {
    VisualElement enemy_Alien_Window; // 에일리언(1-잡몹) UI창
    VisualElement enemy_Drake_Window; // 에일리언(1-정예) UI창
    VisualElement enemy_Clawzombie_Window; // 클로좀비(1-보스) UI창
    VisualElement enemy_Wolf_Window; // 울프(2-잡몹) UI창
    VisualElement enemy_Mutant_Window; // 뮤턴트(2-정예) UI창
    VisualElement enemy_Bestia_Window; // 베스티아(2-보스) UI창
    VisualElement enemy_GreenGoblin_Window; // 그린 고블린(3-잡몹) UI창
    VisualElement enemy_BlueGoblin_Window; // 블루 고블린(3-정예) UI창
    VisualElement enemy_KingGoblin_Window; // 킹고블린(3-보스) UI창
    VisualElement enemy_BlockGolem_Window; // 블록 골렘(4-잡몹) UI창
    VisualElement enemy_StoneGolem_Window; // 스톤 골렘(4-정예) UI창
    VisualElement enemy_GiantGolem_Window; // 자이언트 골렘(4-보스) UI창


    VisualElement soldier_SpawnWindow;

    Label enemy_Alien_info; // 에일리언의 정보 
    VisualElement enemy_Alien_Img; // 에일리언 이미지
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

    VisualElement enemy_Alien_info2; // 에일리언의 세부정보 (최대 HP, 현재 HP, 공격파워, 스피드)
    VisualElement archer_SpawnWindow;
    Label ail_MaxHP; // 에일리언 최대 HP 라벨
    Label ail_CurHP; // 에일리언 현재 HP 라벨
    Label ail_AttPW; // 에일리언 공격파워 라벨
    Label ail_Speed; // 에일리언 스피드 라벨
    Enemy enemy;
    private RTSUnitController rtsUnitController;
    private UnitInfo unitInfo;

    public bool ClickUnitImage = false; // 유닛 이미지 클릭 상태

    void Awake() {
        var root = GetComponent<UIDocument>().rootVisualElement; //UIDocment 컴포넌트를 찾음
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
    public void OnEnemy_AlienImageClick() // 에일리언 유닛 이미지를 클릭했을 때
    {
        ClickUnitImage = true; // 이미지 클릭 상태 true
                               // 마우스 클릭 위치에서 적 객체 검출
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject.CompareTag("Enemy")) {
                Enemy enemyClicked = hit.collider.GetComponent<Enemy>(); // 부딫힌 객체가 Enemy 컴포넌트를 가지고 있는지 확인
                
                if (enemyClicked != null) {
                    // 클릭한 적의 정보를 표시
                    ShowStatusInfo_Enemy(enemyClicked);
                }
                if (hit.collider.gameObject.name.Equals("1_잡몹(Clone)")) {
                    WindowHide();
                    enemy_Alien_Window.style.display = DisplayStyle.Flex; // 에일리언 정보 표시
                    enemy_Alien_info2.style.display = DisplayStyle.Flex; // 유닛 세부정보를 디스플레이상에서 나타냄 (soldier_info2)

                }
                else if (hit.collider.gameObject.name.Equals("1_정예(Clone)")) {
                    WindowHide();
                    enemy_Drake_Window.style.display = DisplayStyle.Flex; // 드레이크 정보 표시
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("1_보스(Clone)")) {
                    WindowHide();
                    enemy_Clawzombie_Window.style.display = DisplayStyle.Flex; // 클로 좀비 정보 표시
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("2_잡몹(Clone)")) {
                    WindowHide();
                    enemy_Wolf_Window.style.display = DisplayStyle.Flex; // 울프 정보 표시
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("2_정예(Clone)")) {
                    WindowHide();
                    enemy_Mutant_Window.style.display = DisplayStyle.Flex; // 뮤턴트 정보 표시
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("2_보스(Clone)")) {
                    WindowHide();
                    enemy_Bestia_Window.style.display = DisplayStyle.Flex; // 베스티아 정보 표시
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("3_잡몹(Clone)")) {
                    WindowHide();
                    enemy_GreenGoblin_Window.style.display = DisplayStyle.Flex; // 그린 고블린 정보 표시
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("3_정예(Clone)")) {
                    WindowHide();
                    enemy_BlueGoblin_Window.style.display = DisplayStyle.Flex; // 블루 고블린 정보 표시
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("3_보스(Clone)")) {
                    WindowHide();
                    enemy_KingGoblin_Window.style.display = DisplayStyle.Flex; // 킹고블린 정보 표시
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("4_잡몹(Clone)")) {
                    WindowHide();
                    enemy_BlockGolem_Window.style.display = DisplayStyle.Flex; // 블록 골렘 정보 표시
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("4_정예(Clone)")) {
                    WindowHide();
                    enemy_StoneGolem_Window.style.display = DisplayStyle.Flex; // 스톤 골렘 정보 표시
                    enemy_Alien_info2.style.display = DisplayStyle.Flex;
                }
                else if (hit.collider.gameObject.name.Equals("4_보스(Clone)")) {
                    WindowHide();
                    enemy_GiantGolem_Window.style.display = DisplayStyle.Flex; // 자이언트 골렘 정보 표시
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

        rtsUnitController.DeselectAll(); // 검사 유닛 전부 선택 해제
        rtsUnitController.DeselectAll2(); // 궁수 유닛 전부 선택 해제
        unitInfo.DeselectUnitHouse(); // 유닛 생성소 선택 해제
        
        Invoke("ClickUnitImage_DelayTime", 0.02f);
    }
    void ShowStatusInfo_Enemy(Enemy enemyClicked) // 적 상태정보
    {

        enemy_Alien_info2.style.display = DisplayStyle.Flex; // 유닛 세부정보를 디스플레이상에서 나타냄 (soldier_info2)

        string maxHPText = "Max HP: " + enemyClicked.hp;
        
        // UI Toolkit의 라벨에 동적으로 생성된 문자열 출력 (적 최대 HP)
        ail_MaxHP.text = maxHPText;

        string curHPText = "Current HP: " + enemyClicked.hp;
        //UI Toolkit의 라벨에 동적으로 생성된 문자열 출력 (적 현재 HP)
        ail_CurHP.text = curHPText;

        string attPWText = "Attack Power: " + enemyClicked.damage;
        // UI Toolkit의 라벨에 동적으로 생성된 문자열 출력 (적 공격력)
        ail_AttPW.text = attPWText;

        string speedText = "Speed: " + enemyClicked.moveSpeed * 10; // 원래 스피드에 10을 곱함
        // UI Toolkit의 라벨에 동적으로 생성된 문자열 출력 (적 스피드)
        ail_Speed.text = speedText;

        soldier_SpawnWindow.style.display = DisplayStyle.None; // 검사 생성창을 닫음
        archer_SpawnWindow.style.display = DisplayStyle.None; // 궁수 생성창을 닫음
    }
    public void DeselectEnemy_Alien() // 에일리언을 선택하지 않았을 때 
    {
        enemy_Alien_Window.style.display = DisplayStyle.None; // 에일리언 정보 숨기기
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

    private void ClickUnitImage_DelayTime() // 0.02초 뒤 유닛 이미지 클릭상태 종료
    {
        ClickUnitImage = false;
    }
}
