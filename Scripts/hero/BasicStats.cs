using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicStats : Health {
    public int lv = 1;
    public int exp = 0;
    public int maxExp = 100;
    public int attackdamage = 10;
    public float attackSpeed = 2;

    public void TakeExp(int takeExp) {
        exp += takeExp;
        if (exp >= maxExp) {
            lv++;
            exp -= maxExp;
        }
    }
  
    public void Die() {
        if (currentHealth <= 0 ) {
            SceneManager.LoadScene("GameOver");
        }
    }
}
