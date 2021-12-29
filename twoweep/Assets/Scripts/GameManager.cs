using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{ 

    private PlayerController playerController1;
    private PlayerController playerController2;

    // fix : yang 2021-12-29
    public List<GameObject> startFlags = new List<GameObject>();
    
    void Start() 
    {
        playerController1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        playerController2 = GameObject.Find("Player2").GetComponent<PlayerController>();

        // fix : yang 2021-12-29
        playerController1.transform.position = startFlags[LevelSelect.thisLevel * 2 - 2].transform.position;
        playerController2.transform.position = startFlags[LevelSelect.thisLevel * 2 - 1].transform.position;
    }

    void Update()
    {
        if (IsEndStage()) 
        {
            Debug.Log("Done");

            // fix : yang 2021-12-29
            playerController1.transform.position = startFlags[(LevelSelect.thisLevel + 1) * 2 - 2].transform.position;
            playerController2.transform.position = startFlags[(LevelSelect.thisLevel + 1) * 2 - 1].transform.position;
            LevelSelect.thisLevel += 1;
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
