using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransformButton : Interactor
{
    public Sprite clickedSprite;
    public Transform generateObjectParent;
    public Transform destroyObjectParent;

    List<Transform> generateObjectList;
    List<Transform> destroyObjectList;

    BoxCollider2D boxCollider2D;
    SpriteRenderer sr;


    // init values
    private Sprite init_sprite;
    private Vector3 init_trapPos;
    private Vector2 init_parentSize;
    private Vector2 init_parentOffset;
    private Vector2 init_size;
    private Vector2 init_offset;

    public override void StoreInitValues()
    {
        // button
        init_sprite = sr.sprite;
        init_parentSize = boxCollider2D.size;
        init_parentOffset = boxCollider2D.offset;
        init_size = gameObject.GetComponent<BoxCollider2D>().size;
        init_offset = gameObject.GetComponent<BoxCollider2D>().offset;
    }

    public override void ResetValues()
    {
        // button
        sr.sprite = init_sprite;
        boxCollider2D.size = init_parentSize;
        boxCollider2D.offset = init_parentOffset;
        gameObject.GetComponent<BoxCollider2D>().size = init_size;
        gameObject.GetComponent<BoxCollider2D>().offset = init_offset;

        // objects
        foreach (Transform t in generateObjectList)
        {
            t.gameObject.SetActive(false);
        }
        foreach (Transform t in destroyObjectList)
        {
            t.gameObject.SetActive(true);
        }
    }

    void Awake()
    {
        boxCollider2D = transform.parent.GetComponent<BoxCollider2D>();
        sr = transform.parent.GetComponent<SpriteRenderer>();

        generateObjectList = new List<Transform>();
        destroyObjectList = new List<Transform>();

        if (generateObjectParent)
        {
            for (int i = 0; i < generateObjectParent.childCount; i++)
            {
                generateObjectList.Add(generateObjectParent.GetChild(i));
            }
        }

        if (destroyObjectParent)
        {
            for (int i = 0; i < destroyObjectParent.childCount; i++)
            {
                destroyObjectList.Add(destroyObjectParent.GetChild(i));
            }
        }

        StoreInitValues();
    }

    private void OnEnable() 
    {    
        ResetValues();
    }

    void GenerateObjects()
    {
        foreach (Transform t in generateObjectList)
        {
            Debug.Log("Gen " + t.gameObject.name);
            t.gameObject.SetActive(true);
        }
    }

    void DestroyObjects()
    {
        foreach (Transform t in destroyObjectList)
        {
            Debug.Log("Dist " + t.gameObject.name);
            t.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Vector3 direction = transform.position - collision.gameObject.GetComponent<CapsuleCollider2D>().transform.position;
            if (Mathf.Round(direction.normalized.y * 100) * 0.01f < -0.75f)
            {
                sr.sprite = clickedSprite;
                boxCollider2D.size = new Vector2(0.625f, 0.115f);
                boxCollider2D.offset = new Vector2(0.0f, -0.245f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.45f, 0.2f);
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.0035f, -0.23f);

                GenerateObjects();
                DestroyObjects();
            }
        }
    }
}
