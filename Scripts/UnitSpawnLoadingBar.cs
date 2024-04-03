using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawnLoadingBar : MonoBehaviour {
    [SerializeField]
    private GameObject soldierUnitPrefab; // 생성될 검사 유닛의 프리팹
    [SerializeField]
    private GameObject archerUnitPrefab; // 생성될 궁수 유닛의 프리팹
    UnitSpawner2 Spawner2;
    public int StartGauge = 0;
    public int EndGauge = 100;
    private float duration = 4f; // 스폰 대기시간
    private GameManager gameManager;
    public Slider spawnSlider;
    public int spawnNum; // 유닛 생성 슬롯 아이콘을을 클릭할시 생성될 유닛 수 

    public bool spawnSoldier = false;
    public bool spawnArcher = false;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        Spawner2 = FindObjectOfType<UnitSpawner2>();
        gameObject.SetActive(false);
    }
    public void StartLoading_SpawnSoldier(int spawnCnt) // 검사생성 로딩
    {
        spawnSlider.value = StartGauge; // 로딩 시작시 velue 0

        gameObject.SetActive(true);

        StartCoroutine(LoadingTime(StartGauge, EndGauge, duration, spawnCnt));
    }

    public void StartLoading_SpawnArcher(int spawnCnt) // 궁수생성 로딩
    {
        spawnSlider.value = StartGauge;

        gameObject.SetActive(true);


        StartCoroutine(LoadingTime(StartGauge, EndGauge, duration, spawnCnt));
    }

    IEnumerator LoadingTime(float startValue, float endValue, float time, int spawnCnt) // 로딩타임
    {

        float elapsedTime = 0f;
        while (elapsedTime < time) {
            // 4초 동안 일정한 속도로 증가하는 값을 계산
            float progress = elapsedTime / time;
            float newValue = Mathf.Lerp(startValue, endValue, progress);

            // 값을 슬라이더에 적용
            spawnSlider.value = newValue;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (spawnSoldier == true) // 생성할 유닛이 검사일 경우
        {
            spawnNum = spawnCnt;
            for (int i = 1; i <= spawnNum; i++) // 생성될 유닛 수 만큼 반복
            {
                gameManager.RecruitSoldier(10);
                Spawner2.SpawnUnit(soldierUnitPrefab); // 검사 유닛 프리팹 생성
            }
            spawnSoldier = false;
        }
        else if (spawnArcher == true) // 생성할 유닛이 궁수일 경우
        {
            spawnNum = spawnCnt;
            for (int i = 1; i <= spawnNum; i++) {
                gameManager.RecruitSoldier(20);
                Spawner2.SpawnUnit2(archerUnitPrefab); // 궁수 유닛 프리팹 생성
            }
            spawnArcher = false;
        }

        // 4초 후에 작업이 완료되면 게임 오브젝트를 비활성화
        gameObject.SetActive(false);
    }
}
