using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    [SerializeField] private bool isFallable = false;
    [SerializeField] private bool isFacingRight = true;

    private SpriteRenderer sr;
    
    private Transform groundCheckerLeft;
    private Transform groundCheckerRight;
    private Transform wallCheckerLeft;
    private Transform wallCheckerRight;

    private Vector3 init_pos;
    private bool init_isFacingRight;
    
    void Awake() 
    {
        sr = this.GetComponent<SpriteRenderer>();

        groundCheckerLeft = transform.Find("GroundCheckerLeft");
        groundCheckerRight = transform.Find("GroundCheckerRight");

        wallCheckerLeft = transform.Find("WallCheckerLeft");
        wallCheckerRight = transform.Find("WallCheckerRight");

        init_pos = transform.position;
        init_isFacingRight = isFacingRight;
    }

    void OnEnable() 
    {
        Reset();
    }

    void Update()
    {
        Move();
        Flip();
    }

    void Reset() 
    {
        transform.position = init_pos;
        isFacingRight = init_isFacingRight;
    }

    void Move() 
    {
        transform.Translate(Vector3.right * (isFacingRight ? 1 : -1) * speed * Time.deltaTime);
    }

    void Flip() 
    {
        bool isLeftCliff = false;
        bool isRightCliff = false;
        bool isLeftWall = false;
        bool isRightWall = false;

        // ground check
        if (!isFallable) 
        {
            if (Physics2D.OverlapPoint(groundCheckerLeft.position) == null)
                isLeftCliff = true;

            if (Physics2D.OverlapPoint(groundCheckerRight.position) == null)
                isRightCliff = true;
        }

        // wall check
        if (Physics2D.OverlapPoint(wallCheckerLeft.position) != null)
            isLeftWall = Physics2D.OverlapPoint(wallCheckerLeft.position).CompareTag("Ground") ? true : false;

        if (Physics2D.OverlapPoint(wallCheckerRight.position) != null)
            isRightWall = Physics2D.OverlapPoint(wallCheckerRight.position).CompareTag("Ground") ? true : false;

        // flip
        if (isLeftWall || isLeftCliff)
            FaceRight(true);

        if (isRightWall || isRightCliff)
            FaceRight(false);
    }

    void FaceRight(bool b) 
    {
        isFacingRight = b;
        sr.flipX = !isFacingRight;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Trap")) {
            this.gameObject.SetActive(false);
        }
    }
}
