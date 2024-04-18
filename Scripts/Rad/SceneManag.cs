using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManag : MonoBehaviour
{   EnemyManager enemyManager;
   
    public int maxStageTransitions = 3; // �̵��� �� �ִ� �ִ� Ƚ��
    private int stageTransitionsRemaining; // ���� �̵� Ƚ��
    // Start is called before the first frame update
    void Start()
    {
      
        enemyManager = FindObjectOfType<EnemyManager>();
        stageTransitionsRemaining = maxStageTransitions;
        

    }
    void OnCollisionEnter(Collision collision) {

        // �ε��� ��ü�� "Player" �±׸� ������ �ְ�, ���� �̵� Ƚ���� ���� ��
        if (collision.gameObject.CompareTag("Player") && stageTransitionsRemaining > 0
            && enemyManager.currentStage == 1) {
         

            SceneManager.LoadScene("raid");
           
            // �̵� Ƚ�� ����
            stageTransitionsRemaining--;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}