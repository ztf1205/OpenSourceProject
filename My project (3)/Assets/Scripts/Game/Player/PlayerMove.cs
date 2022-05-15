using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;

    private float health = 100f;
    private float healthMax = 100f;
    private float healthMaxUpgrade;
    private float playerExp = 0f;
    private int level = 1;
    private int jumpCount = 2;

    private float gameTime = 0f;

    public GameManager gameManager;


    CapsuleCollider2D capsuleCollider;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        healthMaxUpgrade = healthMax + healthMax * DataController.GetGameValue(Constants.HEALTHMAX_IDX);
        health = healthMaxUpgrade;
        jumpCount = 0;
    }

    void Update()
    {
        //시간 갱신
        gameTime += Time.deltaTime;


        //최대 체력 변경 여부 검사
        if (healthMaxUpgrade < healthMax + healthMax * DataController.GetGameValue(Constants.HEALTHMAX_IDX))//다르다면
        {
            //추가된 만큼 회복
            Heal(healthMax + healthMax * DataController.GetGameValue(Constants.HEALTHMAX_IDX) - healthMaxUpgrade);
            //갱신
            healthMaxUpgrade = healthMax + healthMax * DataController.GetGameValue(Constants.HEALTHMAX_IDX);
        }


        //Jump

        if (jumpCount > 0)
            {
            if (Input.GetButtonDown("Jump"))
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                anim.SetBool("isJumping", true);
                    
                jumpCount -= 1;
                
                Debug.Log(jumpCount);
            }
        }
        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //Move By Key Control
        float h = Input.GetAxisRaw("Horizontal");
        float currentMaxSpeed = maxSpeed + maxSpeed * DataController.GetGameValue(Constants.MOVESPEED_IDX);

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > currentMaxSpeed) // Right Max Speed
        {
            rigid.velocity = new Vector2(currentMaxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < currentMaxSpeed * (-1)) // Left Max Speed
        {
            rigid.velocity = new Vector2(currentMaxSpeed * (-1), rigid.velocity.y);
        }

        //Landing Platform
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    jumpCount = 2;
                    anim.SetBool("isJumping", false);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 12)
        {
            OnDamaged(collision.transform.position, collision.gameObject.GetComponent<EnemyMove>().getDamage());
        }
    }

    void OnDamaged(Vector2 targetPos, float enemyDamage)
    {
        // Health Down
        health -= enemyDamage;
        if (health <= 0)
        {
            OnDie();
        }

        // Change Layer
        gameObject.layer = 9;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        // Animation
        anim.SetTrigger("doDamaged");

        Invoke("OffDamaged", 1);
    }

    void OffDamaged()
    {
        // Change Layer
        gameObject.layer = 8;
        // View Alpha
        spriteRenderer.color = new Color((133 / 255.0f), (217 / 255.0f), 1.0f, 1.0f);
    }

    public void OnDie()
    {
        //점수 저장
        DataController.AddScore((int)gameTime);

        //점수 갱신
        DataController.HighscoreCheck();

        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Sprite Flip Y
        spriteRenderer.flipY = true;

        //Collider Disable
        capsuleCollider.enabled = false;

        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

    }

    public void Heal(float heal)
    {
        float currentHealthMax = healthMax + healthMax * DataController.GetGameValue(Constants.HEALTHMAX_IDX);
        health += heal;
        if (health > currentHealthMax)
        {
            health = currentHealthMax;
        }
    }

    public void expGain(float exp)
    {
        levelUpCheck(1f);
        playerExp += exp;
        if (level < 1)
        {
            levelUpCheck(3f);
        }
        else if (level < 5)
        {
            levelUpCheck(5f);
        }
        else if (level < 10)
        {
            levelUpCheck(8f);
        }
        else if (level < 15)
        {
            levelUpCheck(10f);
        }
        else if (level < 20)
        {
            levelUpCheck(12f);
        }
        else
        {
            levelUpCheck(15f);
        }
    }

    private bool levelUpCheck(float levelUpExp)
    {
        if (playerExp >= levelUpExp)
        {
            playerExp -= levelUpExp;
            level++;
            if (DataController.RandomUpgrade() == false)
            {
                Heal(20f);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
