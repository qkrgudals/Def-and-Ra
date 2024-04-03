using UnityEngine;
using UnityEngine.UI;


public class UnitHP : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;

    // �����̴� UI�� ������ �� �ִ� ����
    public Slider hpSlider; 

    void Start()
    {
        currentHP = maxHP;

        // �����̴� �ʱ�ȭ
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
        }

    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        // HP ����
        if (hpSlider != null)
            hpSlider.value = currentHP;

        // ���� HP�� 0 ���ϰ� �Ǿ��� ��
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ������ ������ �����
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}