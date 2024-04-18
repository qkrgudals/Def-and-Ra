using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public int maxHealth = 100;  // 최대 체력
    public int currentHealth;     // 현재 체력
    private Animator animator;    // Animator 컴포넌트 참조

    public GameObject UnitMarker;
    private bool isDying = false;  // 죽는 중인지 여부
    public event Action OnCharacterDeath;
    public UnitInfo unitInfo;
    public Slider hpSlider;


    void Start() {
        unitInfo = FindObjectOfType<UnitInfo>();
        currentHealth = maxHealth;  // 게임 시작 시 현재 체력을 최대 체력으로 초기화
        animator = GetComponent<Animator>();
        // Animator 컴포넌트 참조
        if (hpSlider != null) {
            hpSlider.maxValue = maxHealth;
        }
   
    }

    // 피해를 입을 때 호출할 메서드
    public void TakeDamage(int damage) {
        currentHealth -= damage;

        // HP 갱신
        if (hpSlider != null)
            hpSlider.value = currentHealth;

        
        // 체력이 0 이하로 떨어졌을 때의 처리
        animator = GetComponent<Animator>();
        if (currentHealth <= 0 && !isDying) {
            Die();
            isDying = true;
            currentHealth = 0;
        }
    }

    // 사망 시 호출할 메서드
    void Die() {
        // 여기에 사망 처리를 추가할 수 있습니다.
        // 예를 들어, 사망 소리 재생, 게임 오버 화면 표시 등
        // 죽는 애니메이션 재생
        if (animator != null) {
            animator.SetTrigger("Death");  // "Death" 트리거를 발동시켜 죽는 애니메이션을 재생
        }

        // 여기에 추가적인 사망 처리를 추가할 수 있습니다.
        // 예를 들어, 사망 소리 재생, 게임 오버 화면 표시 등
        OnCharacterDeath?.Invoke();
        StartCoroutine(DestroyAfterAnimation());
    }

    
    IEnumerator DestroyAfterAnimation() {
        if (animator != null)
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);  // 애니메이션 길이만큼 대기
            //Time.timeScale = 1f;  // 시간 경과 속도를 다시 원래대로 설정
            // 유닛 파괴
            //Destroy(gameObject);
            gameObject.SetActive(false);
            
        }
        
    }
    

    void Update() {
       
        if (UnitMarker.activeSelf) {
            if (gameObject.name.Equals("Soldier_01(Clone)")) {
                unitInfo.CurrentHP_Soldier(currentHealth);
                //if (Input.GetKeyDown(KeyCode.Y)) {
                    //TakeDamage(20);
                    //unitInfo.CurrentHP_Soldier(currentHealth);
                //}
            }
            else if (gameObject.name.Equals("Erika Archer With Bow Arrow(Clone)")) {
                unitInfo.CurrentHP_Archer(currentHealth);
               // if (Input.GetKeyDown(KeyCode.Y)) //Y 버튼을 누를때 유닛의 HP가 깎이는지 테스트 
                //{
                    //TakeDamage(20);
                    //Debug.Log(CurrentHP);

                    //TakeDamage(20);
                    //unitInfo.CurrentHP_Archer(currentHealth);
                //}
            }
           
        }
        
    }
    public void sol() {
        unitInfo.CurrentHP_Soldier(currentHealth);
    }
    public void archer() {
        unitInfo.CurrentHP_Archer(currentHealth);
    }
    public bool IsDying() {
        return isDying;
    }
}