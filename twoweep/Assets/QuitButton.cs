using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    // panel에 있는애들 통채로 바꿔야해 ㅠㅠㅠㅠㅠㅠ 이건 임시야
    public void OnClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
