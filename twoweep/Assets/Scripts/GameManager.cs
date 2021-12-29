using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour 
{ 

    private PlayerController playerController1;
    private PlayerController playerController2;

    private CameraController cameraController1;
    private CameraController cameraController2;
    
    public List<GameObject> stages = new List<GameObject>();
    
    void Start() 
    {
        playerController1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerController2 = GameObject.Find("Player2").GetComponent<PlayerController>();

        cameraController1 = GameObject.Find("Camera1").GetComponent<CameraController>();
        cameraController2 = GameObject.Find("Camera2").GetComponent<CameraController>();

        ReadyStage();
    }

    void Update()
    {
        if (IsEndStage()) 
        {
            // Debug.Log("Done");

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


            // set next stage
            PlayerPrefs.SetInt("stageSelected", PlayerPrefs.GetInt("stageSelected") + 1);

            // ready stage
            ReadyStage();
        }
    }

    void ReadyStage() 
    {
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

    void SetPlayerReady(int currentStage) 
    {

        try 
        {
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
        playerController1.Init();
        playerController2.Init();

        playerController1.SetPlayerControllability(true);
        playerController2.SetPlayerControllability(true);
    }

    bool IsEndStage()
    {
        return playerController1.GetIsOnGoalFlag() && playerController2.GetIsOnGoalFlag();
    }
}
