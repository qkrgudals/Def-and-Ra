using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public GameObject gameOverPanel; // ���� ���� UI �г�
    public CountdownTimer countdownTimer;
    void Start() {
        // ���� ���� �г� ��Ȱ��ȭ
        gameOverPanel.SetActive(false);
        
    }

    public void OnGameOver() {
        // ���� ���� �� ������ ���۵�
        Time.timeScale = 0; // ���� �Ͻ�����

        // ���� ���� �г� Ȱ��ȭ
        gameOverPanel.SetActive(true);
    }

    public void RestartGame() {
        // ���� �����
        Time.timeScale = 1; // ���� �簳
    
        LoadingSceneController.LoadScene("MainMenu");
        //LoadingScene.LoadScene("MainMenu");
        //SceneManager.LoadScene("MainMenu"); // ���� �޴� ������ �̵�
    }
   
    public void OnClickQuit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}