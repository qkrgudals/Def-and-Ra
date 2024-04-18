using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public int maxHealth = 100;  // �ִ� ü��
    public int currentHealth;     // ���� ü��
    private Animator animator;    // Animator ������Ʈ ����

    public GameObject UnitMarker;
    private bool isDying = false;  // �״� ������ ����
    public event Action OnCharacterDeath;
    public UnitInfo unitInfo;
    public Slider hpSlider;


    void Start() {
        unitInfo = FindObjectOfType<UnitInfo>();
        currentHealth = maxHealth;  // ���� ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
        animator = GetComponent<Animator>();
        // Animator ������Ʈ ����
        if (hpSlider != null) {
            hpSlider.maxValue = maxHealth;
        }
   
    }

    // ���ظ� ���� �� ȣ���� �޼���
    public void TakeDamage(int damage) {
        currentHealth -= damage;

        // HP ����
        if (hpSlider != null)
            hpSlider.value = currentHealth;

        
        // ü���� 0 ���Ϸ� �������� ���� ó��
        animator = GetComponent<Animator>();
        if (currentHealth <= 0 && !isDying) {
            Die();
            isDying = true;
            currentHealth = 0;
        }
    }

    // ��� �� ȣ���� �޼���
    void Die() {
        // ���⿡ ��� ó���� �߰��� �� �ֽ��ϴ�.
        // ���� ���, ��� �Ҹ� ���, ���� ���� ȭ�� ǥ�� ��
        // �״� �ִϸ��̼� ���
        if (animator != null) {
            animator.SetTrigger("Death");  // "Death" Ʈ���Ÿ� �ߵ����� �״� �ִϸ��̼��� ���
        }

        // ���⿡ �߰����� ��� ó���� �߰��� �� �ֽ��ϴ�.
        // ���� ���, ��� �Ҹ� ���, ���� ���� ȭ�� ǥ�� ��
        OnCharacterDeath?.Invoke();
        StartCoroutine(DestroyAfterAnimation());
    }

    
    IEnumerator DestroyAfterAnimation() {
        if (animator != null)
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);  // �ִϸ��̼� ���̸�ŭ ���
            //Time.timeScale = 1f;  // �ð� ��� �ӵ��� �ٽ� ������� ����
            // ���� �ı�
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
               // if (Input.GetKeyDown(KeyCode.Y)) //Y ��ư�� ������ ������ HP�� ���̴��� �׽�Ʈ 
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