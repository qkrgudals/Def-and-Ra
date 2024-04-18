using UnityEngine;
using UnityEngine.UI;


public class UnitHP_Archer : MonoBehaviour
{
    

    public int MaxHP;

    public int currentHP;

    public GameObject UnitMarker;

    UnitInfo unitInfo;

    // �����̴� UI�� ������ �� �ִ� ����
    public Slider hpSlider; 

    void Start()
    {
        unitInfo = FindObjectOfType<UnitInfo>();

        currentHP = MaxHP;

        // �����̴� �ʱ�ȭ
        if (hpSlider != null)
        {
            hpSlider.maxValue = MaxHP;
            //archerCurrentHP = archerMaxHP;
        }

    }

    private void Update()
    {
 
       if (UnitMarker.activeSelf)
        {
            unitInfo.CurrentHP_Archer(currentHP);
            if (Input.GetKeyDown(KeyCode.Y)) //Y ��ư�� ������ ������ HP�� ���̴��� �׽�Ʈ 
            {
                TakeDamage(20);
                unitInfo.CurrentHP_Archer(currentHP);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        //archerCurrentHP -= damage;

        // HP ����
        if (hpSlider != null)
            hpSlider.value = currentHP;
           // hpSlider.value = archerCurrentHP;

        // ������ HP�� 0 ���ϰ� �Ǿ��� ��
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // ������ ������ �����
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}