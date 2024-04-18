using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Enemy;

public class EnemyManager : MonoBehaviour {
    public EnemyData[] enemyData;
    public int[] maxEnemiesPerType; // 각 적 유형별 최대 적 수
    public float spawnRadius = 5.0f;
    public Vector3[] spawnPositions;
    public List<GameObject> enemies = new List<GameObject>();
    private float[] timeSinceLastSpawn;
    objectfalling objectfall;
    // 스테이지별 몹 생성 관리
    public  int currentStage = 1;
    private int step = 1;
    CountdownTimer countdown;
    // 스테이지별 시간 간격
    public float[] stageTimeIntervals = { 30.0f, 30.0f, 30.0f, 30.0f };

    // 스테이지 클리어 여부
    private bool[] stageCleared = { false, false, false, false };

    // 스테이지별 시작 시간
    private float[] stageStartTime;

    void Start() {
        countdown = FindObjectOfType<CountdownTimer>();
        objectfall = FindObjectOfType<objectfalling>();
        // Initialize the time since last spawn array
        timeSinceLastSpawn = new float[enemyData.Length];
        stageStartTime = new float[stageTimeIntervals.Length];
        // 스테이지별 시작 시간 설정
        for (int i = 0; i < stageStartTime.Length; i++) {
            stageStartTime[i] = Time.time;
        }
    }

