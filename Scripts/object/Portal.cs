using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Portal : MonoBehaviour
{
    public string region;
    bool portalCollisionDetected = false;
    Vector3 portalDestination;
    GameObject playerObject;
    //UnitController controller;
    GameManager gameManager;
    VisualElement portalMoveWindow;
    public bool unitInfoDeactive; // 유닛 정보창 비활성화
    UnitController unitController;

    //private Portal portalScript;
    void Start()
    {
        //GameObject portalObject = GameObject.Find("portal");
        //portalScript = portalObject.GetComponent<Portal>();
        unitController = FindObjectOfType<UnitController>();
        //controller = GetComponent<UnitController>();
        unitInfoDeactive = false;
        gameManager = FindObjectOfType<GameManager>();

        var root = FindObjectOfType<UIDocument>().rootVisualElement;


        portalMoveWindow = root.Q<VisualElement>("PortalMoveWindow");

    }
    private void Update()
    {

        /*
        if (portalCollisionDetected) {
            Debug.Log(region); // 포탈이 어디에 위치하는지 확인하기 위해 출력합니다.

            // 특정 위치로 이동합니다.
            playerObject.transform.position = portalDestination;

            portalCollisionDetected = false; // 플래그를 재설정합니다.
        }
        */
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player") && (gameManager.reg == "world" 
            || gameManager.regionName == "world") && (collision.gameObject.GetComponent<UnitController>().Name == "SworldMaster"
            || collision.gameObject.GetComponent<UnitController>().Name == "Sorceress" ||
            collision.gameObject.GetComponent<UnitController>().Name == "Priest"))
        {
            Debug.Log("포탈 충돌");
            //portalCollisionDetected = true;
            PortalMoveWindow(true);

            unitInfoDeactive = true;
            //playerObject = collision.gameObject;
            //Invoke("unitInfoDeactiveDelayTime", 0.2f);
            //while (true) {

            //}
        }

        else if (collision.gameObject.CompareTag("Player") && (gameManager.regionName != "world")
            &&(gameManager.reg == "raid1" ||
            gameManager.reg == "raid2" || gameManager.reg == "raid3") && (collision.gameObject.GetComponent<UnitController>().Name == "SworldMaster"
            || collision.gameObject.GetComponent<UnitController>().Name == "Sorceress" ||
            collision.gameObject.GetComponent<UnitController>().Name == "Priest"))
        {
            Debug.Log("포탈 충돌");
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            //LoadingSceneController.LoadScene("World");
            gameManager.SceneMove(1);
            // 새로운 위치로 이동
            gameManager.reg = "world";
            if (collision.gameObject.GetComponent<UnitController>().Name == "SworldMaster") {
                collision.gameObject.GetComponent<SwordStats>().swregion = "world";
            }
            else if (collision.gameObject.GetComponent<UnitController>().Name == "Sorceress") {
                collision.gameObject.GetComponent<SorceressStats>().scregion = "world";

            }
            else if (collision.gameObject.GetComponent<UnitController>().Name == "Priest") {
                collision.gameObject.GetComponent<PriestStats>().prregion = "world";
            }
            collision.gameObject.transform.position = new Vector3(13f, -2f, 1f);
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            //unitController.navMeshAgent.enabled = true;
        }

    }
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

           
            switch (gameManager.reg)
            {
                /*
                case "world":
                    collision.gameObject.transform.position = new Vector3(13f, -2f, 1f);
                    //gameManager.regionName = "";
                    //Debug.Log("??");
                    break;
                */
                case "raid1":
                    if (collision.gameObject.GetComponent<UnitController>().Name == "SworldMaster") {
                        collision.gameObject.GetComponent<SwordStats>().swregion = "raid1";
                    }
                    else if (collision.gameObject.GetComponent<UnitController>().Name == "Sorceress") {
                        collision.gameObject.GetComponent<SorceressStats>().scregion = "raid1";

                    }
                    else if(collision.gameObject.GetComponent<UnitController>().Name == "Priest") {
                        collision.gameObject.GetComponent<PriestStats>().prregion = "raid1";
                    }
                    collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                    collision.gameObject.transform.position = new Vector3(4f, -1000f, 8f);
                    collision.gameObject.GetComponent<NavMeshAgent>().enabled = true;
                    unitInfoDeactive = false;
                    //Debug.Log("??");
                    break;
                case "raid2":
                    if (collision.gameObject.GetComponent<UnitController>().Name == "SworldMaster") {
                        collision.gameObject.GetComponent<SwordStats>().swregion = "raid2";
                    }
                    else if (collision.gameObject.GetComponent<UnitController>().Name == "Sorceress") {
                        collision.gameObject.GetComponent<SorceressStats>().scregion = "raid2";

                    }
                    else if (collision.gameObject.GetComponent<UnitController>().Name == "Priest") {
                        collision.gameObject.GetComponent<PriestStats>().prregion = "raid2";
                    }
                    collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                    collision.gameObject.transform.position = new Vector3(11900f, -30f, 150f);
                    //gameManager.regionName = "world";
                    collision.gameObject.GetComponent<NavMeshAgent>().enabled = true;
                    unitInfoDeactive = false;

                    break;
                case "raid3":
                    //Debug.Log("////");
                    if (collision.gameObject.GetComponent<UnitController>().Name == "SworldMaster") {
                        collision.gameObject.GetComponent<SwordStats>().swregion = "raid3";
                    }
                    else if (collision.gameObject.GetComponent<UnitController>().Name == "Sorceress") {
                        collision.gameObject.GetComponent<SorceressStats>().scregion = "raid3";

                    }
                    else if (collision.gameObject.GetComponent<UnitController>().Name == "Priest") {
                        collision.gameObject.GetComponent<PriestStats>().prregion = "raid3";
                    }
                    collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                    collision.gameObject.transform.position = new Vector3(-20000f, -35f, 180f);
                    //gameManager.regionName = "world";
                    collision.gameObject.GetComponent<NavMeshAgent>().enabled = true;
                    unitInfoDeactive = false;

                    break;

            }
            
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && (collision.gameObject.GetComponent<UnitController>().Name == "SworldMaster"
            || collision.gameObject.GetComponent<UnitController>().Name == "Sorceress" ||
            collision.gameObject.GetComponent<UnitController>().Name == "Priest"))
        {
            Debug.Log("포탈 비활성화");
            PortalMoveWindow(false);
            unitInfoDeactive = false;
        }
    }
    public void PortalMoveWindow(bool active)
    {
        if (active == true)
        {
            portalMoveWindow.style.display = DisplayStyle.Flex;
        }
        else
        {
            portalMoveWindow.style.display = DisplayStyle.None;
        }
    }

    public void unitInfoDeactiveDelayTime()
    {
        unitInfoDeactive = false;
    }

}
