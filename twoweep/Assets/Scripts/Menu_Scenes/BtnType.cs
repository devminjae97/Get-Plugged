using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public enum buttonType
    {
        Play,
        Continue,
        Key,
        Back,
        Quit,
        BackToMenu
    }
    [SerializeField] private buttonType currentType;
    [SerializeField] private Transform buttonScale_TR;
    Vector3 defaultScale_V;
    [SerializeField] private CanvasGroup mainGroup;
    [SerializeField] private CanvasGroup keyGuideGroup;
    public Animator SwitchSceneCloseAnim;


    //mj
    private BtnManager btnManager;
    private Animator anim;
    private bool isHovered;
    private bool isClicked;

    private void Awake() 
    {
        isHovered = false;
        isClicked = false;

        if (!btnManager) btnManager = GameObject.Find("BtnManager").GetComponent<BtnManager>();

        if(!anim) anim = this.GetComponentInParent<Animator>();
    }

    private void Start()
    {
        //defaultScale_V = buttonScale_TR.localScale;
    }

    public void OnBtnClick()
    {
        Debug.Log("clicked");

        //isClicked = true;

        //StartCoroutine("IEBtnClick");

    }

    private IEnumerator IEBtnClick()
    {
        yield return new WaitForSeconds(0.1f);

        switch (currentType)
        {
            case buttonType.Play:
                SceneManager.LoadScene("SelectStage");
                break;
            case buttonType.Continue:
                SceneManager.LoadScene("SelectStage");
                break;
            case buttonType.Key:
                //CanvasGroupOn(keyGuideGroup);
                //CanvasGroupOff(mainGroup);
                btnManager.SetKeyGuideMenu(true);
                break;
            case buttonType.Back:
                btnManager.SetMainMenu(true);
                break;
            case buttonType.BackToMenu:
                SwitchSceneCloseAnim.SetTrigger("Close");
                SceneManager.LoadScene("MainMenu");
                break;
            case buttonType.Quit:
                Application.Quit();
                break;
        }

        isClicked = false;

        StopCoroutine("IEBtnClick");
    }



    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //buttonScale_TR.localScale = defaultScale_V * 1.1f;
        if(!isClicked)
        {
            //Debug.Log(this.name + " in");

            isHovered = true;

            if (anim)
                anim.SetBool("isHovered", true);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //buttonScale_TR.localScale = defaultScale_V;
        if(!isClicked)
        {
            //Debug.Log(this.name + " out");

            isHovered = false;

            if(anim)
                anim.SetBool("isHovered", false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (anim)
            anim.SetBool("isClicked", true);
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        if (anim)
            anim.SetBool("isClicked", false);

        if (isHovered) 
            StartCoroutine("IEBeforeButtonActivated");
    }

    IEnumerator IEBeforeButtonActivated() 
    {
        btnManager.SetMainMenu(false);
        btnManager.SetKeyGuideMenu(false);

        yield return new WaitForSeconds(0.4f);

        StartCoroutine("IEBtnClick");

        StopCoroutine("IEBeforeButtonActivated");
    }
}
