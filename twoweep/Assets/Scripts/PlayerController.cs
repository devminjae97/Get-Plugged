using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("On Normal Ground")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpPower = 10f;

    [Header("On Ice Ground")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedOnIceTile;

    private Rigidbody2D rigid;
    private SpriteRenderer sr;
    private Animator anim;

    // temp; private -> private
    public GameObject startPoint = null;
    
    private GameManager gameManager;

    private Transform groundCheckerLeft;
    private Transform groundCheckerCentre;
    private Transform groundCheckerRight;
    private Transform wallCheckerLeft;
    private Transform wallCheckerRight;

    private Transform goalFlag;
 
    private float terminalVelocity = -8f;
    private float initGravityScale;
    private float reversedDirection;

    private bool isControllable;
    private bool isFacingRight;
    private bool isMoving;
    private bool isJumping;
    private bool isReversed;
    private bool isRightWallDetected;
    private bool isLeftWallDetected;
    private bool isOnGoalFlag;
    private bool isOnIce;
    private bool isSliding;

    void Awake()
    {
        gameManager = GameObject.Find("GM").GetComponent<GameManager>();

        rigid = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();

        groundCheckerLeft = transform.Find("GroundCheckerLeft");
        groundCheckerCentre = transform.Find("GroundCheckerCentre");
        groundCheckerRight = transform.Find("GroundCheckerRight");

        wallCheckerLeft = transform.Find("WallCheckerLeft");
        wallCheckerRight = transform.Find("WallCheckerRight");

        initGravityScale = rigid.gravityScale;

        InitVariables();

    }

    // 단발적이지 않거나 rigid를 이용한 물리효과에 사용
    void FixedUpdate()
    {

        if (isControllable)
        {
            if (!isSliding)
                Move();
            else
                MoveOnIceTile();
        }
           
    }
    
    // 단발적인 입력이나 즉각적인 효과를 요할때 사용
    void Update() 
    {
        if(isControllable)
        {
            Stop();
            Jump();
        }

        GroundCheck();
        WallCheck();
    }

    // 카메라에 흔히 사용
    void LateUpdate() 
    {
        
    }

    public void Init() 
    {

        // initiate player position
        if (startPoint != null)
            this.transform.position = startPoint.transform.position + Vector3.up;
        else 
        {
            Debug.Log("There is no start points!");
            return;
        }

        // set visible
        sr.enabled = true;

        // reset velocity
        rigid.velocity = Vector2.zero;

        // initiate variables
        InitVariables();
    }

    void InitVariables() 
    {
        reversedDirection = 1f;

        isFacingRight = true;
        sr.flipX = false;

        transform.localScale = Vector3.one;

        isMoving = false;

        isJumping = true;
        
        rigid.gravityScale = initGravityScale;
        
        isReversed = false;

        isOnGoalFlag = false;

        isControllable = false;

        isRightWallDetected = false;
        isLeftWallDetected = false;

        isOnIce = false;

        isSliding = false;

        goalFlag = null;

        // anim
        anim.SetBool("isRunning", false);
        anim.SetBool("isJumping", false);

    } 

    void Move() 
    {
        float h = 0;

        if (Input.GetButton("Horizontal"))
            h = Input.GetAxis("Horizontal");  // (0, 1] : to right, [-1, 0) : to left

        transform.Translate(Vector3.right * (h == 0 ? 0 : (h < 0 ? (isLeftWallDetected ? 0 : -1) : (isRightWallDetected ? 0 : 1))) * speed * Time.deltaTime);
        
        if ((h > 0 && !isFacingRight) || (h < 0 && isFacingRight))
            Flip();

        // set animation
        if (h != 0)
        {
            isMoving = true;
            anim.SetBool("isRunning", true);
        }
        else
        {
            isMoving = false;
            anim.SetBool("isRunning", false);
        }
    }

    void MoveOnIceTile() 
    {
        /*
        float hAxis = Input.GetAxisRaw("Horizontal");

        if(hAxis == 0)
        {
            isControllable = true;
        }
        else
        {
            isControllable = false;
        }

        if (!isControllable)
        {
            rigid.AddForce(Vector2.right * hAxis * speedOnIceTile, ForceMode2D.Impulse);

            if (rigid.velocity.x > maxSpeed)
            {
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            }
            else if (rigid.velocity.x < (-1) * maxSpeed)
            {
                rigid.velocity = new Vector2((-1) * maxSpeed, rigid.velocity.y);
            }
        }
        */
        if(isMoving)
        {
            anim.SetBool("isRunning", false);
            transform.Translate(Vector3.right * (isFacingRight ? 1 : -1) * speed * Time.deltaTime);
        }
        else
        {
            if(Input.GetButton("Horizontal"))
            {
                float h = Input.GetAxisRaw("Horizontal");

                if ((h > 0 && !isFacingRight) || (h < 0 && isFacingRight))
                    Flip();

                isMoving = true;
            }
        }
        
    }

    void Stop()
    {
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(0.5f * rigid.velocity.x, rigid.velocity.y);
    }

    void Jump() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            rigid.AddForce(Vector3.up * jumpPower * reversedDirection, ForceMode2D.Impulse);
        
        // set Terminal Velocity
        if (rigid.velocity.y * reversedDirection <= terminalVelocity)   // 반대로?
            rigid.velocity = new Vector2(rigid.velocity.x, terminalVelocity * reversedDirection);

        // set animation
        if (isJumping)
            anim.SetBool("isJumping", true);
        else
            anim.SetBool("isJumping", false);
    }

    void Flip() 
    {
        isFacingRight = !isFacingRight;
        sr.flipX = !isFacingRight;
    }

    public void Reverse() 
    {
        isReversed = !isReversed;

        // flip scale.y
        transform.localScale = new Vector3(1, transform.localScale.y * -1, 1);

        // reverse gravity
        rigid.gravityScale *= -1;

        // reverse horizontal control
        reversedDirection *= -1;

        // reset velocity
        rigid.velocity = Vector2.zero;
    }
    
    void GroundCheck() 
    {
        bool isGroundLeft = false;
        bool isGroundCentre = false;
        bool isGroundRight = false;

        bool isIceLeft = false;
        bool isIceCentre = false;
        bool isIceRight = false;

        if (Physics2D.OverlapPoint(groundCheckerLeft.position) != null)
        {
            isGroundLeft = Physics2D.OverlapPoint(groundCheckerLeft.position).CompareTag("Ground") ? true : false;
            isIceLeft = Physics2D.OverlapPoint(groundCheckerLeft.position).CompareTag("Ice") ? true : false;
        }

        if (Physics2D.OverlapPoint(groundCheckerCentre.position) != null)
        {
            isGroundCentre = Physics2D.OverlapPoint(groundCheckerCentre.position).CompareTag("Ground") ? true : false;
            isIceCentre = Physics2D.OverlapPoint(groundCheckerCentre.position).CompareTag("Ice") ? true : false;
        }

        if (Physics2D.OverlapPoint(groundCheckerRight.position) != null)
        {
            isGroundRight = Physics2D.OverlapPoint(groundCheckerRight.position).CompareTag("Ground") ? true : false;
            isIceRight = Physics2D.OverlapPoint(groundCheckerRight.position).CompareTag("Ice") ? true : false;
        }


        // centre 가 ice이면 on ice
        if(isIceCentre)
            isOnIce = true;
        else if(!isIceCentre && isGroundCentre)
            isOnIce = false;

        // test
        //Debug.Log(this.name + " >> " + Physics2D.OverlapPoint(groundCheckerCentre.position).tag);



        isJumping = !(isGroundLeft || isGroundCentre || isGroundRight || isIceLeft || isIceCentre || isIceRight);
    }

    void WallCheck()
    {
        if (Physics2D.OverlapPoint(wallCheckerLeft.position) != null)
            isLeftWallDetected = (Physics2D.OverlapPoint(wallCheckerLeft.position).CompareTag("Ground") || Physics2D.OverlapPoint(wallCheckerLeft.position).CompareTag("Ice")) ? true : false;
        else
            isLeftWallDetected = false;

        if (Physics2D.OverlapPoint(wallCheckerRight.position) != null)
            isRightWallDetected = (Physics2D.OverlapPoint(wallCheckerRight.position).CompareTag("Ground") || Physics2D.OverlapPoint(wallCheckerRight.position).CompareTag("Ice")) ? true : false;
        else
            isRightWallDetected = false;
    }

    public void SetPlayerControllability(bool b) 
    {
        isControllable = b;
    }

    public void SetStartPoint(GameObject o)
    {
        if(o != null)
        {
            startPoint = o;
        }
    }

    public bool GetIsOnGoalFlag() 
    {
        return isOnGoalFlag;
    }

    public void Plug(bool b)
    {
        // plug
        if(b)
        {
            // set invisible
            sr.enabled = false;

            // set position
            transform.position = new Vector3(goalFlag.position.x, transform.position.y, transform.position.z);

            // set sprite direction & play anim
            goalFlag.GetComponent<SpriteRenderer>().flipX = !isFacingRight;
            goalFlag.GetComponent<Animator>().SetBool("isPlugging", true);
        }
        // unplug
        else
        {
            if(goalFlag != null)
                goalFlag.GetComponent<Animator>().SetBool("isPlugging", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Finish"))
        {
            goalFlag = other.transform;
            isOnGoalFlag = true;
        } 
        else if(other.CompareTag("Trap"))
        {
            gameManager.RespawnPlayers();
        }
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("IceAirspace"))
        {
            if(isOnIce)
            {
                isSliding = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Finish")) 
        {
            goalFlag = null;
            isOnGoalFlag = false;
        }
        else if(other.CompareTag("IceAirspace"))
        {
            if(isSliding)
                isSliding = false;
        }
    }
/*
    // yang fix - 20220104
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("IceTile"))
        {
            isOnIce = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("IceTile"))
        {   
            isOnIce = false;
            isControllable = true;
        }
    }
    */
}
