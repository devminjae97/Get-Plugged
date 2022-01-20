using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    
    [SerializeField] private List<Sprite> btns = new List<Sprite>();
    
    private StageManager stageManager;
    //private zTestStageManager stageManager;  //test

    private Button btn;
    private Image img;

    private bool isLocked;
    private bool isHovered;
    private bool isClicked;

    void Awake() 
    {
        stageManager = GameObject.Find("SM").GetComponent<StageManager>();
        //stageManager = GameObject.Find("SM").GetComponent<zTestStageManager>();//test
        btn = this.GetComponent<Button>();
        img = btn.image;

        isLocked = false;
        isHovered = false;
        isClicked = false;
    }

    public void OnPointerEnter(PointerEventData e) 
    {
        if (btn.interactable && !isLocked && !isClicked) 
        {
            isHovered = true;
            img.sprite = btns[1];
        }
    }

    public void OnPointerExit(PointerEventData e)
    {
        if (btn.interactable) 
        {
            isHovered = false;

            if(!isLocked)
                img.sprite = btns[0];
        }
    }

    public void OnPointerDown(PointerEventData e) 
    {
        if (btn.interactable && !isLocked) 
        {
            isClicked = true;

            img.sprite = btns[0];
        }
    }

    public void OnPointerUp(PointerEventData e) 
    {
        if (btn.interactable && !isLocked && isClicked) 
        {
            isClicked = false;

            if(isHovered)
            {
                img.sprite = btns[1];

                // to main
                stageManager.GoToMainScene();
                //SceneManager.LoadScene("MainScene");
            }
            
            
        }
    }
}
