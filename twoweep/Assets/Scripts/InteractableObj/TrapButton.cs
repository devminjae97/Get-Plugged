using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButton : Interactor
{
    public Sprite clickedSprite;
    public GameObject buttonTrap;
    [SerializeField] private float trapSpeed;
    private bool isTrapTriggered = false;
    private bool isTrapEnd = false;

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
        // trap
        init_trapPos = buttonTrap.transform.position;

        // button
        init_sprite = sr.sprite;
        init_parentSize = boxCollider2D.size;
        init_parentOffset = boxCollider2D.offset;
        init_size = gameObject.GetComponent<BoxCollider2D>().size;
        init_offset = gameObject.GetComponent<BoxCollider2D>().offset;
    }

    public override void ResetValues() 
    {
        // trap
        isTrapTriggered = false;
        isTrapEnd = false;
        buttonTrap.tag = "Trap";
        buttonTrap.GetComponent<BoxCollider2D>().isTrigger = true;
        buttonTrap.transform.position = init_trapPos;

        // button
        sr.sprite = init_sprite;
        boxCollider2D.size = init_parentSize;
        boxCollider2D.offset = init_parentOffset;
        gameObject.GetComponent<BoxCollider2D>().size = init_size;
        gameObject.GetComponent<BoxCollider2D>().offset = init_offset;
    }

    void Awake() 
    {
        boxCollider2D = transform.parent.GetComponent<BoxCollider2D>();
        sr = transform.parent.GetComponent<SpriteRenderer>();

        StoreInitValues();
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
            sr.sprite = clickedSprite;
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
        if (other.gameObject == buttonTrap)
        {
            isTrapTriggered = false;
            isTrapEnd = true;
            other.gameObject.tag = "Ground";
            other.isTrigger = false;
        }
    }
}