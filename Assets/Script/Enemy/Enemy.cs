using UnityEngine;
public class Enemy : MonoBehaviour, IDameReciever
{
    protected bool isFacingLeft = true;
    protected bool isMoveEnemy = true;
    public float healthEnemy = 4;
    public float moveSpeedEnemy = 1f;
    protected bool isHurt = false;
    protected float hurtTimer = 0f;
    protected Vector3 initEnemyPosition;
    Player player;
    AudioManager audio;
    Animator animator;
   
    protected virtual void Start()
    {
        ObseverManager.Instance.AddObsever(this);
        initEnemyPosition = transform.position;
        player = FindObjectOfType<Player>();
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        HandleMovement();
        if (isHurt) HandleHurt();

        if (healthEnemy <= 0)
        {
            audio.PlaySFX(audio.enemyDead);
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
        if (newPosition.x <= (initEnemyPosition.x - 1f) || newPosition.x >= (initEnemyPosition.x + 1f))
        {
            isMoveEnemy = !isMoveEnemy;
            Flip.Instance.FlipObj();
        }
    }
    protected void HandleHurt()
    {
        hurtTimer += Time.deltaTime;
        if (hurtTimer >= 2f)
        {
            healthEnemy = 2;
            isHurt = false;
            hurtTimer = 0f;
            animator.SetBool("isHurt", false);
            moveSpeedEnemy = 1f;
        }
    }
  
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            HandlePlayerCollision(collision);
    }
    protected virtual void HandlePlayerCollision(Collision2D collision)
    {
        Vector2 collisionPoint = collision.contacts[0].point;

        // Check if player is not null before accessing its properties/methods
        if (player != null)
        {
            if (collisionPoint.y > transform.position.y)
            {
                DamageReciever(player.dameSkill);
            }
            else if (player.ani.GetBool("isRoll"))
            {

                healthEnemy -= player.dameSkill;
            }
            else
            {
                audio.PlaySFX(audio.hit);

                player.healthPlayer--;
            }
        }
        else
        {
            Debug.Log("Player reference is null in HandlePlayerCollision");
        }
    }
    public void DamageReciever(int damage)
    {
        healthEnemy -= damage;
        moveSpeedEnemy = 0;
        hurtTimer = 0;
        animator.SetBool("isHurt", true);
    }
}
