using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class PlayerController : MonoBehaviour, IDameReciever
{
    //GameObjhInHierarchy
    public CircleCollider2D PlayerCollider;
    public bool isFacingRight = true;
    public bool isGrounded = true;
    public float jumpCount = 0;
    public float maxJumpCount = 2;
    public float healthPlayer = 20f;
    public float xDirection = 0f;
    //PrivateObject
    [SerializeField] private GameObject PointColiiderHolder;
    [SerializeField] private StateManager stateManager;
    private bool isCanRoll = true;
    private bool isWallClimbing = false;
    private int damePlayer = 2;
    private float healthPlayerMax = 20f;
    public static PlayerController Instance => instance;
    private static PlayerController instance;
    private Rigidbody2D rb;
    private PlayerManager controls;
    private HealthCavans hc;

    private void Awake()
    {
        stateManager = GetComponent<StateManager>();
        rb = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<CircleCollider2D>();
        hc = FindObjectOfType<HealthCavans>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (stateManager == null)
        {
            Debug.LogError("StateManager is null in PlayerController.");
        }
        stateManager.ChangeState(new IdlePlayer(this));

        controls = new PlayerManager();
        controls.Enable();
        controls.Stomper.Movement.performed += ctx =>
        {
            xDirection = ctx.ReadValue<float>();
            if (xDirection != 0)
            {
                stateManager.ChangeState(new WalkPlayer(this));
            }
        };

        controls.Stomper.Jump.performed += ctx =>
        {
            if (jumpCount < maxJumpCount)
            {
                stateManager.ChangeState(new JumpPlayer(this));
            }
        };
        controls.Stomper.Roll.performed += ctx =>
        {
            if (isCanRoll && xDirection != 0)
            {
                isCanRoll = false;
                stateManager.ChangeState(new RollPlayer(this));
                StartCoroutine(WaitSkillPlayer());
            }
        };

        controls.Stomper.Duck.performed += ctx =>
        {
            stateManager.ChangeState(new DuckPlayer(this));
        };
    }
    protected void Update()
    {
        hc.updateHealthBar(healthPlayer, healthPlayerMax);
        if(healthPlayer <= 0)
        {
            UIController.Instance.GameOver();
        }
        //float xDirection = Input.GetAxis("Horizontal");
        if (xDirection == 0 && isGrounded && !isWallClimbing)
        {
            stateManager.ChangeState(new IdlePlayer(this));
        }
        //else if (xDirection != 0 && isCanRoll)
        //{
        //    stateManager.ChangeState(new WalkPlayer(this));
        //}
        //if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        //{
        //    stateManager.ChangeState(new JumpPlayer(this));
        //}
        //if (Input.GetKeyDown(KeyCode.J) && isCanRoll && xDirection != 0)
        //{
        //    isCanRoll = false;
        //    stateManager.ChangeState(new RollPlayer(this));
        //    StartCoroutine(WaitSkillPlayer());
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    stateManager.ChangeState(new DuckPlayer(this));
        //    PointColiiderHolder.SetActive(true);
        //    PlayerCollider.enabled = false;
        //}
        //else
        //{
        //    PointColiiderHolder.SetActive(false);
        //    PlayerCollider.enabled = true;
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
        if (collision.gameObject.CompareTag("Enemy") && !isCanRoll)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.DamageReciever(2);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            ObseverManager.Instance.AddObsever(this);
        }
    }
    private IEnumerator WaitSkillPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        isCanRoll = true;
    }

    public void DamageReciever(int damage)
    {
        healthPlayer -= damage;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hit);
      
    }
    public void FlipObj()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void OnEnable()
    {
        controls.Enable();
    }
    public void OnDisable()
    {
        controls.Disable();
    }

}
