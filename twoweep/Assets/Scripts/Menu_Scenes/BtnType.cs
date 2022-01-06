using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public enum buttonType
    {
        New,
        Continue,
        Option,
        Back,
        Quit,
        BackToMenu
    }
    [SerializeField] private buttonType currentType;
    [SerializeField] private Transform buttonScale_TR;
    Vector3 defaultScale_V;
    [SerializeField] private CanvasGroup mainGroup;
    [SerializeField] private CanvasGroup optionGroup;
    public Animator SwitchSceneCloseAnim;

    private void Start()
    {
        defaultScale_V = buttonScale_TR.localScale;
    }
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case buttonType.New:
                SceneLoader.LoadSceneHandle("SelectStage", 0);
                break;
            case buttonType.Continue:
                SceneLoader.LoadSceneHandle("SelectStage", 1);
                break;
            case buttonType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case buttonType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case buttonType.BackToMenu:
                SwitchSceneCloseAnim.SetTrigger("Close");
                SceneLoader.LoadSceneHandle("MainMenu", 0);
                break;
            case buttonType.Quit:
                Application.Quit();
                break;
        }
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
        buttonScale_TR.localScale = defaultScale_V * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale_TR.localScale = defaultScale_V;
    }
}
