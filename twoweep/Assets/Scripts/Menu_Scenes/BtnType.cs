using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private buttonType currentType;
    [SerializeField] private Transform buttonScale_TR;
    Vector3 defaultScale_V;
    [SerializeField] private CanvasGroup mainGroup;
    [SerializeField] private CanvasGroup optionGroup;

    private void Start()
    {
        defaultScale_V = buttonScale_TR.localScale;
    }
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case buttonType.New:
                SceneLoader.LoadSceneHandle("Play", 0);
                break;
            case buttonType.Continue:
                SceneLoader.LoadSceneHandle("Play", 1);
                break;
            case buttonType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case buttonType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case buttonType.Quit:
                Application.Quit();
                Debug.Log("³ª°¡±â");
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
