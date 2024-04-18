using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PriestScript : MonoBehaviour
{
    public GameObject marker;
    public Canvas skillUi;

    [Header("Skill1")]
    public Image skillImage1;
    public float cooldown1 = 5.0f;
    bool isCooldown1 = false;
    public KeyCode skill1;
    public bool isSkill1 = false;
    public int skill1UseMp;

    [Header("Skill2")]
    public Image skillImage2;
    public float cooldown2 = 20.0f;
    bool isCooldown2 = false;
    public KeyCode skill2;
    public bool isSkill2 = false;

    public Canvas skill2Canvas;
    public Image skill2Range;
    public float maxSkill2Distance = 7;
    public GameObject skill2Prefab;
    public float skill2HealX;
    public float skill2HealXUp;
    public int skill2UseMp;

    private Vector3 skill2Position;

    private Vector3 position;
    private RaycastHit hit;
    private Ray ray;
    private Animator anim;
    private UnitController controller;
    private MouseClick click;
    private PriestStats priestStats;
    private int mp;

    void Start()
    {
        skillImage1.fillAmount = 1;
        skillImage2.fillAmount = 1;

        skill2Range.enabled = false;

        skill2Canvas.enabled = false;

        skillUi.enabled = false;

        anim = GetComponent<Animator>();
        controller = GetComponent<UnitController>();
        click = FindObjectOfType<MouseClick>();
        priestStats = GetComponent<PriestStats>();
        mp = priestStats.mp;
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

            Skill2Canvas();
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

    private void Skill2Canvas()
    {
        //��ųĵ������ ���콺��ġ�� ����
        int layerMask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject != this.gameObject)
            {
                position = hit.point;
            }
        }

        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxSkill2Distance);

        var newHitPos = transform.position + hitPosDir * distance;
        skill2Canvas.transform.position = (newHitPos);
    }
    void Skill1()
    {
        if (Input.GetKeyDown(skill1) && !isCooldown1 && !isSkill1) //��ų �ߵ�
        {
            skill2Canvas.enabled = false;
            skill2Range.enabled = false;
            Cursor.visible = true;
            isSkill2 = false;

            mp = priestStats.mp;
            if (mp < skill1UseMp) //������ �����ϸ� �ߵ�����
            {
                click.SkillOn(false);
                return;
            }
            controller.navMeshAgent.SetDestination(transform.position);
            controller.navMeshAgent.stoppingDistance = 1;

            anim.SetTrigger("PowerUp");
            isCooldown1 = true;
            skillImage1.fillAmount = 0;
            priestStats.UsedMP(skill1UseMp);

            isSkill1 = true;

            click.SkillOn(true);
        }
    }

    void Skill1Cooldown()
    {
        //��ٿ� �ڵ�
        if (isCooldown1)
        {
            skillImage1.fillAmount += 1 / cooldown1 * Time.deltaTime;

            if (skillImage1.fillAmount >= 1)
            {
                skillImage1.fillAmount = 1;
                isCooldown1 = false;
            }
        }
    }

    public void SpawnPowerUp() //��ų�ִϸ��̼��̺�Ʈ�� ȣ��
    {
        StartCoroutine(PowerUp()); //�ݺ��� ���� �ڸ�ƾȣ��
    }
    public IEnumerator PowerUp()
    {
        //��ų �ߵ���
        isSkill1 = false;
        click.SkillOn(false);
        
        float originAttackSpeed = GetComponent<BasicStats>().attackSpeed;
        GetComponent<BasicStats>().attackSpeed *= 2;
        yield return new WaitForSeconds(10);
        GetComponent<BasicStats>().attackSpeed = originAttackSpeed;
    }

    void Skill2()
    {
        if (Input.GetKeyDown(skill2) && !isSkill1)
        {
            if (!isCooldown2 && !isSkill2) //��ų����
            {
                skill2Canvas.enabled = true;
                skill2Range.enabled = true;

                Cursor.visible = false;

                isSkill2 = true;
                click.SkillOn(true);
            }
            else //��ų���
            {
                skill2Canvas.enabled = false;
                skill2Range.enabled = false;
                Cursor.visible = true;
                isSkill2 = false;
                click.SkillOn(false);
            }
        }

        if (skill2Range.enabled && Input.GetMouseButtonDown(0)) //��ų �ߵ�
        {
            mp = priestStats.mp;
            if (mp < skill1UseMp) //������ �����ϸ� �ߵ�����
            {
                skill2Canvas.enabled = false;
                skill2Range.enabled = false;
                Cursor.visible = true;
                isSkill2 = false;
                click.SkillOn(false);
                return;
            }
            Quaternion rotationToLookAt2 = Quaternion.LookRotation(position - transform.position);
            float rotationY2 = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt2.eulerAngles.y, ref controller.rotateSpeedMovement, 0);

            transform.eulerAngles = new Vector3(0, rotationY2, 0);

            controller.navMeshAgent.SetDestination(transform.position);
            controller.navMeshAgent.stoppingDistance = 1;

            anim.SetTrigger("Heal");
            isCooldown2 = true;
            skillImage2.fillAmount = 0;
            priestStats.UsedMP(skill2UseMp);

            skill2Position = position;//��ų�� ���� ���콺 ��ġ�� �����԰���

            skill2Canvas.enabled = false;
            skill2Range.enabled = false;

            Cursor.visible = true;
        }
    }

    void Skill2Cooldown() //��ٿ�
    {
        if (isCooldown2)
        {
            skillImage2.fillAmount += 1 / cooldown2 * Time.deltaTime;

            if (skillImage2.fillAmount >= 1)
            {
                skillImage2.fillAmount = 1;
                isCooldown2 = false;
            }
        }
    }

    public void SpawnHeal() //��ų�ߵ���
    {
        int heal = Mathf.RoundToInt(priestStats.attackdamage * (skill2HealX + skill2HealXUp * priestStats.lv));
        isSkill2 = false;
        click.SkillOn(false);
        GameObject instance = Instantiate(skill2Prefab, skill2Position, transform.rotation);
        HealProjectile healProjectile = instance.GetComponent<HealProjectile>();
        healProjectile.SetCaster(gameObject);
        if (healProjectile != null)
        {
            healProjectile.heal = heal;
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }
}
