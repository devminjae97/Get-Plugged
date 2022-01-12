using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INT_Reverse : Interactor
{

    public override void StoreInitValues() {
        // none
    }

    public override void ResetValues() {
        // none
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
            other.transform.parent.GetComponent<PlayerController>().Reverse();
            gameObject.SetActive(false);       
        }
    }     
}
