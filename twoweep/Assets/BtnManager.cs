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

        StartCoroutine("IEOnScene");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IEOnScene()
    {
        yield return new WaitForSeconds(0.5f);
        
        animPlay.SetBool("on", true);

        yield return new WaitForSeconds(0.1f);

        animOption.SetBool("on", true);

        yield return new WaitForSeconds(0.1f);

        animQuit.SetBool("on", true);

        yield return new WaitForSeconds(0.35f);

        SetEnable(mainGroup, true);

        StopCoroutine("OnScene");
    }

    void SetEnable(CanvasGroup cg, bool b){
        cg.interactable = b;
        cg.blocksRaycasts = b;
    }
}
