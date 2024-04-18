using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillScript : MonoBehaviour
{
    public GameObject marker;

    [Header("Skill1")]
    public Image skillImage1;
    public float cooldown1 = 5.0f;
    bool isCooldown1 = false;
    public KeyCode skill1;
    public bool isSkill1 = false;
    public GameObject skill1Prefab;
    public Transform skill1SpawnPoint;


    public Canvas skill1Canvas;
    public Image skill1Skillshot;
    private Transform skill1transform;

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
    private Transform skill2transform;

    private Vector3 position;
    private RaycastHit hit;
    private Ray ray;
    private Animator anim;
    private UnitController controller;
    private MouseClick click;



    void Start()
    {
        skillImage1.fillAmount = 1;
        skillImage2.fillAmount = 1;

        skill1Skillshot.enabled = false;
        skill2Range.enabled = false;

        skill1Canvas.enabled = false;
        skill2Canvas.enabled = false;

        anim = GetComponent<Animator>();
        controller = GetComponent<UnitController>();
        click = FindObjectOfType<MouseClick>();
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
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Skill1();
            Skill2();

            Skill1Canvas();
            Skill2Canvas();
        }

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
        if (Input.GetKey(skill1) && !isCooldown1 && !isSkill2) //��ų ����
        {
            skill1Canvas.enabled = true;
            skill1Skillshot.enabled = true;
            isSkill1 = true;

            skill2Canvas.enabled = false;
            skill2Range.enabled = false;

            Cursor.visible = true;

            click.SkillOn(true);
        }

        if (skill1Skillshot.enabled && Input.GetMouseButtonDown(0)) //��ų �ߵ�
        {
            //��¹����� �ٶ󺸰���
            Quaternion rotationToLookAt = Quaternion.LookRotation(position -  transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref controller.rotateSpeedMovement, 0);

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            controller.navMeshAgent.SetDestination(transform.position);
            controller.navMeshAgent.stoppingDistance = 1;

            anim.SetTrigger("FireBall");
            isCooldown1 = true;
            skillImage1.fillAmount = 0;

            skill1Canvas.enabled = false;
            skill1Skillshot.enabled = false;

            skill1transform.rotation = skill1Canvas.transform.rotation; //��ų ���󰡴¹����� ĵ���� �����̶� �����ϰԸ���
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

    public void SpawnFireBall()
    {
        //��ų �ߵ���
        isSkill1 = false;
        click.SkillOn(false);
        Instantiate(skill1Prefab, skill1SpawnPoint.transform.position, skill1transform.rotation);
    }

    void Skill2()
    {
        if (Input.GetKey(skill2) && !isCooldown2 && !isSkill1) //��ų����
        {
            skill2Canvas.enabled = true;
            skill2Range.enabled = true;
            isSkill2 = true;

            skill1Canvas.enabled = false;
            skill1Skillshot.enabled = false;

            Cursor.visible = false;
                        
            click.SkillOn(true);
        }

        if (skill2Range.enabled && Input.GetMouseButtonDown(0)) //��ų �ߵ�
        {
            Quaternion rotationToLookAt2 = Quaternion.LookRotation(position - transform.position);
            float rotationY2 = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt2.eulerAngles.y, ref controller.rotateSpeedMovement, 0);

            transform.eulerAngles = new Vector3(0, rotationY2, 0);

            controller.navMeshAgent.SetDestination(transform.position);
            controller.navMeshAgent.stoppingDistance = 1;

            anim.SetTrigger("Explosion");
            isCooldown2 = true;
            skillImage2.fillAmount = 0;

            skill2Canvas.enabled = false;
            skill2Range.enabled = false;

            skill2transform = skill2Canvas.transform; //��ų�� ���� ĵ���� ��ġ�� �����԰���

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

    public void SpawnExplosion() //��ų�ߵ���
    {
        isSkill2 = false;
        click.SkillOn(false);
        Instantiate(skill2Prefab, skill2transform.position, skill2transform.rotation);
    }
}
