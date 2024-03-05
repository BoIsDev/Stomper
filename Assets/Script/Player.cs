using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D playerCollider;
    private Collider2D wallCollider;

    public UIController ui;
    public HealthCavans hc;
    [SerializeField] private float moveSpeed = 4f;
    public float jumpForce = 5f;
    public float duckForce = -6f;
    private float rollSpeed = 10f;

    public float healthPlayer = 10f;

    private float healthPlayerMax = 10f; 
    public float dameSkill = 1;
    public bool isRoll = true;

    public bool isSetJumping = true;    

    protected bool isDucked = true;
    private bool isFacingRight = true;
    private bool isGrounded = false;
    public bool isWallClimbing = false;

    private int jumpCount = 0;
    private float duckCount = 0;
    private float lastSurfTime = 0f;

    public Animator ani;
    AudioManager audio;
    public GameObject pointColliderHolder;

    private const KeyCode JumpKey = KeyCode.Space;
    private const KeyCode DuckKey = KeyCode.S;
    private const KeyCode RollKey = KeyCode.J;

    private const float DefaultJumpForce = 5f;
    private const float SurfCooldown = 1.5f;
    private const float DefaultRollSpeed = 10f;

    private float wallSlideSpeed = 2f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        playerCollider = GetComponent<CircleCollider2D>();
        wallCollider = GetComponent<Collider2D>();
        hc = FindObjectOfType<HealthCavans>();
        hc.updateHealthBar(healthPlayer, healthPlayerMax);
        ui = FindObjectOfType<UIController>();
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {   
        hc.updateHealthBar(healthPlayer, healthPlayerMax);
        if(healthPlayer <= 0 )
        {
            ui.btPauseGame.SetActive(true);
            ui.scoreCoin.SetActive(false);
            ui.healthBar.SetActive(false);
        }
       
        HandleMovementInput();
        HandleJumpInput();
        HandleDuckInput();
        HandleRollInput();
        HandleWallSilinding();
        
    }

    protected void HandleMovementInput()
    {
        float xDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xDirection * moveSpeed, rb.velocity.y);

        if (xDirection > 0 && !isFacingRight || xDirection < 0 && isFacingRight)
        {
            Flip();
        }

        bool isMoving = Mathf.Abs(xDirection) > 0.01f;
        ani.SetBool("isRunning", isMoving);

    }

    private void HandleWallSilinding()
    {

        if (isWallClimbing)
        {
            // Áp dụng lực nhảy khi người chơi nhấn nút nhảy
            
            if (Input.GetKeyDown(JumpKey))
            {
                Jump();
                isWallClimbing = false; // Kết thúc bám tường khi nhảy
            }
            // Tuột tường nếu ngừng nhấn nút di chuyển
            else if (Input.GetAxis("Horizontal") == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
            // Cho phép người chơi di chuyển lên hoặc xuống tường
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f; // Áp dụng trọng lực bình thường
        }
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(JumpKey) && isSetJumping)
        {
            
            if(jumpCount == 0 && isGrounded)
            {
            Jump();
            jumpCount++;
            ani.SetBool("isJump", true);
            }
            
            else if(jumpCount == 1 )
            {
                jumpCount--;
                jumpForce *= 0.8f;
                Jump();
                ani.SetBool("isJump", true);
                isSetJumping= false;
           
            }  
        }
        
    }

private void HandleDuckInput()
{
    if(Input.GetKeyDown(DuckKey) && isGrounded)
    {
        audio.PlaySFX(audio.duck);

        if (Input.GetKey(DuckKey) && isGrounded)
        {
            moveSpeed = 1f;
            Duck();
            playerCollider.enabled = false;
            ani.SetBool("isDuck", true);
            pointColliderHolder.SetActive(true);
        }
    }
    
    else if (Input.GetKeyUp(DuckKey))
    {
        ani.SetBool("isDuck", false);
        playerCollider.enabled = true;
        pointColliderHolder.SetActive(false);
        duckCount = 0;
        duckForce = -6f;
        moveSpeed = 4f;

    }
    else if (!isGrounded && Input.GetKeyDown(DuckKey))
    {
        duckCount++;
        if (duckCount == 1)
        {
            audio.PlaySFX(audio.duck);

            Duck();

            ani.SetBool("isDuck", true);
        }
        else if (duckCount == 2)
        {
            dameSkill = 5;
            duckForce = -10;
            ani.SetBool("isDuck", false);
            playerCollider.enabled = true;
            pointColliderHolder.SetActive(false);
            duckCount = 0;
            duckForce = -6f;         
        }
    }
}
   

    private void HandleRollInput()
    {   
        
            if (isRoll && Input.GetKey(RollKey))
        {
                rb.velocity = new Vector2(Input.GetAxis("Horizontal") * rollSpeed, rb.velocity.y);
                isRoll = false;
                ani.SetBool("isRoll", true);
                dameSkill = 2;
                if (Input.GetKeyDown(RollKey))
            {
                    audio.PlaySFX(audio.acttack);

            }
        }
        
   
        else
        {
            isRoll = true;
            ani.SetBool("isRoll", false);
            dameSkill = 1;
        }
    }

// Reset Time behaviour
    private void HandleSurfCooldown(ref bool isSurfCooldown)
    {
        if (!isSurfCooldown && Time.time - lastSurfTime >= SurfCooldown)
        {
            lastSurfTime = Time.time;
         
            isSurfCooldown = !isSurfCooldown;
        }
    }

    private void Jump()
    {
        audio.PlaySFX(audio.jump);

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
       
    }

    private void Duck()
    {

        rb.velocity = new Vector2(rb.velocity.x, duckForce);
    }
private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = true;
        jumpForce = DefaultJumpForce;
        ani.SetBool("isJump", false);
        duckCount = 0;
        isSetJumping = true;
        jumpCount = 0;
    }
    else if (collision.gameObject.CompareTag("WallSliding"))
    {
        isWallClimbing = true;
        // jumpForce = 5f;
        wallCollider = collision.collider;
        ani.SetBool("isWall", true);
        isSetJumping = false;
    }  
    else if (collision.gameObject.CompareTag("Enemy"))
        {
            isGrounded = true;
            jumpForce = DefaultJumpForce;
            ani.SetBool("isJump", false);
            duckCount = 0;
            isSetJumping = true;
            jumpCount = 0;
        }
}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
          
        }
        if (collision.collider == wallCollider)
        {
            isWallClimbing = false;
            wallCollider = null;
            ani.SetBool("isWall", false);
        }
         
    }
        private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
