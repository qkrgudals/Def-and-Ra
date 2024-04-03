using UnityEngine;
using UnityEngine.UI;


public class UnitHP : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;

    // 슬라이더 UI를 연결할 수 있는 변수
    public Slider hpSlider; 

    void Start()
    {
        currentHP = maxHP;

        // 슬라이더 초기화
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
        }

    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        // HP 갱신
        if (hpSlider != null)
            hpSlider.value = currentHP;

        // 적의 HP가 0 이하가 되었을 때
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 유닛이 죽으면 사라짐
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}