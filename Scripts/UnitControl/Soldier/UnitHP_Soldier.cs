using UnityEngine;
using UnityEngine.UI;


public class UnitHP_Soldier : MonoBehaviour
{
    

    public int MaxHP = 100;

    public int currentHP;

    public GameObject UnitMarker;

    UnitInfo unitInfo;

    // 슬라이더 UI를 연결할 수 있는 변수
    public Slider hpSlider; 

    void Start()
    {
        unitInfo = FindObjectOfType<UnitInfo>();

        currentHP = MaxHP;
        //CurrentHP = MaxHP;

        // 슬라이더 초기화
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
            if (Input.GetKeyDown(KeyCode.Y)) //Y 버튼을 누를때 유닛의 HP가 깎이는지 테스트 
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

        // HP 갱신
        if (hpSlider != null)
        {
            hpSlider.value = currentHP;
            //unitInfo.CurrentHP_Soldier(CurrentHP);
        }
            

        // 유닛의 HP가 0 이하가 되었을 때
        if (currentHP <= 0)
        {
            Die();
           
        }
    }

    public void Die()
    {
        // 유닛이 죽으면 사라짐
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}