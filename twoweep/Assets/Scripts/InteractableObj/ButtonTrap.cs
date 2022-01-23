using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrap : MonoBehaviour
{
    public GameObject NormalTile;
    public TrapButton trapButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == NormalTile)
        {
            trapButton.SetTrapProps();
            gameObject.tag = "Ground";
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
