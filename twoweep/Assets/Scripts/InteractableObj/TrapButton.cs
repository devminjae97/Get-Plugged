using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButton : MonoBehaviour
{
    public Sprite clickedSprite;
    public GameObject buttonTrap;
    [SerializeField] private float trapSpeed;
    private bool isTrapTriggered = false;
    private bool isTrapEnd = false;

    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        boxCollider2D = transform.parent.GetComponent<BoxCollider2D>();
        spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
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
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.sprite = clickedSprite;
            boxCollider2D.size = new Vector2(0.625f, 0.115f);
            boxCollider2D.offset = new Vector2(0.0f, -0.245f);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.45f, 0.2f);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.0035f, -0.23f);


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
            other.gameObject.tag = "Ground";
            other.isTrigger = false;
        }
    }
}