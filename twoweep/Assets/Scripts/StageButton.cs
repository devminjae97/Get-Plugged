using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour 
{

    [SerializeField] private Image sprite_num0;
    [SerializeField] private Image sprite_num1;
    [SerializeField] private Image sprite_num2;

    [SerializeField] private List<Sprite> nums = new List<Sprite>();

    private int number;

    // Start is called before the first frame update
    void Start()
    {
        SetNumber(17);
    }

    public void SetNumber(int n) 
    {
        number = n;

        if(n > 9) 
        {
            sprite_num0.sprite = nums[10];
            sprite_num1.sprite = nums[n/10];
            sprite_num2.sprite = nums[n%10];
        }
        else if(n > 0) 
        {
            sprite_num0.sprite = nums[n];
            sprite_num1.sprite = nums[10];
            sprite_num2.sprite = nums[10];
        }
    }
}
