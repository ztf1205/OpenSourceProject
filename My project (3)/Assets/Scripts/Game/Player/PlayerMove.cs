using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*플레이어 캐릭터의 모든 이동조건, 상태를 표시하는 스크립트*/
public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;

    private static float health = 100f;
    private float healthMax = 100f;
    private float healthMaxUpgrade;
    private float playerExp = 0f;
    private int level = 1;
    private int jumpCount = 2;

    private float gameTime = 0f;
    public GameObject levelUpText;
    public Transform hudPos;


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
        //캐릭터가 멈춰있으면
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //애니메이션
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
        //a, d로 입력 시 이동
        float h = Input.GetAxisRaw("Horizontal");
        float currentMaxSpeed = maxSpeed + maxSpeed * DataController.GetGameValue(Constants.MOVESPEED_IDX);

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > currentMaxSpeed) // 오른쪽 이동속도 최대치 조정
        {
            rigid.velocity = new Vector2(currentMaxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < currentMaxSpeed * (-1)) // 왼쪽 이동속도 최대치 조정
        {
            rigid.velocity = new Vector2(currentMaxSpeed * (-1), rigid.velocity.y);
        }

        //땅에 닿을 시 착지 하는 함수
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            RaycastHit2D baseRayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("BaseGround"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    jumpCount = 2;
                    anim.SetBool("isJumping", false);
                }
            }

            if (baseRayHit.collider != null)
            {
                if (baseRayHit.distance < 0.5f)
                {
                    jumpCount = 2;
                    anim.SetBool("isJumping", false);
                }
            }
        }
    }

    //몬스터와 충돌시 반응 함수
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 12)
        {
            OnDamaged(collision.transform.position, collision.gameObject.GetComponent<EnemyMove>().getDamage());
        }
    }

    //데미지 계산
    void OnDamaged(Vector2 targetPos, float enemyDamage)
    {
        // 체력 감소
        health -= enemyDamage;
        if (health <= 0)
        {
            OnDie();
            GameManager.gameIsOver = true;
        }

        // 레이어 변경을 통해 무적시간 적용
        gameObject.layer = 9;

        // 알파 값을 조정하여 무적시간 시각적 표현
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        // 데미지 입을 시 애니메이션
        anim.SetTrigger("doDamaged");

        //무적시간 적용
        Invoke("OffDamaged", 1);
    }

    //무적시간 끝날 때
    void OffDamaged()
    {
        // 레이어 변경을 통해 무적시간 종료
        gameObject.layer = 8;
        // 알파값을 조정하여 캐릭터 무적시간 시각적 적용
        spriteRenderer.color = new Color((133 / 255.0f), (217 / 255.0f), 1.0f, 1.0f);
    }

    //플레이어가 게임오버 될 조건 함수
    public void OnDie()
    {
        //점수 저장
        DataController.AddScore((int)gameTime);

        //점수 갱신
        DataController.HighscoreCheck();

        //색의 알파값을 조정해 시각적으로 죽음 표시
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Sprite Flip Y
        spriteRenderer.flipY = true;

        //충돌 조건 종료
        capsuleCollider.enabled = false;

        //죽을 때 이펙트
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
        playerExp += exp;
        if (level < 1)
        {
            TrylevelUp(5f);
        }
        else if (level < 5)
        {
            TrylevelUp(10f);
        }
        else if (level < 10)
        {
            TrylevelUp(20f);
        }
        else if (level < 15)
        {
            TrylevelUp(35f);
        }
        else if (level < 20)
        {
            TrylevelUp(50f);
        }
        else
        {
            TrylevelUp(80f);
        }
    }

    private void TrylevelUp(float levelUpExp)
    {
        while (playerExp >= levelUpExp)
        {
            playerExp -= levelUpExp;
            level++;
            // 레벨업 텍스트 출력
            GameObject ltext = Instantiate(levelUpText);
            ltext.transform.position = hudPos.position;
            if (DataController.RandomUpgrade() == false)
            {
                Heal(20f);
            }
        }
    }
    public static float GetHealthPoint()
    {
        return health;
    }
    public int GetPlayerLevel()
    {
        return level;
    }
    public float GetPlayerExp()
    {
        return playerExp;
    }
    public float GetGameTime()
    {
        return gameTime;
    }
}
