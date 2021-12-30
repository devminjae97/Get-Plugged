using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButton : MonoBehaviour
{
    public Sprite clickedSprite;
    public GameObject buttonTrap;
    public float trapSpeed;
    private bool isTrapTriggered = false;
    private bool isTrapEnd = false;

    Transform trapTransform;
    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trapTransform = buttonTrap.transform;
    }

    void Update()
    {
        if (isTrapTriggered)
        {
            buttonTrap.transform.Translate(Vector3.down * trapSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<CapsuleCollider2D>().bounds.center.y > gameObject.GetComponent<BoxCollider2D>().bounds.center.y)
        {
            spriteRenderer.sprite = clickedSprite;
            boxCollider2D.size = new Vector2(0.625f, 0.19f);
            boxCollider2D.offset = new Vector2(0.0f, -0.22f);

            if (!isTrapEnd)
            {
                isTrapTriggered = true;
            }
        }       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            isTrapTriggered = false;
            isTrapEnd = true;
            other.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
