using UnityEngine;

public class Enemy : MonoBehaviour, IDameReciever
{
    public float healthEnemy = 4;
    public float moveSpeedEnemy = 1f;
    public bool isFacingRight = true;
    private bool isHurt = false;
    private float hurtTimer = 0f;
    private Vector3 initEnemyPosition;
    private bool isMoveEnemy = true;
    PlayerController player;
    Animator animator;
    protected virtual void Start()
    {
        initEnemyPosition = transform.position;
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        HandleMovement();
        if (isHurt == true)
        {
            HandleHurt();
        }
        if (healthEnemy <= 0)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDead);
            ObseverManager.Instance.RemoveObsever(this);
            Destroy(gameObject);
        }
    }
    protected void HandleMovement()
    {
        Vector3 newPosition = transform.position;
        if (!isMoveEnemy)
            newPosition.x += moveSpeedEnemy * Time.deltaTime;
        else
            newPosition.x -= moveSpeedEnemy * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, initEnemyPosition.x - 1f, initEnemyPosition.x + 1f);
        transform.position = newPosition;

        if ((newPosition.x <= (initEnemyPosition.x - 1f) && isFacingRight) ||
            (newPosition.x >= (initEnemyPosition.x + 1f) && !isFacingRight))
        {
            isMoveEnemy = !isMoveEnemy;
            FlipObj();
        }
    }
    protected void HandleHurt()
    {
        hurtTimer += Time.deltaTime;
        if (hurtTimer >= 2f)
        {
            healthEnemy = 4;
            isHurt = false;
            moveSpeedEnemy = 1f;        
            hurtTimer = 0f;
            animator.SetBool("isHurt", false);
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerCollision(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            Debug.Log("Lava");
            ObseverManager.Instance.AddObsever(this);
            //DamageReciever(1);
        }
    }
    protected virtual void HandlePlayerCollision(Collision2D collision)
    {
        Vector2 collisionPoint = collision.contacts[0].point;
        if (player != null)
        {
            if (collisionPoint.y > transform.position.y)
            {
                DamageReciever(1);
                player.jumpCount = 0;
                isHurt = true;
            }
            else
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.hit);
                player.DamageReciever(1);
            }
        }
        else
        {
            Debug.Log("Player reference is null in HandlePlayerCollision");
        }
    }
    public void DamageReciever(int damage)
    {
        if (healthEnemy <= 0)
        {
            healthEnemy = 0;
            animator.SetBool("isHurt", false);
        }
        healthEnemy -= damage;
        moveSpeedEnemy = 0;
        hurtTimer = 0;
        animator.SetBool("isHurt", true);
    }
    public void FlipObj()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
