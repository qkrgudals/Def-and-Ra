using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SorceressStats : BasicStats {
    public int maxExeUp = 50;
    public int maxHealthUp = 50;
    public int mp = 100;
    public int maxMp = 100;
    public int maxMpUp = 50;
    public int mpFillPerTime = 1;
    public int damageUp = 10;
    public float speed;
    //UnitInfo unitInfo;

    public int sorceressLv = 1;
    public int sorceressExp = 0;
    public string scregion;
    float mpTime = 0;

    public int nowLv = 1;

    void Start() {
        scregion = "world";
        lv = sorceressLv;
        exp = sorceressExp;
        maxHealth = currentHealth;
        maxMp = mp;

        //unitInfo = FindObjectOfType<UnitInfo>();
        speed = GetComponent<NavMeshAgent>().speed;
        unitInfo.SorceressWindow(false, nowLv, exp, maxExp, currentHealth, maxHealth, mp, maxMp, attackdamage, speed);
    }

    private void FixedUpdate() {
        sorceressLv = lv;
        sorceressExp = exp;
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
            maxExp += maxExeUp * upLv;
            maxHealth += maxHealthUp * upLv;
            currentHealth += maxHealthUp * upLv;
            maxMp += maxMpUp * upLv;
            mp += maxMpUp * upLv;
            attackdamage += damageUp * upLv;
        }
    }
    private void Update() {
        //if (Input.GetKeyDown(KeyCode.U)) // 경험치 Test: U버튼 누를시 경험치 + 10 획득
        // {
        //TakeExp(10);
        Die();
        
            if (nowLv < lv) {
                int upLv = lv - nowLv;
                nowLv = lv;
                maxExp += maxExeUp * upLv;
                maxHealth += maxHealthUp * upLv;
                currentHealth += maxHealthUp * upLv;
                maxMp += maxMpUp * upLv;
                mp += maxMpUp * upLv;
                attackdamage += damageUp * upLv;
            }

           // unitInfo.SorceressWindow(true, nowLv, exp, maxExp, currentHealth, maxHealth, mp, maxMp, attackdamage, speed);
      
        //}
    }

    public void UsedMP(int usemp) {
        mp -= usemp;
    }
}