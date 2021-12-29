using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour 
{ 

    private PlayerController playerController1;
    private PlayerController playerController2;

    // fix : yang 2021-12-29
    public List<GameObject> stages = new List<GameObject>();
    
    void Start() 
    {
        playerController1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerController2 = GameObject.Find("Player2").GetComponent<PlayerController>();

        // fix : yang 2021-12-29
        try
        {
            playerController1.transform.position = stages[LevelSelect.thisLevel - 1].transform.Find("StartFlag1").transform.position;
            playerController2.transform.position = stages[LevelSelect.thisLevel - 1].transform.Find("StartFlag2").transform.position;
        }
        catch (System.ArgumentOutOfRangeException e)
        {
            Debug.Log("Stage is not selected. Current Stage is Null.");
        }
    }

    void Update()
    {
        if (IsEndStage()) 
        {
            // Debug.Log("Done");

            // fix : yang 2021-12-29
            try
            {
                playerController1.transform.position = stages[LevelSelect.thisLevel].transform.Find("StartFlag1").transform.position;
                playerController2.transform.position = stages[LevelSelect.thisLevel].transform.Find("StartFlag2").transform.position;
                LevelSelect.thisLevel += 1;
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                Debug.Log("Lack of stages Array. Stage Array should be added.");
            }
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
