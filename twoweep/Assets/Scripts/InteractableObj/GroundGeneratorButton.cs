using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGeneratorButton : MonoBehaviour
{
    public Sprite clickedSprite;
    public GameObject groundObjectParent;
    List<Transform> groundObjectList = new List<Transform>();

    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        boxCollider2D = transform.parent.GetComponent<BoxCollider2D>();
        spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();

        for (int i = 0; i < groundObjectParent.transform.childCount; i++)
        {
            groundObjectList.Add(groundObjectParent.transform.GetChild(i));
        }
    }

    void GenerateGround()
    {
        foreach (Transform t in groundObjectList)
        {
            t.gameObject.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<CapsuleCollider2D>().bounds.center.y > gameObject.GetComponent<BoxCollider2D>().bounds.center.y)
        {
            spriteRenderer.sprite = clickedSprite;
            boxCollider2D.size = new Vector2(0.625f, 0.115f);
            boxCollider2D.offset = new Vector2(0.0f, -0.245f);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.45f, 0.2f);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.0035f, -0.23f);

            GenerateGround();
        }
    }
}
