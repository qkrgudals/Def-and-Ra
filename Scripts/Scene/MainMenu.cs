using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public CountdownTimer countdownTimer;
    GameManager gameManager;
    public BasicStats stats;
    EnemyManager enemyManager;
    void Start() {
        enemyManager = FindObjectOfType<EnemyManager>();
        countdownTimer = FindObjectOfType<CountdownTimer>();
        gameManager = FindObjectOfType<GameManager>();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // �ε�� ������ CountdownTimer ��ũ��Ʈ�� ã�� ������ �Ҵ��մϴ�.
       
        stats = FindObjectOfType<BasicStats>();
        if (countdownTimer == null) {
            Debug.LogError("CountdownTimer ��ũ��Ʈ�� ã�� �� �����ϴ�!");
        }
        else {
            gameManager.totalCoins = 300; // ���÷� 300���� �ʱ�ȭ

            // �������� �ʱ�ȭ
            enemyManager.currentStage = 1; // ���÷� 1�� �ʱ�ȭ

            // �ð� �ʱ�ȭ
            countdownTimer.timeRemaining = 30.0f; // CountdownTimer�� �ʱⰪ���� �ð� ����

            //stats.lv = 1;
            //stats.exp = 0;
        }
    }
    public void OnClickNewGame() {
        Debug.Log("�� ����");
        LoadingSceneController.LoadScene("World");
        // �ٸ� �����κ��� CountdownTimer ��ü�� �������� ���� �ش� ���� �ε��մϴ�.
        //SceneManager.LoadScene("World", LoadSceneMode.Additive);
        // LoadSceneMode.Additive�� �����Ͽ� ���� �� ���� ���ο� ���� �ε��մϴ�.

        // ���ο� ���� �ε�� �Ŀ� CountdownTimer ��ü�� ã�� ������ �Ҵ��մϴ�.
        // ����: �� ����� �ش� ���� ������ �ε�Ǳ� �������� ��ü�� ������ �� �����ϴ�.
        // ���� �ε�� ���� Awake �Ǵ� Start �޼��忡�� CountdownTimer ��ü�� ����Ϸ���
        // �ٸ� ����� ����ؾ� �� �� �ֽ��ϴ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
        // 'LoadingSceneController'�� ����Ͽ� �ε����� ȣ���Ͽ� ���� ������ ��ȯ
        
    }

    public void OnClickLoad() {
        Debug.Log("�ε�");
    }

    public void OnClickOption() {
        Debug.Log("�ɼ�");
    }

    public void OnClickQuit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
