using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedGround : MonoBehaviour
{
    public float timeToBreak;
    public float timeToRespawn;
    IEnumerator BreakGround()
    {
        if (gameObject.transform.childCount > 0)
        {
            yield return new WaitForSeconds(timeToBreak);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(timeToRespawn);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(timeToBreak);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            yield return new WaitForSeconds(timeToRespawn);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
            
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<CapsuleCollider2D>().bounds.center.y > gameObject.GetComponent<BoxCollider2D>().bounds.center.y)
        {
            StartCoroutine("BreakGround");
        }
    }
}
