using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransformButton : MonoBehaviour
{
    public Sprite clickedSprite;
    public Transform generateObjectParent;
    public Transform destroyObjectParent;

    List<Transform> generateObjectList = new List<Transform>();
    List<Transform> destroyObjectList = new List<Transform>();

    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        boxCollider2D = transform.parent.GetComponent<BoxCollider2D>();
        spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();

        for (int i = 0; i < generateObjectParent.childCount; i++)
        {
            generateObjectList.Add(generateObjectParent.GetChild(i));
        }
        for (int i = 0; i < destroyObjectParent.childCount; i++)
        {
            destroyObjectList.Add(destroyObjectParent.GetChild(i));
        }
    }

    void GenerateObjects()
    {
        foreach (Transform t in generateObjectList)
        {
            t.gameObject.SetActive(true);
        }
    }
    void DestroyObjects()
    {
        foreach (Transform t in destroyObjectList)
        {
            t.gameObject.SetActive(false);
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

            GenerateObjects();
            DestroyObjects();
        }
    }
}
