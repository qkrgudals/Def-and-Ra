using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SwordStats : BasicStats {
    public int maxExpUp = 50;
    public int maxHealthUp = 50;
    public int mp = 100;
    public int maxMp = 100;
    public int maxMpUp = 50;
    public int mpFillPerTime = 1;
    public int damageUp = 10;
    public float speed;
    //UnitInfo unitInfo;
    public string swregion;
    public  int swordLv = 1;
    public  int swordExp = 0;

    float mpTime = 0;

    public int nowLv = 1;

    void Start() {
        swregion = "world";
        lv = swordLv;
        exp = swordExp;
        maxHealth = currentHealth;
        maxMp = mp;

        //unitInfo = FindObjectOfType<UnitInfo>();
        speed = GetComponent<NavMeshAgent>().speed;
        unitInfo.SwordMasterWindow(false, nowLv, exp, maxExp, currentHealth, maxHealth, mp, maxMp, attackdamage, speed);
    }

    private void FixedUpdate() {
        swordLv = lv;
        swordExp = exp;
        if (mp < maxMp) {
            mpTime += Time.deltaTime;
            if (mpTime >= 1) {
                mp += mpFillPerTime;
                mpTime = 0;
            }
            if (mp > maxMp) {
                mp = maxMp;
            }
        }
        if (nowLv < lv) {
            int upLv = lv - nowLv;
            nowLv = lv;
            maxExp += maxExpUp * upLv;
            maxHealth += maxHealthUp * upLv;
            currentHealth += maxHealthUp * upLv;
            maxMp += maxMpUp * upLv;
            mp += maxMpUp * upLv;
            attackdamage += damageUp * upLv;
        }
    }

    private void Update() {
        // if (Input.GetKeyDown(KeyCode.U)) // 경험치 Test: U버튼 누를시 경험치 + 10 획득
        //{
        //TakeExp(10);
        Die();
    
        
        if (nowLv < lv) {
                int upLv = lv - nowLv;
                nowLv = lv;
                maxExp += maxExpUp * upLv;
                maxHealth += maxHealthUp * upLv;
                currentHealth += maxHealthUp * upLv;
                maxMp += maxMpUp * upLv;
                mp += maxMpUp * upLv;
                attackdamage += damageUp * upLv;
            }

            //unitInfo.SwordMasterWindow(false, nowLv, exp, maxExp, currentHealth, maxHealth, mp, maxMp, attackdamage, speed);
       
        //}
    }

    public void UsedMP(int usemp) {
        mp -= usemp;
    }
}
