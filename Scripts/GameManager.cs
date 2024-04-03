using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI coinText; // UI�� ǥ�õ� ���� �� �ؽ�Ʈ
    public TextMeshProUGUI stageText;
    public int totalCoins = 300; // ��ü ���� ��
    //public int costPerSoldier = 10; // �� ����� ��� ���� ���
    private EnemyManager enemyManager;

    VisualElement garrisonButton;
    VisualElement garrisonButton_Lock;
    VisualElement dungeonButton;
    VisualElement dungeonButton_Lock;
    VisualElement colosseumButton;
    VisualElement colosseumButton_Lock;
    VisualElement floatingBuilding;
    VisualElement floatingBuilding_Lock;


    void Start() {
        var root = FindObjectOfType<UIDocument>().rootVisualElement;
        DontDestroyOnLoad(this.gameObject);
        UpdateCoinText();

        enemyManager = FindObjectOfType<EnemyManager>();

        garrisonButton = root.Q<VisualElement>("GarrisonButton");
        garrisonButton_Lock = root.Q<VisualElement>("GarrisonButton_Lock");
        dungeonButton = root.Q<VisualElement>("DungeonButton");
        dungeonButton_Lock = root.Q<VisualElement>("DungeonButton_Lock");
        colosseumButton = root.Q<VisualElement>("ColosseumButton");
        colosseumButton_Lock = root.Q<VisualElement>("ColosseumButton_Lock");
        floatingBuilding = root.Q<VisualElement>("FloatingBuilding");
        floatingBuilding_Lock = root.Q<VisualElement>("FloatingBuilding_Lock");
    }

    // ���� �׿��� �� ȣ��� �Լ�
    public void EnemyKilled(int enemyCoins) {
        totalCoins += enemyCoins;
        //Debug.Log(totalCoins);
        UpdateCoinText();
    }

    // ���縦 ���� �� ȣ��� �Լ�
    public void RecruitSoldier(int costPerSoldier) {
        

        // ����� ������ �ִ��� Ȯ��
        if (totalCoins >= costPerSoldier) {
            totalCoins -= costPerSoldier;
            //Debug.Log(totalCoins);
            UpdateCoinText();
        }
        else {
            Debug.Log("������ �����Ͽ� ���縦 ���� �� �����ϴ�. ���� COIN: " + totalCoins);
        }
    }

    // UI�� ���� �� ������Ʈ �Լ�
    void UpdateCoinText() {
        coinText.text = "COIN: " + totalCoins;
    }
    private void Update() {
        if (enemyManager != null) {
            stageText.text = "Stage: " + (enemyManager.currentStage+1);

            switch (enemyManager.currentStage) // �������� ��� ���� ����
            {
                case 1: dungeonButton_Lock.style.display = DisplayStyle.None; dungeonButton.style.display = DisplayStyle.Flex; break; // �������� 2
                case 2: colosseumButton_Lock.style.display = DisplayStyle.None; colosseumButton.style.display = DisplayStyle.Flex; break; // �������� 3 
                case 3: floatingBuilding_Lock.style.display = DisplayStyle.None; floatingBuilding.style.display = DisplayStyle.Flex; break; // �������� 4 
            }

            switch (SceneManager.GetActiveScene().name)
            {
                case "World": garrisonButton_Lock.style.display = DisplayStyle.Flex; garrisonButton.style.display = DisplayStyle.None; break;
                case "raid": dungeonButton_Lock.style.display = DisplayStyle.Flex; dungeonButton.style.display = DisplayStyle.None; break;
                case "raid2": colosseumButton_Lock.style.display = DisplayStyle.Flex; colosseumButton.style.display = DisplayStyle.None; break;
                case "raid3": floatingBuilding_Lock.style.display = DisplayStyle.Flex; floatingBuilding.style.display = DisplayStyle.None; break;
            }
        }
    }
}
