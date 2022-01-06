using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour 
{
    [SerializeField] private GameObject menuSet;

    private SFXManager sfxManager;

    private PlayerController playerController1;
    private PlayerController playerController2;

    private CameraController cameraController1;
    private CameraController cameraController2;

    // serializefield로 바꾸거나 코드로 받아오기
    public List<GameObject> stages = new List<GameObject>();

    private bool isCleaningStage;

    void Start() 
    {
        menuSet.SetActive(false);

        sfxManager = GameObject.Find("SFXM").GetComponent<SFXManager>();

        playerController1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerController2 = GameObject.Find("Player2").GetComponent<PlayerController>();

        cameraController1 = GameObject.Find("Camera1").GetComponent<CameraController>();
        cameraController2 = GameObject.Find("Camera2").GetComponent<CameraController>();

        isCleaningStage = false;

        ReadyStage();
    }

    void Update() 
    {
        if (IsStageFinished()) 
        {
            // Debug.Log("Done");

            // confirmation window 생기면 ie말고 그냥 function으로 수정
            if(!isCleaningStage)
            {
                isCleaningStage = true;
                StartCoroutine(IECleanStage());
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            //Submenu
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
    }

    // 임시로 ie로함
    IEnumerator IECleanStage()
    {

        // stop players
        playerController1.SetPlayerControllability(false);
        playerController2.SetPlayerControllability(false);

        // disable camera trace
        cameraController1.SetCameraTrace(false);
        cameraController2.SetCameraTrace(false);

        // unlock stage
        PlayerPrefs.SetInt("stageReached", PlayerPrefs.GetInt("stageReached") + 1);

        // show confirmation window
        //asdf
        // temp

        sfxManager.PlaySFXPlug();

        playerController1.Plug(true);
        playerController2.Plug(true);

        yield return new WaitForSeconds(0.5f);

        sfxManager.PlaySFXClear();

        // 5초뒤
        yield return new WaitForSeconds(2);



        // set next stage
        PlayerPrefs.SetInt("stageSelected", PlayerPrefs.GetInt("stageSelected") + 1);


        // ready stage
        ReadyStage();

        isCleaningStage = false;
    }

    void ReadyStage() 
    {
        // get & set num of next stage
        SetPlayerReady(PlayerPrefs.GetInt("stageSelected"));
        
        // set camera position
        cameraController1.SetCameraPos(stages[PlayerPrefs.GetInt("stageSelected") - 1].transform.position.y);
        cameraController2.SetCameraPos(stages[PlayerPrefs.GetInt("stageSelected") - 1].transform.position.y);

        // enable camera trace
        cameraController1.SetCameraTrace(true);
        cameraController2.SetCameraTrace(true);

        // start players
        playerController1.SetPlayerControllability(true);
        playerController2.SetPlayerControllability(true);
    }

    void SetPlayerReady(int currentStage) {

        try 
        {    
            playerController1.Plug(false);
            playerController2.Plug(false);
        
            playerController1.SetStartPoint(stages[currentStage - 1].transform.Find("StartFlag1").gameObject);
            playerController2.SetStartPoint(stages[currentStage - 1].transform.Find("StartFlag2").gameObject);

            playerController1.Init();
            playerController2.Init();
        }
        catch (System.ArgumentOutOfRangeException) 
        {
            Debug.Log("Stage is not selected. or Stage is Empty.");
        }
    }

    public void RespawnPlayers() 
    {
        //sfxManager.PlaySFXDead();

        playerController1.Init();
        playerController2.Init();

        playerController1.SetPlayerControllability(true);
        playerController2.SetPlayerControllability(true);
    }

    bool IsStageFinished() 
    {
        return playerController1.GetIsOnGoalFlag() && playerController2.GetIsOnGoalFlag();
    }
}
