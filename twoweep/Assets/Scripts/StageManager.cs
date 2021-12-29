using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public int numOfStage;
    public RectTransform parentPanel;
    public GameObject stageButton;
    public Sprite lockedButton;

    int stageReached;
    //public static int thisLevel;

    private void Awake()
    {
        // for PlayerPrefs initialization code
        //PlayerPrefs.DeleteAll();
        // for PlayerPrefs testing code
        //PlayerPrefs.SetInt("stageReached", 2);

        Init();
        GenerateStageButtons();
    }

    void Init() 
    {
        if (PlayerPrefs.HasKey("stageReached")) {
            stageReached = PlayerPrefs.GetInt("stageReached");
        }
        else {
            PlayerPrefs.SetInt("stageReached", 1);
            stageReached = PlayerPrefs.GetInt("stageReached");
        }
    }

    void GenerateStageButtons()
    {
        for (int i = 0; i < numOfStage; i++)
        {
            int x = i + 1;

            GameObject stageButton = Instantiate(this.stageButton);
            Text buttonText = stageButton.GetComponentInChildren<Text>();

            stageButton.transform.SetParent(parentPanel, false);
            buttonText.text = (i + 1).ToString();

            stageButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate
            {
                SelectStage(x);
            });

            // Stage Lock Function
            if (i + 1 > stageReached)
            {
                stageButton.GetComponent<Button>().interactable = false;
                stageButton.GetComponent<Image>().sprite = lockedButton;
            }
        }
    }

    void SelectStage(int index)
    {
        PlayerPrefs.SetInt("stageSelected", index);
        SceneManager.LoadScene("InGameScene");
    }
}
