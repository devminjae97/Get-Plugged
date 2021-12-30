using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INT_Reverse : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<PlayerController>().Reverse();
            gameObject.SetActive(false);       
        }
    }     
}
