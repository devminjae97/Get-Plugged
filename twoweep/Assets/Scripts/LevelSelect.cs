using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public int noOfLevels;
    public GameObject levelButton;
    public RectTransform parentPanel;
    int levelReached;
    public static int thisLevel;

    private void Awake()
    {
        var objs = FindObjectsOfType<LevelSelect>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LevelButtons();
    }

    void LevelButtons()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            levelReached = PlayerPrefs.GetInt("level");
        }
        else
        {
            PlayerPrefs.SetInt("level", 1);
            levelReached = PlayerPrefs.GetInt("level");
        }

        for (int i=0; i<noOfLevels; i++)
        {
            int x = new int();
            x = i + 1;

            GameObject lvlButton = Instantiate(levelButton);
            lvlButton.transform.SetParent(parentPanel, false);
            Text buttonText = lvlButton.GetComponentInChildren<Text>();
            buttonText.text = (i + 1).ToString();

            lvlButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate
            {
                thisLevel = x;
                LevelSelected(x);
            });

            // Stage Lock Function
            if (i+1 > levelReached)
            {
                lvlButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    void LevelSelected(int index)
    {
        PlayerPrefs.SetInt("levelSelected", index);
        LoadGamePlay(index);
    }

    void LoadGamePlay(int index)
    {
        SceneManager.LoadScene("SampleScene");       
    }
}
