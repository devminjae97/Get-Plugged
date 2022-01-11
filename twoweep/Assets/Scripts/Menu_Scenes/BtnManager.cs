using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
    private CanvasGroup mainGroup;
    private CanvasGroup optionGroup;

    private Animator animPlay;
    private Animator animOption;
    private Animator animQuit;

    void Awake() {
        mainGroup = GameObject.Find("MainMenu").GetComponent<CanvasGroup>();

        animPlay = GameObject.Find("Btn_play").GetComponent<Animator>();
        animOption = GameObject.Find("Btn_opt").GetComponent<Animator>();
        animQuit = GameObject.Find("Btn_quit").GetComponent<Animator>();


        SetEnable(mainGroup, false);

        SetButtons(true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetButtons(bool b) 
    {
        if (b)
            StartCoroutine("IEOnButtons");
        else
            StartCoroutine("IEOffButtons");
    }

    IEnumerator IEOnButtons()
    {
        SetAlpha(mainGroup, 1f);

        yield return new WaitForSeconds(0.1f);
        
        animPlay.SetBool("on", true);

        yield return new WaitForSeconds(0.1f);

        animOption.SetBool("on", true);

        yield return new WaitForSeconds(0.1f);

        animQuit.SetBool("on", true);

        yield return new WaitForSeconds(0.3f);

        SetEnable(mainGroup, true);

        StopCoroutine("IEOnButtons");
    }

    IEnumerator IEOffButtons() 
    {

        SetEnable(mainGroup, false);

        animQuit.SetBool("on", false);

        yield return new WaitForSeconds(0.1f);

        animOption.SetBool("on", false);

        yield return new WaitForSeconds(0.1f);

        animPlay.SetBool("on", false);

        yield return new WaitForSeconds(0.3f);

        SetAlpha(mainGroup, 0f);

        StopCoroutine("IEOffButtons");
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
}
