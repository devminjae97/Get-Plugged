using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedGround : MonoBehaviour
{
    [SerializeField] private float timeToBreak;
    [SerializeField] private float timeToRespawn;
    [SerializeField] private bool isUsingReverse;
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

        /*if (isUsingReverse)
        {
            bc.size = new Vector2(1.0f, 1.0f);
            bc.offset = new Vector2(0.0f, 0.0f);
        }*/
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

    private void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(Mathf.Round(direction.normalized.y * 100) * 0.01f);
        if (collision.collider.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Vector3 direction = transform.position - collision.gameObject.GetComponent<CapsuleCollider2D>().transform.position;
            if (Mathf.Round(direction.normalized.y * 100) * 0.01f < -0.75f || Mathf.Round(direction.normalized.y * 100) * 0.01f > 0.8f)
            {
                StartCoroutine("BreakGround");
            }
        }
    }
}
