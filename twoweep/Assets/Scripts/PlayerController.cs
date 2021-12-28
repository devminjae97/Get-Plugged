using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpPower = 10f;

    private Rigidbody2D rigid;
    private SpriteRenderer sr;
    private Animator anim;

    // temp; private -> private
    public GameObject startPoint = null;
    
    private GameManager gameManager;

    private Transform groundCheckerLeft;
    private Transform groundCheckerCentre;
    private Transform groundCheckerRight;

    private Collider2D checkerTargetLeft;
    private Collider2D checkerTargetCentre;
    private Collider2D checkerTargetRight;


    private float terminalVelocity = -8f;

    private bool isControllable;
    private bool isFacingRight;
    private bool isMoving;
    private bool isJumping;
    private bool isOnGoalFlag;

    void Start()
    {
        gameManager = GameObject.Find("GM").GetComponent<GameManager>();

        rigid = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();

        groundCheckerLeft = transform.Find("GroundCheckerLeft");
        groundCheckerCentre = transform.Find("GroundCheckerCentre");
        groundCheckerRight = transform.Find("GroundCheckerRight");

        Init();
    }

    // 단발적이지 않거나 rigid를 이용한 물리효과에 사용
    void FixedUpdate()
    {
        if(isControllable)
            Move();
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
    }

    // 카메라에 흔히 사용
    void LateUpdate() 
    {
        
    }

    public void Init() 
    {
        // disable control
        isControllable = false;

        // initiate player position
        if(startPoint != null)
            this.transform.position = startPoint.transform.position + Vector3.up;
        else
            return;

        rigid.velocity = Vector2.zero;

        // initiate variables
        checkerTargetLeft = null;
        checkerTargetCentre = null;
        checkerTargetRight = null;
        
        isFacingRight = true;
        sr.flipX = false;

        isJumping = true;

        isOnGoalFlag = false;

        // enable control
        isControllable = true;
    }

    void Move() 
    {
        float h = 0;

        if (Input.GetButton("Horizontal"))
            h = Input.GetAxis("Horizontal");  // (0, 1] : to right, [-1, 0) : to left

        transform.Translate(Vector3.right * (h == 0 ? 0 : (h < 0 ? -1 : 1)) * speed * Time.deltaTime);

        if ((h > 0 && !isFacingRight) || (h < 0 && isFacingRight))
            Flip();

        // set animation
        if (h != 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
    }

    void Stop()
    {
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(0.5f * rigid.velocity.x, rigid.velocity.y);
    }

    void Jump() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
        
        // set Terminal Velocity
        if (rigid.velocity.y <= terminalVelocity)
            rigid.velocity = new Vector2(rigid.velocity.x, terminalVelocity);

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
    
    void GroundCheck() 
    {
        bool isCheckedLeft = false;
        bool isCheckedCentre = false;
        bool isCheckedRight = false;

        checkerTargetLeft = Physics2D.OverlapPoint(groundCheckerLeft.position);
        checkerTargetCentre = Physics2D.OverlapPoint(groundCheckerCentre.position);
        checkerTargetRight = Physics2D.OverlapPoint(groundCheckerRight.position);

        if (Physics2D.OverlapPoint(groundCheckerLeft.position) != null)
            isCheckedLeft = Physics2D.OverlapPoint(groundCheckerLeft.position).CompareTag("Ground") ? true : false;

        if (Physics2D.OverlapPoint(groundCheckerCentre.position) != null)
            isCheckedCentre = Physics2D.OverlapPoint(groundCheckerCentre.position).CompareTag("Ground") ? true : false;

        if (Physics2D.OverlapPoint(groundCheckerRight.position) != null)
            isCheckedRight = Physics2D.OverlapPoint(groundCheckerRight.position).CompareTag("Ground") ? true : false;

        isJumping = !(isCheckedLeft || isCheckedCentre || isCheckedRight);
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

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Finish")){
            Debug.Log("Finish");
            isOnGoalFlag = true;
        } 
        else if(other.CompareTag("Floor"))  // or Trap
        {
            Debug.Log("Fall");
            gameManager.Reset();
            // Init();
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Finish")) 
        {
            isOnGoalFlag = false;
        }
    }
}
