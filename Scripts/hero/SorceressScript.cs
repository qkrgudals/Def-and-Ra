using UnityEngine;
using UnityEngine.UI;

public class SorceressScript : MonoBehaviour
{
    public GameObject marker;
    public Canvas Ui;

    [Header("Skill1")]
    public Image skillImage1;
    public float cooldown1 = 5.0f;
    bool isCooldown1 = false;
    public KeyCode skill1;
    bool isSkill1 = false;
    public GameObject skill1Prefab;
    public Transform skill1SpawnPoint;
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

    public Canvas skill2Canvas;
    public Image skill2Range;
    public float maxSkill2Distance = 7;
    public GameObject skill2Prefab;
    public float skill2damageX;
    public float skill2damageXUp;
    public int skill2UseMp;

    private Vector3 skill2Position;

    private Vector3 position;
    private RaycastHit hit;
    private Ray ray;
    private Animator anim;
    private UnitController controller;
    private MouseClick click;
    private SorceressStats sorceressStats;
    private int mp;

    void Start()
    {
        skillImage1.fillAmount = 1;
        skillImage2.fillAmount = 1;

        skill1Skillshot.enabled = false;
        skill2Range.enabled = false;

        skill1Canvas.enabled = false;
        skill2Canvas.enabled = false;

        Ui.enabled = false;

        anim = GetComponent<Animator>();
        controller = GetComponent<UnitController>();
        click = FindObjectOfType<MouseClick>();
        sorceressStats = GetComponent<SorceressStats>();
        mp = sorceressStats.mp;
    }

    void Update()
    {
        if (marker == null)
        {
            return;
        }
        //마커가 활성화 상태일때만 스킬발동
        if (marker.activeSelf)
        {
            Ui.enabled = true;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Skill1();
            Skill2();

            Skill1Canvas();
            Skill2Canvas();
        }
        else
        {
            Ui.enabled = false;
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

    private void Skill2Canvas()
    {
        //스킬캔버스가 마우스위치를 따라감
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
        if (Input.GetKeyDown(skill1))
        {
            if (!isCooldown1 && !isSkill1) //스킬 조준
            {
                skill1Canvas.enabled = true;
                skill1Skillshot.enabled = true;
                isSkill1 = true;
                isSkill2 = false;

                skill2Canvas.enabled = false;
                skill2Range.enabled = false;

                Cursor.visible = true;

                click.SkillOn(true);
            }
            else //스킬취소
            {
                skill1Canvas.enabled = false;
                skill1Skillshot.enabled = false;
                isSkill1 = false;
                click.SkillOn(false);
            }
        }

        if (skill1Skillshot.enabled && Input.GetMouseButtonDown(0)) //스킬 발동
        {
            mp = sorceressStats.mp;
            if (mp < skill1UseMp) //마나가 부족하면 발동안함
            {
                skill1Canvas.enabled = false;
                skill1Skillshot.enabled = false;
                isSkill1 = false;
                click.SkillOn(false);
                return;
            }
            //쏘는방향을 바라보게함
            Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref controller.rotateSpeedMovement, 0);

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            controller.navMeshAgent.SetDestination(transform.position);
            controller.navMeshAgent.stoppingDistance = 1;


            anim.SetTrigger("FireBall");
            isCooldown1 = true;
            skillImage1.fillAmount = 0;
            sorceressStats.UsedMP(skill1UseMp);

            skill1Canvas.enabled = false;
            skill1Skillshot.enabled = false;
        }
    }

    void Skill1Cooldown()
    {
        //쿨다운 코드
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

    public void SpawnFireBall()
    {
        //스킬 발동됨
        int skill1damage = Mathf.RoundToInt(sorceressStats.attackdamage * (skill1damageX + skill1damageXUp * (sorceressStats.lv - 1)));
        isSkill1 = false;
        click.SkillOn(false);
        GameObject instance = Instantiate(skill1Prefab, transform.position, transform.rotation);
        FireBallProjectile fireBallProjectile = instance.GetComponent<FireBallProjectile>();
        fireBallProjectile.SetCaster(gameObject);
        if (fireBallProjectile != null)
        {
            fireBallProjectile.damage = skill1damage;
        }
    }

    void Skill2()
    {
        if (Input.GetKeyDown(skill2))
        {
            if (!isCooldown2 && !isSkill2) //스킬조준
            {
                skill2Canvas.enabled = true;
                skill2Range.enabled = true;
                isSkill2 = true;
                isSkill1 = false;

                skill1Canvas.enabled = false;
                skill1Skillshot.enabled = false;

                Cursor.visible = false;

                click.SkillOn(true);
            }
            else//스킬취소
            {
                skill2Canvas.enabled = false;
                skill2Range.enabled = false;
                isSkill2 = false;
                click.SkillOn(false);
                Cursor.visible = true;
            }
        }

        if (skill2Range.enabled && Input.GetMouseButtonDown(0)) //스킬 발동
        {
            mp = sorceressStats.mp;
            if (mp < skill2UseMp) //마나가 부족하면 발동안함
            {
                skill2Canvas.enabled = false;
                skill2Range.enabled = false;
                isSkill2 = false;
                click.SkillOn(false);
                Cursor.visible = true;
                return;
            }
            Quaternion rotationToLookAt2 = Quaternion.LookRotation(position - transform.position);
            float rotationY2 = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt2.eulerAngles.y, ref controller.rotateSpeedMovement, 0);

            transform.eulerAngles = new Vector3(0, rotationY2, 0);

            controller.navMeshAgent.SetDestination(transform.position);
            controller.navMeshAgent.stoppingDistance = 1;

            anim.SetTrigger("Explosion");
            isCooldown2 = true;
            skillImage2.fillAmount = 0;
            sorceressStats.UsedMP(skill2UseMp);

            skill2Position = position;//스킬이 현재 마우스 위치에 나오게고정

            skill2Canvas.enabled = false;
            skill2Range.enabled = false;

            Cursor.visible = true;
        }


    }

    void Skill2Cooldown() //쿨다운
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

    public void SpawnExplosion() //스킬발동됨
    {
        int skill2damage = Mathf.RoundToInt(sorceressStats.attackdamage * (skill2damageX + skill2damageXUp * (sorceressStats.lv - 1)));
        isSkill2 = false;
        click.SkillOn(false);
        GameObject instance = Instantiate(skill2Prefab, skill2Position, transform.rotation);
        ExplosionProjectile explosionProjectile = instance.GetComponent<ExplosionProjectile>();
        explosionProjectile.SetCaster(gameObject);
        if (explosionProjectile != null)
        {
            explosionProjectile.damage = skill2damage;
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }
}
