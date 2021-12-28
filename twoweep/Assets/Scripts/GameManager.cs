using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{ 

    private PlayerController playerController1;
    private PlayerController playerController2;
    
    void Start() 
    {
        Debug.Log("TEST!");
        playerController1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerController2 = GameObject.Find("Player2").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsEndStage()) 
        {
            Debug.Log("Done");
        }
    }

    public void Reset() 
    {
        playerController1.Init();
        playerController2.Init();
    }

    bool IsEndStage()
    {
        return playerController1.GetIsOnGoalFlag() && playerController2.GetIsOnGoalFlag();
    }
}
