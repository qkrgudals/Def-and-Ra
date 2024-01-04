using TMPro;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
	[SerializeField]
	private	GameObject		unitMarker; // 유닛 선택을 표시하는 마커
	private	NavMeshAgent	navMeshAgent; // 유닛의 이동을 담당하는 변수
	Animator animator;
    EnemyHP enemy; // 체력바를 달고있는 적
    float distanceToEnemy; // 적과의 거리

    private float attackRange = 1.5f; // 유닛 공격 범위
    public int damageAmount = 20; // 적에게 데미지를 가하는 양
    public bool startAttack = false; // 공격상태인지 판별
    public bool startWalking = false;

    private GameObject targetPointerPrefab; // 유닛의 타겟 포인터에 쓰일 프리팹
    private GameObject targetPointer; // 타겟 포인터
    RTSUnitController rtsCt; // RTSUnitController의 클래스를 가져옴

    private void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트를 가져옴
        animator = GetComponent<Animator>();
        rtsCt = FindObjectOfType<RTSUnitController>();
        targetPointerPrefab = Resources.Load<GameObject>("Target Pointer"); // Resources 폴더 내부에서 Target Pointer 프리팹을 가져옴
        enemy = FindObjectOfType<EnemyHP>();

    }
   
    void Update()
    {

        if (navMeshAgent.velocity.magnitude >= 0.01f) // 유닛의 실제 이동속도가 0.1 이상일 때
        {
            animator.SetBool("SwordAttack", false);
            animator.SetBool("Walking", true); // 걷기
        }
        else
        {
            animator.SetBool("Walking", false); // 아닐경우 멈추기
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange); // 유닛의 위치와 공격범위를 계산해서 원형 범위내에 적이 충돌하는지 검사

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // 플레이어와 적과의 거리를 계산 (타켓포인터로 계산)
                //distanceToEnemy = Vector3.Distance(transform.position, targetPointer.transform.position);
                distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                //enemy = collider.GetComponent<EnemyHP>();

                // 적과의 거리 내에 있을 경우 공격 실행
                if (distanceToEnemy <= attackRange)
                {
                    animator.SetBool("SwordAttack", true);
                    animator.SetBool("Walking", false);
                    

                    if (!startAttack) // 공격상태가 아닐시 코루틴 시작
                    {
                        navMeshAgent.speed = 0; // 유닛의 스피드
                        StartCoroutine(AttackEnemyRepeatedly());
                        startAttack = true;
                    }
                    
                }
                else
                {
                    animator.SetBool("SwordAttack", false);
                    animator.SetBool("Walking", true);
                    startAttack = false;
                }
            }
        }
    }
    IEnumerator AttackEnemyRepeatedly()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f); // 1.5초마다 적이 사정거리 내에 있으면 공격

            if (enemy!= null && startAttack == true)
            {
                enemy.TakeDamage(damageAmount);
                Debug.Log("공격");
            }
            else if(enemy == null )
            {
                navMeshAgent.speed = 2.5f;
                animator.SetBool("SwordAttack",  false); // 적이 죽으면 코루틴 종료
                break;
            }
        }
    }

    public void SelectUnit() // 해당 유닛을 선택할시
	{
		unitMarker.SetActive(true); //유닛의 마커를 활성화 시킴
        animator.SetBool("Preparation", true); // 전투 준비자세 모션을 취함
    }

	public void DeselectUnit() // 해당 유닛을 선택 해제할시
	{
		unitMarker.SetActive(false); // 유닛의 마커를 비활성화 시킴
        animator.SetBool("SwordAttack", false);
        animator.SetBool("Preparation", false);
    }

	public void MoveTo(Vector3 end) // 해당 유닛이 움직일 시
	{
        end = end + Random.insideUnitSphere * (rtsCt.selectedUnitList.Count) * 0.3f; // 선택된 유닛이 많을수록 타겟 포인트 범위 증가 
        end = new Vector3(end.x, 0.1f, end.z); // 타겟 포인트 위치 재정렬

        if (targetPointer != null)
        {
            Destroy(targetPointer);
        }
        // 목표 위치에 타겟 포인터 생성
        targetPointer = Instantiate(targetPointerPrefab, end, Quaternion.identity);

        navMeshAgent.SetDestination(end);

        //animator.SetBool("Walking", true);
        
    }
   
}

