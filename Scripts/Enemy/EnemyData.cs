using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyData {
    public GameObject prefab;
    public int maxEnemies;
    public float spawnInterval;
    public float moveSpeed;
    public int damage;
    public int hp;
    public float attackDelay;
    public float attackRange;
    public int Coin;
    public int EXP;
}