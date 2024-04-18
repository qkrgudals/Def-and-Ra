using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI coinText; // UI�� ǥ�õ� ���� �� �ؽ�Ʈ
    public TextMeshProUGUI stageText;
    public int totalCoins = 300; // ��ü ���� ��

    public string regionName;
    public string reg;
    bool clear;
    UnitInfo unitInfo;
    Portal portal;
    VisualElement garrisonButton;
    VisualElement garrisonButton_Lock;
    VisualElement dungeonButton;
    VisualElement dungeonButton_Lock;
    VisualElement colosseumButton;
    VisualElement colosseumButton_Lock;
    VisualElement floatingBuilding;
    VisualElement floatingBuilding_Lock;

    VisualElement dungeonButton2;
    VisualElement dungeonButton_Lock2;
    VisualElement colosseumButton2;
    VisualElement colosseumButton_Lock2;
    VisualElement floatingBuilding2;
    VisualElement floatingBuilding_Lock2;

     Portal portalScript;
    EnemyManager enemyManager;
    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        GameObject portalObject = GameObject.Find("Sphere");
        portalScript = portalObject.GetComponent<Portal>();
        var root = FindObjectOfType<UIDocument>().rootVisualElement;
        unitInfo = FindObjectOfType<UnitInfo>();
        portal = FindObjectOfType<Portal>();
        regionName = "world";
        reg = "world";

   
       UpdateCoinText();

      

        garrisonButton = root.Q<VisualElement>("GarrisonButton");
        garrisonButton_Lock = root.Q<VisualElement>("GarrisonButton_Lock");
        dungeonButton = root.Q<VisualElement>("DungeonButton");
        dungeonButton_Lock = root.Q<VisualElement>("DungeonButton_Lock");
        colosseumButton = root.Q<VisualElement>("ColosseumButton");
        colosseumButton_Lock = root.Q<VisualElement>("ColosseumButton_Lock");
        floatingBuilding = root.Q<VisualElement>("FloatingBuilding");
        floatingBuilding_Lock = root.Q<VisualElement>("FloatingBuilding_Lock");

        dungeonButton2 = root.Q<VisualElement>("DungeonButton2");
        dungeonButton_Lock2 = root.Q<VisualElement>("DungeonButton_Lock2");
        colosseumButton2 = root.Q<VisualElement>("ColosseumButton2");
        colosseumButton_Lock2 = root.Q<VisualElement>("ColosseumButton_Lock2");
        floatingBuilding2 = root.Q<VisualElement>("FloatingBuilding2");
        floatingBuilding_Lock2 = root.Q<VisualElement>("FloatingBuilding_Lock2");

        garrisonButton.RegisterCallback<MouseDownEvent>(evt => SceneMove(1));
        dungeonButton.RegisterCallback<MouseDownEvent>(evt => SceneMove(2));
        colosseumButton.RegisterCallback<MouseDownEvent>(evt => SceneMove(3));
        floatingBuilding.RegisterCallback<MouseDownEvent>(evt => SceneMove(4));

        dungeonButton2.RegisterCallback<MouseDownEvent>(evt => HeroMove("raid1"));
        colosseumButton2.RegisterCallback<MouseDownEvent>(evt => HeroMove("raid2"));
        floatingBuilding2.RegisterCallback<MouseDownEvent>(evt => HeroMove("raid3"));
    }

    // ���� �׿��� �� ȣ��� �Լ�
    public void EnemyKilled(int enemyCoins)
    {
        totalCoins += enemyCoins;
        //Debug.Log(totalCoins);
        UpdateCoinText();
    }

    // ���縦 ���� �� ȣ��� �Լ�
    public void RecruitSoldier(int costPerSoldier)
    {


        // ����� ������ �ִ��� Ȯ��
        if (totalCoins >= costPerSoldier)
        {
            totalCoins -= costPerSoldier;
            //Debug.Log(totalCoins);
            UpdateCoinText();
        }
        else
        {
            Debug.Log("������ �����Ͽ� ���縦 ���� �� �����ϴ�. ���� COIN: " + totalCoins);
        }
    }

    // UI�� ���� �� ������Ʈ �Լ�
    void UpdateCoinText()
    {
        coinText.text = "COIN: " + totalCoins;
    }

    private void Update()
    {
        clear = true;
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        //if (enemyManager != null) {
        foreach (GameObject obj in allObjects) {
            if (obj.name.Contains("4_����")) {
                clear = false;
            }
            if (!clear) {
                break;
            }
        }
        if(clear) {
            SceneManager.LoadScene("VICTORY");
        }
            stageText.text = "Stage: " + enemyManager.currentStage;

        switch (enemyManager.currentStage) // �������� ��� ���� ����
        {
            case 2: dungeonButton_Lock.style.display = DisplayStyle.None; dungeonButton.style.display = DisplayStyle.Flex; break; // �������� 2
            case 3: colosseumButton_Lock.style.display = DisplayStyle.None; colosseumButton.style.display = DisplayStyle.Flex; break; // �������� 3 
            case 4: floatingBuilding_Lock.style.display = DisplayStyle.None; floatingBuilding.style.display = DisplayStyle.Flex; break; // �������� 4
        }

        switch (regionName) // Ư�� �������� �̵��� �ش�Ǵ� ��ư ��� (�ӽ� �ּ�ó��)
        {
            case "World": garrisonButton_Lock.style.display = DisplayStyle.Flex; garrisonButton.style.display = DisplayStyle.None; break;
                /*
            case "raid1": dungeonButton_Lock.style.display = DisplayStyle.Flex; dungeonButton.style.display = DisplayStyle.None; break;
            case "raid2": colosseumButton_Lock.style.display = DisplayStyle.Flex; colosseumButton.style.display = DisplayStyle.None; break;
            case "raid3": floatingBuilding_Lock.style.display = DisplayStyle.Flex; floatingBuilding.style.display = DisplayStyle.None; break;
                */

        }

        switch (enemyManager.currentStage) // �������� ��� ���� ����
        {
            case 2: dungeonButton_Lock2.style.display = DisplayStyle.None; dungeonButton2.style.display = DisplayStyle.Flex; break; // �������� 2
            case 3: colosseumButton_Lock2.style.display = DisplayStyle.None; colosseumButton2.style.display = DisplayStyle.Flex; break; // �������� 3 
            case 4: floatingBuilding_Lock2.style.display = DisplayStyle.None; floatingBuilding2.style.display = DisplayStyle.Flex; break; // �������� 4 
        }

        switch (SceneManager.GetActiveScene().name)
        {
            case "raid1": dungeonButton_Lock2.style.display = DisplayStyle.Flex; dungeonButton2.style.display = DisplayStyle.None; break;
            case "raid2": colosseumButton_Lock2.style.display = DisplayStyle.Flex; colosseumButton2.style.display = DisplayStyle.None; break;
            case "raid3": floatingBuilding_Lock2.style.display = DisplayStyle.Flex; floatingBuilding2.style.display = DisplayStyle.None; break;
        }

        if (portalScript.unitInfoDeactive == true) // ������ ��Ż�� �����ҽ� ���� ����â ��Ȱ��ȭ
        {
            unitInfo.soldier_Window.style.display = DisplayStyle.None;
            unitInfo.archer_Window.style.display = DisplayStyle.None;
            unitInfo.swordMaster_Window.style.display = DisplayStyle.None;
            unitInfo.swordMaster_info2.style.display = DisplayStyle.None;
            unitInfo.sorceress_Window.style.display = DisplayStyle.None;
            unitInfo.sorceress_info2.style.display = DisplayStyle.None;
            unitInfo.priest_Window.style.display = DisplayStyle.None;
            unitInfo.priest_info2.style.display = DisplayStyle.None;
            unitInfo.soldier_SpawnWindow.style.display = DisplayStyle.None;
            unitInfo.archer_SpawnWindow.style.display = DisplayStyle.None;
        }
    }

    public void SceneMove(int region)
    {
        switch (region)
        {
            case 1: regionName = "world"; Camera.main.transform.position = new Vector3(2f, 34f, -39f); Camera.main.transform.rotation = Quaternion.Euler(60f, 0f, 0); break;
            case 2: regionName = "raid1"; Camera.main.transform.position = new Vector3(3.6f, -981f, 28f); Camera.main.transform.rotation = Quaternion.Euler(40f, 180f, 0); break;
            case 3: regionName = "raid2"; Camera.main.transform.position = new Vector3(11872f, 0f, 148f); Camera.main.transform.rotation = Quaternion.Euler(40f, 90f, 0); break;
            case 4: regionName = "raid3";  Camera.main.transform.position = new Vector3(-19995.5f, 12f, 240f); Camera.main.transform.rotation = Quaternion.Euler(40f, 180f, 0); break;
        }
    }

    public void HeroMove(string playerTeleportPosition)
    {
        switch (playerTeleportPosition)
        {
            case "raid1": reg = "raid1"; regionName = "raid1"; Camera.main.transform.position = new Vector3(3.6f, -981f, 28f); Camera.main.transform.rotation = Quaternion.Euler(40f, 180f, 0); break;
            case "raid2": reg = "raid2"; regionName = "raid2"; Camera.main.transform.position = new Vector3(11872f, 0f, 148f); Camera.main.transform.rotation = Quaternion.Euler(40f, 90f, 0); break;
            case "raid3": reg = "raid3"; regionName = "raid3"; Camera.main.transform.position = new Vector3(-19995.5f, 12f, 240f); Camera.main.transform.rotation = Quaternion.Euler(40f, 180f, 0); break;
        }
    }

    public void SceneCall(string Name)
    {
        SceneManager.LoadScene(Name);
    }
}
