using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedGround : MonoBehaviour
{
    public float timeToBreak;
    public float timeToRespawn;
    IEnumerator BreakGround()
    {
        yield return new WaitForSeconds(timeToBreak);
        gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        if (gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        yield return new WaitForSeconds(timeToRespawn);
        gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        if (gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("BreakGround");
        }
    }
}
