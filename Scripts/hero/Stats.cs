using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int lv;
    public int exe;
    public int maxExe;
    public int maxExeUp;
    public int giveExe;
    public int health;
    public int maxHealth;
    public int maxHealthUp;
    public int mp;
    public int maxMp;
    public int maxMpUp;
    public int mpFillPerTime;
    public int damage;
    public int damageUp;
    public float attackSpeed;
    float mpTime = 0;
    void Start()
    {
        maxHealth = health;
        maxMp = mp;
    }

    private void FixedUpdate()
    {
        if (mp < maxMp)
        {
            mpTime += Time.deltaTime;
            Debug.Log(mpTime);
            if (mpTime >= 1)
            {
                mp += mpFillPerTime;
                mpTime = 0;
            }
            if (mp > maxMp)
            {
                mp = maxMp;
            }
        }
    }

    public void TakeDamage(GameObject target, GameObject caster,int damage)
    {
        target.GetComponent<Stats>().health -= damage;

        if (target.GetComponent<Stats>().health <= 0)
        {
            if (caster != null)
            {
                caster.GetComponent<Stats>().TakeExe(target.GetComponent<Stats>().giveExe); // 경험치 추가
            }
            Destroy(target.gameObject);
        }

        if (target.GetComponent<Stats>().health > target.GetComponent<Stats>().maxHealth)
        {
            target.GetComponent<Stats>().health = target.GetComponent<Stats>().maxHealth;
        }
    }

    public void TakeExe(int takeExe)
    {
        exe += takeExe;
        if (exe >= maxExe)
        {
            LevelUp();
        }
    }

    public void UsedMP(int usemp)
    {
        mp -= usemp;
    }

    public void LevelUp()
    {
        lv++;
        exe -= maxExe;
        maxExe += maxExeUp;
        health += maxHealthUp;
        maxHealthUp += maxHealthUp;
        damage += damageUp;
        mp += maxMpUp;
        maxMp += maxMpUp;
    }
}
