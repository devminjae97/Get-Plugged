using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNumber : MonoBehaviour
{
    [SerializeField] private List<Sprite> nums = new List<Sprite>();

    [SerializeField] private Image img_num0;
    [SerializeField] private Image img_num1;
    [SerializeField] private Image img_num2;

    public void SetStageNumber(int n) 
    {
        if (n > 9) {
            img_num0.sprite = nums[10];
            img_num1.sprite = nums[n / 10];
            img_num2.sprite = nums[n % 10];
        }
        else if (n > 0) {
            img_num0.sprite = nums[n];
            img_num1.sprite = nums[10];
            img_num2.sprite = nums[10];
        }
        else {
            img_num0.sprite = nums[10];
            img_num1.sprite = nums[10];
            img_num2.sprite = nums[10];
        }
    }
}
