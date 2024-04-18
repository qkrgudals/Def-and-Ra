using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManag : MonoBehaviour
{   EnemyManager enemyManager;
    private GameObject player;
    public int maxStageTransitions = 3; // 이동할 수 있는 최대 횟수
    private int stageTransitionsRemaining; // 남은 이동 횟수
    // Start is called before the first frame update

    void Start()
    {
      
        enemyManager = FindObjectOfType<EnemyManager>();
        stageTransitionsRemaining = maxStageTransitions;
        

    }
    void OnCollisionEnter(Collision collision) {

        // 부딪힌 객체가 "Player" 태그를 가지고 있고, 남은 이동 횟수가 있을 때
        if (collision.gameObject.CompareTag("Player") && stageTransitionsRemaining > 0
            && enemyManager.currentStage == 2) {
         
            player = collision.gameObject;

            player.SetActive(false);

            SceneManager.LoadScene("raid");
           
            // 이동 횟수 감소
            stageTransitionsRemaining--;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
