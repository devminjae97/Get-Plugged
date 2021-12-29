using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [SerializeField] private GameObject menuSet_GO;
    private void Start()
    {
        menuSet_GO.SetActive(false);
    }
    private void Update()
    {     
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Submenu
            if (menuSet_GO.activeSelf)
                menuSet_GO.SetActive(false);
            else
                menuSet_GO.SetActive(true);
        }
    }
}