    void Update() {

        // 스테이지별 시간 간격 체크
        for (int i = 0; i < stageTimeIntervals.Length; i++) {
            if (Time.time - stageStartTime[i] > stageTimeIntervals[i] && !stageCleared[i]) {

                // 스테이지 클리어 여부 확인
                stageCleared[0] = true;
                int count = 0;

                if (enemies.Count == 9) {
                    if (enemies[8].GetComponent<Enemy>().enemyType == EnemyType.Boss) {
                        if (GameObject.Find("1_보스(Clone)") == null) {
                            //Debug.Log("//");
                            stageCleared[1] = true;

                            currentStage++;
                            //objectfall.Rise();
                            count++;

                            if (currentStage >= 3) {
                                currentStage = 2;
                                //countdown.timeRemaining = 30.0f;
                            }


                            //countdown.timeRemaining -= Time.deltaTime;
                            //objectfall.Fall();
                        }

                    }
                }
                if (enemies.Count == 18) {
                    if (enemies[17].GetComponent<Enemy>().enemyType == EnemyType.Boss) {
                        if (GameObject.Find("2_보스(Clone)") == null) {
                            stageCleared[2] = true;


                            currentStage++;

                            //objectfall.Rise();
                            if (currentStage >= 4) {
                                currentStage = 3;
                                //countdown.timeRemaining = 30.0f;
                            }

                            //countdown.timeRemaining -= Time.deltaTime;
                            //objectfall.Fall();
                        }

                    }
                }
                if (enemies.Count == 27) {
                    if (enemies[26].GetComponent<Enemy>().enemyType == EnemyType.Boss) {
                        if (GameObject.Find("3_보스(Clone)") == null) {
                            stageCleared[3] = true;


                            currentStage++;

                            //objectfall.Rise();
                            if (currentStage >= 5) {
                                currentStage = 4;
                                // countdown.timeRemaining = 30.0f;
                            }

                            //countdown.timeRemaining -= Time.deltaTime;
                            //objectfall.Fall();

                        }

                    }
                }


                //if (stageClear) {
                // 스테이지 클리어



                // 다음 스테이지로 넘어감
                if (i < stageTimeIntervals.Length - 1) {
                    //currentStage++;
                    //step = 1;
                    stageStartTime[i + 1] = Time.time;
                }
                //}

            }
        }

        // Update the time since the last enemy was spawned for each enemy type
        for (int i = 0; i < enemyData.Length; i++) {
            timeSinceLastSpawn[i] += Time.deltaTime;
        }

        // Spawn a new enemy if it's been long enough
        for (int i = 0; i < enemyData.Length; i++) {
            //Debug.Log(enemyData.Length);


            if (timeSinceLastSpawn[i] >= enemyData[i].spawnInterval && enemies.Count < maxEnemiesPerType[i] && stageCleared[currentStage - 1]) {


                // 스테이지별 몹 생성 관리
                /*
                Debug.Log("max :" +maxEnemiesPerType[i]);
                Debug.Log("e : " +enemies.Count);
                Debug.Log("currs: "+currentStage);
                Debug.Log("step: "+step);
                Debug.Log("i: "+i);
                Debug.Log(stageCleared[currentStage]);
                */
                //Debug.Log("t: " +timeSinceLastSpawn[2]);
                if (currentStage == 1 && step == 1 && i == 0 && countdown.timeRemaining == 0) {


                    SpawnEnemy(i, spawnPositions[0]);

                    if (enemies.Count >= maxEnemiesPerType[i]) {
                        step++;
                        //enemies.Clear();
                    }
                }
                else if (currentStage == 1 && step == 2 && i == 1) {
                    // 정예 몹 생성
                    SpawnEnemy(i, spawnPositions[0]);

                    if (enemies.Count >= maxEnemiesPerType[i]) {
                        step++;
                        //enemies.Clear();

                    }
                    timeSinceLastSpawn[2] = 0f;
                }
                else if (currentStage == 1 && step == 3 && i == 2) {
                    // 보스 몹 생성
                    SpawnEnemy(i, spawnPositions[0]);

                    if (enemies.Count >= maxEnemiesPerType[i]) {
                        //enemies.Clear();
                        //currentStage++;
                        step = 1;

                        //Debug.Log("//")
                        countdown.timeRemaining = 60.0f;
                    }
                    timeSinceLastSpawn[3] = 0.0f;
                    //currentStage++;
                    //step = 1;

                }

                //else { break; }

                else if (currentStage == 2 && step == 1 && i == 3 && countdown.timeRemaining <= 0) {


                    if (enemies.Count < maxEnemiesPerType[i - 1]
                        + (maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 2) {
                        SpawnEnemy(i, spawnPositions[0]);
                    }
                    else if (enemies.Count >= maxEnemiesPerType[i - 1]
                        + (maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 2) {
                        SpawnEnemy(i, spawnPositions[1]);
                    }
                    if (enemies.Count >= maxEnemiesPerType[i]) {
                        step++;
                        timeSinceLastSpawn[4] = 0.0f;
                        //enemies.Clear();
                    }
                }

                else if (currentStage == 2 && step == 2 && i == 4) {

                    if (enemies.Count < maxEnemiesPerType[i - 1]
                        + (maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 2) {
                        SpawnEnemy(i, spawnPositions[0]);
                    }
                    else if (enemies.Count >= maxEnemiesPerType[i - 1]
                        + (maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 2) {
                        SpawnEnemy(i, spawnPositions[1]);
                    }
                    if (enemies.Count >= maxEnemiesPerType[i]) {
                        step++;
                        timeSinceLastSpawn[5] = 0.0f;
                        //enemies.Clear();
                    }
                }
                else if (currentStage == 2 && step == 3 && i == 5) {

                    SpawnEnemy(i, spawnPositions[0]);

                    if (enemies.Count >= maxEnemiesPerType[i]) {
                        //currentStage++;
                        step = 1;
                        countdown.timeRemaining = 60.0f;

                        //enemies.Clear();
                    }
                    timeSinceLastSpawn[6] = 0.0f;

                }
                else if (currentStage == 3 && step == 1 && i == 6 && countdown.timeRemaining <= 0) {

                    if (enemies.Count < maxEnemiesPerType[i - 1]
                      + (maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 3)
                        SpawnEnemy(i, spawnPositions[0]);
                    else if (enemies.Count >= maxEnemiesPerType[i - 1]
                      + (maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 3
                      && enemies.Count < maxEnemiesPerType[i - 1]
                         + ((maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 3) * 2)
                        SpawnEnemy(i, spawnPositions[1]);
                    else if (enemies.Count >= maxEnemiesPerType[i - 1]
                         + ((maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 3) * 2)
                        SpawnEnemy(i, spawnPositions[2]);
                    if (enemies.Count >= maxEnemiesPerType[i]) {
                        step++;
                        timeSinceLastSpawn[7] = 0.0f;
                        //enemies.Clear();
                    }
                }
                else if (currentStage == 3 && step == 2 && i == 7) {
                    if (enemies.Count < maxEnemiesPerType[i - 1]
                    + (maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 3)
                        SpawnEnemy(i, spawnPositions[0]);
                    else if (enemies.Count >= maxEnemiesPerType[i - 1]
                      + (maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 3
                      && enemies.Count < maxEnemiesPerType[i - 1]
                         + ((maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 3) * 2)
                        SpawnEnemy(i, spawnPositions[1]);
                    else if (enemies.Count >= maxEnemiesPerType[i - 1]
                         + ((maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 3) * 2)
                        SpawnEnemy(i, spawnPositions[2]);

                    if (enemies.Count >= maxEnemiesPerType[i]) {
                        step++;
                        timeSinceLastSpawn[8] = 0.0f;
                        //enemies.Clear();
                    }
                }
                else if (currentStage == 3 && step == 3 && i == 8) {
                    SpawnEnemy(i, spawnPositions[0]);

                    if (enemies.Count >= maxEnemiesPerType[i]) {
                        //currentStage++;
                        step = 1;
                        //enemies.Clear();
                        countdown.timeRemaining = 60.0f;

                    }
                    timeSinceLastSpawn[9] = 0.0f;
                }
                else if (currentStage == 4 && step == 1 && i == 9 && countdown.timeRemaining <= 0) {

                    if (enemies.Count < maxEnemiesPerType[i - 1]
                      + (maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 4)
                        SpawnEnemy(i, spawnPositions[0]);
                    else if (enemies.Count >= maxEnemiesPerType[i - 1]
                      + (maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 4
                      && enemies.Count < maxEnemiesPerType[i - 1]
                         + ((maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 4) * 2)
                        SpawnEnemy(i, spawnPositions[1]);
                    else if (enemies.Count >= maxEnemiesPerType[i - 1]
                         + ((maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 4) * 2
                         && enemies.Count < maxEnemiesPerType[i - 1]
                       + ((maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 4) * 3)
                        SpawnEnemy(i, spawnPositions[2]);
                    else if (enemies.Count >= maxEnemiesPerType[i - 1]
                       + ((maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 4) * 3)
                        SpawnEnemy(i, spawnPositions[3]);
                    if (enemies.Count >= maxEnemiesPerType[i]) {
                        step++;
                        timeSinceLastSpawn[10] = 0.0f;

                        //enemies.Clear();
                    }
                }
                else if (currentStage == 4 && step == 2 && i == 10) {
                    if (enemies.Count < maxEnemiesPerType[i - 1]
                      + (maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 4)
                        SpawnEnemy(i, spawnPositions[0]);
                    else if (enemies.Count >= maxEnemiesPerType[i - 1]
                      + (maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 4
                      && enemies.Count < maxEnemiesPerType[i - 1]
                         + ((maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 4) * 2)
                        SpawnEnemy(i, spawnPositions[1]);
                    else if (enemies.Count >= maxEnemiesPerType[i - 1]
                         + ((maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 4) * 2
                         && enemies.Count < maxEnemiesPerType[i - 1]
                       + ((maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 4) * 3)
                        SpawnEnemy(i, spawnPositions[2]);
                    else if (enemies.Count >= maxEnemiesPerType[i - 1]
                       + ((maxEnemiesPerType[i] - maxEnemiesPerType[i - 1]) / 4) * 3)
                        SpawnEnemy(i, spawnPositions[3]);

                    if (enemies.Count >= maxEnemiesPerType[i]) {
                        step++;
                        timeSinceLastSpawn[11] = 0.0f;
                        //enemies.Clear();
                    }
                }
                else if (currentStage == 4 && step == 3 && i == 11) {
                    // 보스 몹 생성
                    SpawnEnemy(i, spawnPositions[0]);

                    if (enemies.Count >= maxEnemiesPerType[i]) {
                        //currentStage++;
                        step = 1;
                        //enemies.Clear();
                    }
                }
                /*
              else if (currentStage == 4 && step == 1) {
                  SpawnEnemy(i, spawnPositions[0]);
                  SpawnEnemy(i, spawnPositions[1]);
                  SpawnEnemy(i, spawnPositions[2]);
                  SpawnEnemy(i, spawnPositions[3]);
              }
              else if (currentStage == 4 && step == 2) {
                  // 정예 몹 생성
                  SpawnEnemy(i + 1, spawnPositions[0]);
                  SpawnEnemy(i + 1, spawnPositions[1]);
                  SpawnEnemy(i + 1, spawnPositions[2]);
              }
              else if (currentStage == 4 && step == 3) {
                  // 보스 몹 생성
                  SpawnEnemy(i + 2, spawnPositions[3]);
              }
                */
                timeSinceLastSpawn[i] = 0.0f;

            }
        }

        // Remove any enemies that have been destroyed
        for (int i = enemies.Count - 1; i >= 0; i--) {
            if (enemies[i] == null) {
                enemies.RemoveAt(i);
            }
        }
    }

    private void SpawnEnemy(int enemyIndex, Vector3 spawnPosition) {

        GameObject enemy = Instantiate(enemyData[enemyIndex].prefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<Enemy>().Initialize(enemyData[enemyIndex]);
        enemies.Add(enemy);
    }
}

/*
 using System.Collections;
 using System.Collections.Generic;
 using Unity.VisualScripting;
 using UnityEngine;
 public class EnemyManager : MonoBehaviour{ 
 public EnemyData[] enemyData;  
 public int[] maxEnemies;   
 public float spawnRadius = 5.0f; 
 public Vector3[] spawnPositions;   
  private List<GameObject> enemies = new List<GameObject>();  
  private float[] timeSinceLastSpawn;   
  private void Start() {        // Initialize the time since last spawn array    
 timeSinceLastSpawn = new float[enemyData.Length];      
         // Spawn the initial enemies        
 for (int i = 0; i < enemyData.Length; i++) {        
  for (int j = 0; j < enemyData[i].maxEnemies; j++) {    
 SpawnEnemy(i, spawnPositions[0]);            }        }          } 
   
private void Update() {     
  for (int i = 0; i < enemyData.Length; i++) {         
timeSinceLastSpawn[i] += Time.deltaTime;        }      
// Spawn a new enemy if it's been long enough        
for (int i = 0; i < enemyData.Length; i++) {                   
if (timeSinceLastSpawn[i] >= enemyData[i].spawnInterval && enemies.Count < maxEnemies[i]) {  
SpawnEnemy(i, spawnPositions[0]);                
timeSinceLastSpawn[i] = 0.0f;               
Debug.Log(i);            
}                    
}               
// Remove any enemies that have been destroyed       
for (int i = enemies.Count - 1; i >= 0; i--) {          
if (enemies[i] == null) {               
enemies.RemoveAt(i);           
}        
}    
}    
private void SpawnEnemy(int enemyIndex, Vector3 spawnPosition) {  
// Spawn the enemy       
GameObject enemy = Instantiate(enemyData[enemyIndex].prefab, spawnPosition, Quaternion.identity);
*/
// enemy.GetComponent<Enemy>().Initialize(enemyData[enemyIndex]);        enemies.Add(enemy);    ***/


