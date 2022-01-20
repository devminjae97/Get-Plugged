using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
    private CanvasGroup mainGroup;
    private CanvasGroup keyGuideGroup;

    private Animator animPlay;
    private Animator animKey;
    private Animator animQuit;
    private Animator animBack;

    private

    void Awake() 
    {
        mainGroup = GameObject.Find("MainMenu").GetComponent<CanvasGroup>();
        keyGuideGroup = GameObject.Find("KeyGuideMenu").GetComponent<CanvasGroup>();

        animPlay = GameObject.Find("Btn_play").GetComponent<Animator>();
        animKey = GameObject.Find("Btn_key").GetComponent<Animator>();
        animQuit = GameObject.Find("Btn_quit").GetComponent<Animator>();
        animBack = GameObject.Find("Btn_back").GetComponent<Animator>();

        animPlay.SetBool("on", false);
        animPlay.SetBool("isClicked", false);
        animPlay.SetBool("isHovered", false);
        animKey.SetBool("on", false);
        animKey.SetBool("isClicked", false);
        animKey.SetBool("isHovered", false);
        animQuit.SetBool("on", false);
        animQuit.SetBool("isClicked", false);
        animQuit.SetBool("isHovered", false);


        SetEnable(mainGroup, false);

        SetMainMenu(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            OnESCInKeyGuideMenu();

        // test code for Dev. & Testers
        TypeDevCodes();
    }

    public void SetMainMenu(bool b) 
    {
        if (b)
            StartCoroutine("IEMainMenuOn");
        else
            StartCoroutine("IEMainMenuOff");
    }

    IEnumerator IEMainMenuOn()
    {
        SetAlpha(mainGroup, 1f);

        yield return new WaitForSeconds(0.1f);
        
        animPlay.SetBool("on", true);

        yield return new WaitForSeconds(0.1f);

        animKey.SetBool("on", true);

        yield return new WaitForSeconds(0.1f);

        animQuit.SetBool("on", true);

        yield return new WaitForSeconds(0.3f);

        SetEnable(mainGroup, true);

        StopCoroutine("IEMainMenuOn");
    }

    IEnumerator IEMainMenuOff() 
    {
        if(mainGroup.alpha == 1)
        {
            SetEnable(mainGroup, false);

            animQuit.SetBool("on", false);

            yield return new WaitForSeconds(0.1f);

            animKey.SetBool("on", false);

            yield return new WaitForSeconds(0.1f);

            animPlay.SetBool("on", false);

            yield return new WaitForSeconds(0.3f);

            SetAlpha(mainGroup, 0f);
        }
        StopCoroutine("IEMainMenuOff");
    }

    public void SetKeyGuideMenu(bool b)
    {
        if (b)
            StartCoroutine("IEKeyGuideMenuOn");
        else
            StartCoroutine("IEKeyGuideMenuOff");
    }

    IEnumerator IEKeyGuideMenuOn()
    {
        SetAlpha(keyGuideGroup, 1f);

        yield return new WaitForSeconds(0.1f);

        animBack.SetBool("on", true);

        yield return new WaitForSeconds(0.3f);

        SetEnable(keyGuideGroup, true);

        StopCoroutine("IEKeyGuideMenuOn");
    }

    IEnumerator IEKeyGuideMenuOff() 
    {
        if(keyGuideGroup.alpha == 1)
        {
            SetEnable(keyGuideGroup, false);

            animBack.SetBool("on", false);

            yield return new WaitForSeconds(0.3f);

            SetAlpha(keyGuideGroup, 0f);
        }

        StopCoroutine("IEKeyGuideMenuOff");
    }

    void SetEnable(CanvasGroup cg, bool b)
    {
        cg.interactable = b;
        cg.blocksRaycasts = b;
    }

    void SetAlpha(CanvasGroup cg, float f) 
    {
        cg.alpha = f;
    }

    void OnESCInKeyGuideMenu()
    {
        if(keyGuideGroup.alpha== 1)
            StartCoroutine("IEOnESCKeyGuideMenu");
    }

    IEnumerator IEOnESCKeyGuideMenu()
    {
        SetMainMenu(false);
        SetKeyGuideMenu(false);
        
        yield return new WaitForSeconds(0.5f);

        SetMainMenu(true);
    }


    // for testers & developers

    private char[] unlockCodeL = {'u', 'n', 'l', 'o', 'c', 'k'};
    private char[] unlockCodeU = {'U', 'N', 'L', 'O', 'C', 'K'};

    private char[] resetCodeL = {'r', 'e', 's', 'e', 't'};
    private char[] resetCodeU = {'R', 'E', 'S', 'E', 'T'};

    private List<char> inputCode = new List<char>();

    private void TypeDevCodes()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(IsUnlockCode())
            {
                int nos = 30;   // num of stages
                Debug.Log("Unlock 1 to " + nos + " stages.");
                PlayerPrefs.SetInt("stageReached", 30);
            }
            else if(IsResetCode())
            {
                Debug.Log("Reset all stages.");
                PlayerPrefs.SetInt("stageReached", 1);
            }

            inputCode = new List<char>();
        }
        else if(Input.anyKeyDown)
        {
            foreach(char c in Input.inputString)
                inputCode.Add(c);
        }
    }

    private bool IsUnlockCode()
    {
        if(inputCode.Count == 6)
        {
            for(int i = 0; i < 6; i++)
            {
                if(!(inputCode[i] == unlockCodeL[i] || inputCode[i] == unlockCodeU[i]))
                {
                    return false;
                }
            }
            return true;
        }
        else
            return false;
    }

    private bool IsResetCode()
    {
        if(inputCode.Count == 5)
        {
            for(int i = 0; i < 5; i++)
            {
                if(!(inputCode[i] == resetCodeL[i] || inputCode[i] == resetCodeU[i]))
                {
                    return false;
                }
            }
            return true;
        }
        else
            return false;
    }
}
