using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawnLoadingBar : MonoBehaviour {
    [SerializeField]
    private GameObject soldierUnitPrefab; // ������ �˻� ������ ������
    [SerializeField]
    private GameObject archerUnitPrefab; // ������ �ü� ������ ������
    UnitSpawner2 Spawner2;
    public int StartGauge = 0;
    public int EndGauge = 100;
    private float duration = 4f; // ���� ���ð�
    private GameManager gameManager;
    public Slider spawnSlider;
    public int spawnNum; // ���� ���� ���� ���������� Ŭ���ҽ� ������ ���� �� 

    public bool spawnSoldier = false;
    public bool spawnArcher = false;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        Spawner2 = FindObjectOfType<UnitSpawner2>();
        gameObject.SetActive(false);
    }
    public void StartLoading_SpawnSoldier(int spawnCnt) // �˻���� �ε�
    {
        spawnSlider.value = StartGauge; // �ε� ���۽� velue 0

        gameObject.SetActive(true);

        StartCoroutine(LoadingTime(StartGauge, EndGauge, duration, spawnCnt));
    }

    public void StartLoading_SpawnArcher(int spawnCnt) // �ü����� �ε�
    {
        spawnSlider.value = StartGauge;

        gameObject.SetActive(true);


        StartCoroutine(LoadingTime(StartGauge, EndGauge, duration, spawnCnt));
    }

    IEnumerator LoadingTime(float startValue, float endValue, float time, int spawnCnt) // �ε�Ÿ��
    {

        float elapsedTime = 0f;
        while (elapsedTime < time) {
            // 4�� ���� ������ �ӵ��� �����ϴ� ���� ���
            float progress = elapsedTime / time;
            float newValue = Mathf.Lerp(startValue, endValue, progress);

            // ���� �����̴��� ����
            spawnSlider.value = newValue;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (spawnSoldier == true) // ������ ������ �˻��� ���
        {
            spawnNum = spawnCnt;
            for (int i = 1; i <= spawnNum; i++) // ������ ���� �� ��ŭ �ݺ�
            {
                gameManager.RecruitSoldier(10);
                Spawner2.SpawnUnit(soldierUnitPrefab); // �˻� ���� ������ ����
            }
            spawnSoldier = false;
        }
        else if (spawnArcher == true) // ������ ������ �ü��� ���
        {
            spawnNum = spawnCnt;
            for (int i = 1; i <= spawnNum; i++) {
                gameManager.RecruitSoldier(20);
                Spawner2.SpawnUnit2(archerUnitPrefab); // �ü� ���� ������ ����
            }
            spawnArcher = false;
        }

        // 4�� �Ŀ� �۾��� �Ϸ�Ǹ� ���� ������Ʈ�� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
