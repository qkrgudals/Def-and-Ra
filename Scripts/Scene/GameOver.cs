using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public GameObject gameOverPanel; // 게임 오버 UI 패널
    public CountdownTimer countdownTimer;
    void Start() {
        // 게임 오버 패널 비활성화
        gameOverPanel.SetActive(false);
        
    }

    public void OnGameOver() {
        // 게임 오버 시 실행할 동작들
        Time.timeScale = 0; // 게임 일시정지

        // 게임 오버 패널 활성화
        gameOverPanel.SetActive(true);
    }

    public void RestartGame() {
        // 게임 재시작
        Time.timeScale = 1; // 게임 재개
    
        LoadingSceneController.LoadScene("MainMenu");
        //LoadingScene.LoadScene("MainMenu");
        //SceneManager.LoadScene("MainMenu"); // 메인 메뉴 씬으로 이동
    }
   
    public void OnClickQuit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}