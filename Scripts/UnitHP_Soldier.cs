using UnityEngine;
using UnityEngine.UI;


public class UnitHP_Soldier : MonoBehaviour
{
    

    public int MaxHP = 100;

    public int currentHP;

    public GameObject UnitMarker;

    UnitInfo unitInfo;

    // �����̴� UI�� ������ �� �ִ� ����
    public Slider hpSlider; 

    void Start()
    {
        unitInfo = FindObjectOfType<UnitInfo>();

        currentHP = MaxHP;
        //CurrentHP = MaxHP;

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
            unitInfo.CurrentHP_Soldier(currentHP);
            if (Input.GetKeyDown(KeyCode.Y)) //Y ��ư�� ������ ������ HP�� ���̴��� �׽�Ʈ 
            {
                TakeDamage(20);
                //Debug.Log(CurrentHP);
                unitInfo.CurrentHP_Soldier(currentHP);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        // HP ����
        if (hpSlider != null)
        {
            hpSlider.value = currentHP;
            //unitInfo.CurrentHP_Soldier(CurrentHP);
        }
            

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