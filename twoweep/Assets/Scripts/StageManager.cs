using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{

    [SerializeField] private int numOfStages;
    [SerializeField] private float mouseDisabledTime;
    public Animator switchSceneMask;

    private Button[] buttons;
    private PageButton prevButton;
    private PageButton nextButton;

    private int stageReached;
    private int page;   // 0, 1, 2, ...



    //public static int thisLevel;

    private void Awake()
    {

        // for PlayerPrefs initialization code
        //PlayerPrefs.DeleteAll();
        // for PlayerPrefs testing code
        //PlayerPrefs.SetInt("stageReached", 2);
        Init();
        
    }

    private void Start()
    {
        //buttons = FindObjectsOfType<Button>(); // not ordered
        SetPage(0);
        StartCoroutine("IEButtonDisabled");
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
        
        buttons = GameObject.Find("Frame").GetComponentsInChildren<Button>();

        prevButton = GameObject.Find("PrevButton").GetComponent<PageButton>();
        nextButton = GameObject.Find("NextButton").GetComponent<PageButton>();

        page = 0;        
    }

    public void SetPage(int n)
    {
        page += n;

        for(int i = 0; i < 15; i++)
        {
            int s = page * 15 + i + 1;

            buttons[i].GetComponent<StageButton>().SetNumber(s);
            
            if(s > numOfStages)
                buttons[i].GetComponent<StageButton>().SetClear();
            /*else if(s > stageReached)
                buttons[i].GetComponent<StageButton>().Lock(true);*/
            else
                buttons[i].GetComponent<StageButton>().Lock(false);
        }

        if(page == 0)
            prevButton.Lock(true);
        else
            prevButton.Lock(false);

        if((page + 1) * 15 >= numOfStages)
            nextButton.Lock(true);
        else
            nextButton.Lock(false);
    }

    IEnumerator IEButtonDisabled()
    {
        foreach (Button b in buttons)
        {
            b.interactable = false;
        }

        yield return new WaitForSeconds(mouseDisabledTime);

        foreach (Button b in buttons)
        {
            b.interactable = true;
        }
    }

//ref
    // void GenerateStageButtons()
    // {
    //     for (int i = 0; i < numOfStage; i++)
    //     {
    //         int x = i + 1;

    //         GameObject stageButton = Instantiate(this.stageButton);
    //         Text buttonText = stageButton.GetComponentInChildren<Text>();

    //         stageButton.transform.SetParent(parentPanel, false);
    //         buttonText.text = (i + 1).ToString();

    //         stageButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate
    //         {
    //             SelectStage(x);
    //         });

    //         // Stage Lock Function
    //         if (i + 1 > stageReached)
    //         {
    //             stageButton.GetComponent<Button>().interactable = false;
    //             stageButton.GetComponent<Image>().sprite = lockedButton;
    //         }
    //     }
    
    public void GoToMainScene()
    {
        StartCoroutine("IEAnimStartAndGoToMainScene");
    }

    IEnumerator IEAnimStartAndGoToMainScene()
    {
        switchSceneMask.SetTrigger("Close");

        foreach (Button b in buttons)
        {
            b.interactable = false;
        }

        yield return new WaitForSeconds(mouseDisabledTime);

        SceneManager.LoadScene("MainScene");
    }

    public void SelectStage(int index)
    {
        PlayerPrefs.SetInt("stageSelected", index);
        StartCoroutine("IEAnimStartAndSwitchScene");
    }

    IEnumerator IEAnimStartAndSwitchScene()
    {
        switchSceneMask.SetTrigger("Close");

        foreach (Button b in buttons)
        {
            b.interactable = false;
        }

        yield return new WaitForSeconds(mouseDisabledTime);

        SceneManager.LoadScene("InGameScene");
    }
}
