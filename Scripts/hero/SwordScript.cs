using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwordScript : MonoBehaviour
{
    public GameObject marker;
    public Canvas skillUi;

    [Header("Skill1")]
    public Image skillImage1;
    public float cooldown1 = 5.0f;
    bool isCooldown1 = false;
    bool isSkill1 = false;
    public KeyCode skill1;
    public GameObject skill1Prefab;
    public float skill1damageX;
    public float skill1damageXUp;
    public int skill1UseMp;

    public Canvas skill1Canvas;
    public Image skill1Skillshot;

    [Header("Skill2")]
    public Image skillImage2;
    public float cooldown2 = 20.0f;
    bool isCooldown2 = false;
    public KeyCode skill2;
    bool isSkill2 = false;

    public int roll = 8;
    public int oneRollAttack = 2;
    public GameObject skill2Prefab;
    public float skill2damageX;
    public float skill2damageXUp;
    public int skill2UseMp;

    private Vector3 position;
    private RaycastHit hit;
    private Ray ray;
    private Animator anim;
    private UnitController controller;
    private MouseClick click;
    private SwordStats swordStats;
    private int mp;


    void Start()
    {
        skillImage1.fillAmount = 1;
        skillImage2.fillAmount = 1;

        skill1Skillshot.enabled = false;

        skill1Canvas.enabled = false;

        skillUi.enabled = false;

        anim = GetComponent<Animator>();
        controller = GetComponent<UnitController>();
        click = FindObjectOfType<MouseClick>();
        swordStats = GetComponent<SwordStats>();
        mp = swordStats.mp;
    }

    public void Update()
    {
        if (marker == null)
        {
            return;
        }

        //마커가 활성화 상태일때만 스킬발동
        if (marker.activeSelf)
        {
            skillUi.enabled = true;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Skill1();
            Skill2();

            Skill1Canvas();
        }
        else
        {
            skillUi.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        Skill1Cooldown();
        Skill2Cooldown();
    }

    private void Skill1Canvas()
    {
        if (skill1Skillshot != null)
        {
            //스킬캔버스가 마우스위치를 바라보게만듦
            int layerMask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            Quaternion sk1Canvas = Quaternion.LookRotation(position - transform.position);
            sk1Canvas.eulerAngles = new Vector3(0, sk1Canvas.eulerAngles.y, sk1Canvas.eulerAngles.z);

            skill1Canvas.transform.rotation = Quaternion.Lerp(sk1Canvas, skill1Canvas.transform.rotation, 0);
        }
    }

    void Skill1()
    {
        if (Input.GetKeyDown(skill1) && !isSkill2)
        {
            //쿨타임이 아닐때 스킬키를 누르면 캔버스가 켜짐
            if (!isCooldown1 && !isSkill1)
            {
                skill1Canvas.enabled = true;
                skill1Skillshot.enabled = true;
                isSkill1 = true;

                click.SkillOn(true); //스킬조준중 마우스 클릭방지
            }
            else//스킬취소
            {
                skill1Canvas.enabled = false;
                skill1Skillshot.enabled = false;
                isSkill1 = false;
                click.SkillOn(false);
            }
        }

        //캔버스가 켜진상태일때 마우스를 클릭하면 스킬이 사용됨
        if (skill1Skillshot.enabled && Input.GetMouseButtonDown(0))
        {
            mp = swordStats.mp;
            if (mp < skill1UseMp) //마나가 부족하면 발동안함
            {
                skill1Canvas.enabled = false;
                skill1Skillshot.enabled = false;
                isSkill1 = false;
                click.SkillOn(false);
                return;
            }
            //마우스를 향해 캐릭터가 바라봄
            Quaternion rotationToLookAt = Quaternion.LookRotation(position -  transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref controller.rotateSpeedMovement, 0);

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            //유닛 이동 멈춤
            controller.navMeshAgent.SetDestination(transform.position);
            controller.navMeshAgent.stoppingDistance = 1;

            anim.SetTrigger("DownAttack");

            //쿨타임
            isCooldown1 = true;
            skillImage1.fillAmount = 0;
            swordStats.UsedMP(skill1UseMp);

            skill1Canvas.enabled = false;
            skill1Skillshot.enabled = false;
        }
    }

    void Skill1Cooldown()
    {
        if (isCooldown1) //스킬쿨다운실행
        {
            skillImage1.fillAmount += 1 / cooldown1 * Time.deltaTime; // 1초마다 1/쿨다운 만큼 유닛아이콘이 차오름

            if (skillImage1.fillAmount >= 1) // 아이콘차오른게 1보다크면 1로고정하고 쿨다운 종료
            {
                skillImage1.fillAmount = 1;
                isCooldown1 = false;
            }
        }
    }

    public void SpawnDownAttack() //스킬애니메이션이벤트로 호출
    {
        //스킬 발동됨
        int skill1damage = Mathf.RoundToInt(swordStats.attackdamage * (skill1damageX + skill1damageXUp * swordStats.lv));
        isSkill1 = false;
        click.SkillOn(false);
        GameObject instance = Instantiate(skill1Prefab, transform.position, transform.rotation);
        DownAttackProjectile downAttack = instance.GetComponent<DownAttackProjectile>();
        downAttack.SetCaster(gameObject);
        if (downAttack != null)
        {
            downAttack.damage = skill1damage;
        }
        click.SkillOn(false);
    }

    void Skill2()
    {
        //쿨타임이 아닐때 스킬키를누르면 스킬발동
        if (Input.GetKeyDown(skill2) && !isCooldown2 && !isSkill2)
        {
            skill1Canvas.enabled = false;
            skill1Skillshot.enabled = false;
            isSkill1 = false;
            mp = swordStats.mp;
            if (mp < skill1UseMp) //마나가 부족하면 발동안함
            {
                click.SkillOn(false);
                return;
            }
            //유닛 이동 멈춤
            controller.navMeshAgent.SetDestination(transform.position);
            controller.navMeshAgent.stoppingDistance = 1;

            isSkill2 = true;
            skill1Canvas.enabled = false;
            skill1Skillshot.enabled = false;

            anim.SetTrigger("Tornado");
            //쿨타임
            isCooldown2 = true;
            skillImage2.fillAmount = 0;
            swordStats.UsedMP(skill2UseMp);

            click.SkillOn(true);
        }
    }

    void Skill2Cooldown()
    {
        if (isCooldown2) //스킬쿨다운실행
        {
            skillImage2.fillAmount += 1 / cooldown2 * Time.deltaTime; // 1초마다 1/쿨다운 만큼 유닛아이콘이 차오름

            if (skillImage2.fillAmount >= 1) // 아이콘차오른게 1보다크면 1로고정하고 쿨다운 종료
            {
                skillImage2.fillAmount = 1;
                isCooldown2 = false;
            }
        }
    }

    public void SpawnTornado() //스킬애니메이션이벤트로 호출
    {
        StartCoroutine(Tornado()); //반복을 위해 코르틴호출
    }
    public IEnumerator Tornado()
    {
        int skill2damage = Mathf.RoundToInt(swordStats.attackdamage * (skill2damageX + skill2damageXUp * swordStats.lv) / (roll * oneRollAttack)); //총데미지에 총타격수를 나눔
        if (skill2damage == 0) skill2damage = 1;
        anim.speed = 0;
        for (int i = 0; i < roll * 360; i = i + 20) //roll * 360도 돌리기(회전)
        {
            transform.eulerAngles = new Vector3(0, i, 0);
            if (i  % (360 / oneRollAttack) == 0) //360 / oneRollAttack 마다 스킬프리팹소환(한바퀴돌때 몇대때릴지)(360을 나눌때 정수가 안나오면 안됨)
            {
                GameObject instance = Instantiate(skill2Prefab, transform.position, transform.rotation);
                TornadoProjectile Tornado = instance.GetComponent<TornadoProjectile>();
                Tornado.SetCaster(gameObject);
                if (Tornado != null)
                {
                    Tornado.damage = skill2damage;
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        anim.speed = 1;
        isSkill2 = false;
        click.SkillOn(false);
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }
}
