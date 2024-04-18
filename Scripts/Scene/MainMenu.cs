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
        // 로드된 씬에서 CountdownTimer 스크립트를 찾아 변수에 할당합니다.
       
        stats = FindObjectOfType<BasicStats>();
        if (countdownTimer == null) {
            Debug.LogError("CountdownTimer 스크립트를 찾을 수 없습니다!");
        }
        else {
            gameManager.totalCoins = 300; // 예시로 300으로 초기화

            // 스테이지 초기화
            enemyManager.currentStage = 1; // 예시로 1로 초기화

            // 시간 초기화
            countdownTimer.timeRemaining = 30.0f; // CountdownTimer의 초기값으로 시간 설정

            //stats.lv = 1;
            //stats.exp = 0;
        }
    }
    public void OnClickNewGame() {
        Debug.Log("새 게임");
        LoadingSceneController.LoadScene("World");
        // 다른 씬으로부터 CountdownTimer 객체를 가져오기 위해 해당 씬을 로드합니다.
        //SceneManager.LoadScene("World", LoadSceneMode.Additive);
        // LoadSceneMode.Additive로 설정하여 기존 씬 위에 새로운 씬을 로드합니다.

        // 새로운 씬이 로드된 후에 CountdownTimer 객체를 찾아 변수에 할당합니다.
        // 주의: 이 방법은 해당 씬이 완전히 로드되기 전까지는 객체를 가져올 수 없습니다.
        // 만약 로드된 씬의 Awake 또는 Start 메서드에서 CountdownTimer 객체를 사용하려면
        // 다른 방법을 고려해야 할 수 있습니다.
        SceneManager.sceneLoaded += OnSceneLoaded;
        // 'LoadingSceneController'를 사용하여 로딩씬을 호출하여 게임 씬으로 전환
        
    }

    public void OnClickLoad() {
        Debug.Log("로드");
    }

    public void OnClickOption() {
        Debug.Log("옵션");
    }

    public void OnClickQuit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
