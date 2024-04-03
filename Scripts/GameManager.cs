using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI coinText; // UI에 표시될 코인 수 텍스트
    public TextMeshProUGUI stageText;
    public int totalCoins = 300; // 전체 코인 수
    //public int costPerSoldier = 10; // 한 병사당 드는 코인 비용
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

    // 적을 죽였을 때 호출될 함수
    public void EnemyKilled(int enemyCoins) {
        totalCoins += enemyCoins;
        //Debug.Log(totalCoins);
        UpdateCoinText();
    }

    // 병사를 뽑을 때 호출될 함수
    public void RecruitSoldier(int costPerSoldier) {
        

        // 충분한 코인이 있는지 확인
        if (totalCoins >= costPerSoldier) {
            totalCoins -= costPerSoldier;
            //Debug.Log(totalCoins);
            UpdateCoinText();
        }
        else {
            Debug.Log("코인이 부족하여 병사를 뽑을 수 없습니다. 현재 COIN: " + totalCoins);
        }
    }

    // UI에 코인 수 업데이트 함수
    void UpdateCoinText() {
        coinText.text = "COIN: " + totalCoins;
    }
    private void Update() {
        if (enemyManager != null) {
            stageText.text = "Stage: " + (enemyManager.currentStage+1);

            switch (enemyManager.currentStage) // 스테이지 잠금 해제 조건
            {
                case 1: dungeonButton_Lock.style.display = DisplayStyle.None; dungeonButton.style.display = DisplayStyle.Flex; break; // 스테이지 2
                case 2: colosseumButton_Lock.style.display = DisplayStyle.None; colosseumButton.style.display = DisplayStyle.Flex; break; // 스테이지 3 
                case 3: floatingBuilding_Lock.style.display = DisplayStyle.None; floatingBuilding.style.display = DisplayStyle.Flex; break; // 스테이지 4 
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
