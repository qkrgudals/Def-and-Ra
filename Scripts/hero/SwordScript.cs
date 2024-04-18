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

        //��Ŀ�� Ȱ��ȭ �����϶��� ��ų�ߵ�
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
            //��ųĵ������ ���콺��ġ�� �ٶ󺸰Ը���
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
            //��Ÿ���� �ƴҶ� ��ųŰ�� ������ ĵ������ ����
            if (!isCooldown1 && !isSkill1)
            {
                skill1Canvas.enabled = true;
                skill1Skillshot.enabled = true;
                isSkill1 = true;

                click.SkillOn(true); //��ų������ ���콺 Ŭ������
            }
            else//��ų���
            {
                skill1Canvas.enabled = false;
                skill1Skillshot.enabled = false;
                isSkill1 = false;
                click.SkillOn(false);
            }
        }

        //ĵ������ ���������϶� ���콺�� Ŭ���ϸ� ��ų�� ����
        if (skill1Skillshot.enabled && Input.GetMouseButtonDown(0))
        {
            mp = swordStats.mp;
            if (mp < skill1UseMp) //������ �����ϸ� �ߵ�����
            {
                skill1Canvas.enabled = false;
                skill1Skillshot.enabled = false;
                isSkill1 = false;
                click.SkillOn(false);
                return;
            }
            //���콺�� ���� ĳ���Ͱ� �ٶ�
            Quaternion rotationToLookAt = Quaternion.LookRotation(position -  transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref controller.rotateSpeedMovement, 0);

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            //���� �̵� ����
            controller.navMeshAgent.SetDestination(transform.position);
            controller.navMeshAgent.stoppingDistance = 1;

            anim.SetTrigger("DownAttack");

            //��Ÿ��
            isCooldown1 = true;
            skillImage1.fillAmount = 0;
            swordStats.UsedMP(skill1UseMp);

            skill1Canvas.enabled = false;
            skill1Skillshot.enabled = false;
        }
    }

    void Skill1Cooldown()
    {
        if (isCooldown1) //��ų��ٿ����
        {
            skillImage1.fillAmount += 1 / cooldown1 * Time.deltaTime; // 1�ʸ��� 1/��ٿ� ��ŭ ���־������� ������

            if (skillImage1.fillAmount >= 1) // �������������� 1����ũ�� 1�ΰ����ϰ� ��ٿ� ����
            {
                skillImage1.fillAmount = 1;
                isCooldown1 = false;
            }
        }
    }

    public void SpawnDownAttack() //��ų�ִϸ��̼��̺�Ʈ�� ȣ��
    {
        //��ų �ߵ���
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
        //��Ÿ���� �ƴҶ� ��ųŰ�������� ��ų�ߵ�
        if (Input.GetKeyDown(skill2) && !isCooldown2 && !isSkill2)
        {
            skill1Canvas.enabled = false;
            skill1Skillshot.enabled = false;
            isSkill1 = false;
            mp = swordStats.mp;
            if (mp < skill1UseMp) //������ �����ϸ� �ߵ�����
            {
                click.SkillOn(false);
                return;
            }
            //���� �̵� ����
            controller.navMeshAgent.SetDestination(transform.position);
            controller.navMeshAgent.stoppingDistance = 1;

            isSkill2 = true;
            skill1Canvas.enabled = false;
            skill1Skillshot.enabled = false;

            anim.SetTrigger("Tornado");
            //��Ÿ��
            isCooldown2 = true;
            skillImage2.fillAmount = 0;
            swordStats.UsedMP(skill2UseMp);

            click.SkillOn(true);
        }
    }

    void Skill2Cooldown()
    {
        if (isCooldown2) //��ų��ٿ����
        {
            skillImage2.fillAmount += 1 / cooldown2 * Time.deltaTime; // 1�ʸ��� 1/��ٿ� ��ŭ ���־������� ������

            if (skillImage2.fillAmount >= 1) // �������������� 1����ũ�� 1�ΰ����ϰ� ��ٿ� ����
            {
                skillImage2.fillAmount = 1;
                isCooldown2 = false;
            }
        }
    }

    public void SpawnTornado() //��ų�ִϸ��̼��̺�Ʈ�� ȣ��
    {
        StartCoroutine(Tornado()); //�ݺ��� ���� �ڸ�ƾȣ��
    }
    public IEnumerator Tornado()
    {
        int skill2damage = Mathf.RoundToInt(swordStats.attackdamage * (skill2damageX + skill2damageXUp * swordStats.lv) / (roll * oneRollAttack)); //�ѵ������� ��Ÿ�ݼ��� ����
        if (skill2damage == 0) skill2damage = 1;
        anim.speed = 0;
        for (int i = 0; i < roll * 360; i = i + 20) //roll * 360�� ������(ȸ��)
        {
            transform.eulerAngles = new Vector3(0, i, 0);
            if (i  % (360 / oneRollAttack) == 0) //360 / oneRollAttack ���� ��ų�����ռ�ȯ(�ѹ������� ��붧����)(360�� ������ ������ �ȳ����� �ȵ�)
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
