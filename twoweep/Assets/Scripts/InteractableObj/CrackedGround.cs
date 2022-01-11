using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedGround : MonoBehaviour
{
    [SerializeField] private float timeToBreak;
    [SerializeField] private float timeToRespawn;
    public GameObject killCollider;
    private SpriteRenderer parentSprite;
    private BoxCollider2D parent_bc;
    private BoxCollider2D bc;
    private BoxCollider2D kill_bc;
    private CapsuleCollider2D ccc;

    private void Start()
    {
        parentSprite = gameObject.transform.parent.GetComponent<SpriteRenderer>();
        parent_bc = gameObject.transform.parent.GetComponent<BoxCollider2D>();
        bc = GetComponent<BoxCollider2D>();
        kill_bc = killCollider.GetComponent<BoxCollider2D>();
    }

    IEnumerator BreakGround()
    {
        yield return new WaitForSeconds(timeToBreak);
        parentSprite.enabled = false;
        parent_bc.enabled = false;
        bc.enabled = false;
        kill_bc.enabled = false;

        if (gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        yield return new WaitForSeconds(timeToRespawn);
        parentSprite.enabled = true;
        parent_bc.enabled = true;
        bc.enabled = true;
        kill_bc.enabled = true;

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
        ccc = collision.gameObject.GetComponent<CapsuleCollider2D>();
        float direction = transform.position.y - collision.gameObject.transform.position.y;
        Debug.Log(direction);
        if(direction < 0)
        {
            if (collision.gameObject.CompareTag("Player") && (ccc.bounds.center.y - ccc.bounds.extents.y) > (bc.bounds.center.y + bc.bounds.extents.y))
            {
                StartCoroutine("BreakGround");
            }
        }
        

        /*if (collision.gameObject.CompareTag("Player") && (ccc.bounds.center.y - ccc.bounds.extents.y) > (bc.bounds.center.y + bc.bounds.extents.y))
        {
            StartCoroutine("BreakGround");
        }*/
    }
}
