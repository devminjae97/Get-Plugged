using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    void ToInGameSceneCloseAnim()
    {
        if (SceneManager.GetActiveScene().name == "SelectStage")
        {
            Debug.Log("ToSceneYang");
            SceneManager.LoadScene("SceneYang");
        }
        else if (SceneManager.GetActiveScene().name == "SceneYang")
        {
            Debug.Log("ToSelectStage");
            SceneManager.LoadScene("SelectStage");
        }
    }
}
