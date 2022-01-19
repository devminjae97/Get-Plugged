using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private Image sprite_num0;
    [SerializeField] private Image sprite_num1;
    [SerializeField] private Image sprite_num2;

    [SerializeField] private List<Sprite> nums = new List<Sprite>();
    [SerializeField] private List<Sprite> btns = new List<Sprite>();

    private Button btn;
    private Image sprite_btn;

    Color colorBG;
    Color colorWH;

    private int number = 0;

    private bool isLocked;
    private bool isHovered;
    private bool isClicked;

    void Awake() 
    {
        isLocked = true;
        btn = this.GetComponent<Button>();
        sprite_btn = btn.image;

        ColorUtility.TryParseHtmlString("#222034", out colorBG);
        colorWH = new Color(1f, 1f, 1f, 1f);
    }

    void Start()
    {
        //test
        SetNumber(17);
    }

    public void SetNumber(int n) 
    {
        number = n;
    }

    public void Lock(bool b) 
    {
        isLocked = b;

        if (!b)
            ShowNumber(number);
        else
            ShowNumber(-1);
    }

    private void ShowNumber(int n) 
    {
        if (n > 9) {
            sprite_num0.sprite = nums[10];
            sprite_num1.sprite = nums[n / 10];
            sprite_num2.sprite = nums[n % 10];
        }
        else if (n > 0) {
            sprite_num0.sprite = nums[n];
            sprite_num1.sprite = nums[10];
            sprite_num2.sprite = nums[10];
        }
        else 
        {
            sprite_num0.sprite = nums[10];
            sprite_num1.sprite = nums[10];
            sprite_num2.sprite = nums[10];
        }
    }

    public void OnPointerEnter(PointerEventData e) 
    {
        if (btn.interactable) 
        {
            isHovered = true;

            sprite_num0.color = this.colorBG;
            sprite_num1.color = this.colorBG;
            sprite_num2.color = this.colorBG;

            if(!isClicked)
                sprite_btn.sprite = btns[1];
        }
    }

    public void OnPointerExit(PointerEventData e)
    {
        if (btn.interactable) 
        {
            isHovered = false;

            sprite_num0.color = this.colorWH;
            sprite_num1.color = this.colorWH;
            sprite_num2.color = this.colorWH;

            sprite_btn.sprite = btns[0];
        }
    }

    public void OnPointerDown(PointerEventData e) 
    {
        if (btn.interactable) 
        {
            isClicked = true;

            sprite_num0.color = this.colorWH;
            sprite_num1.color = this.colorWH;
            sprite_num2.color = this.colorWH;
            
            sprite_btn.sprite = btns[0];
        }
    }

    public void OnPointerUp(PointerEventData e) 
    {
        if (btn.interactable) 
        {
            isClicked = false;

            if (isHovered) {
                isHovered = false;

                Debug.Log("to stage " + number);
            }
        }
    }
}
