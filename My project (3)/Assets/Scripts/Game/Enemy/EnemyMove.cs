using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject treasure;
    public GameObject heal;
    public bool isElite = false;

    public int nextMove;

    private float dropChance_treasure = 0.0f;
    private float dropChance_heal = 0.05f;

    private float health = 100f;
    private float enemyDamage = 25f;
    private float exp = 1f;
    private float money = 1f;

    private bool deathFlag = false;
    private float deathTimer = 0f;

    public float getDamage()
    {
        return enemyDamage;
    }
    GameObject player;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    Animator anim;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        player = GameObject.FindWithTag("Player");

        Invoke("Think", 5f);
    }

    private void Start()
    {
        if (isElite)
        {
            dropChance_treasure = 0.2f;
            dropChance_heal = 0.0f;

            health = 500f;
            enemyDamage = 50f;
            exp = 5f;
            money = 5f;

            GetComponent<FollowPlayer>().speed *= 0.7f;
            transform.localScale = new Vector3(2f, 2f, 1f);
            spriteRenderer.color = new Color(1f, 0f, 1f, 1f);
        }
    }


    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Flatform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    private void Update()
    {
        if (deathFlag)
        {
            deathTimer += Time.deltaTime;
            spriteRenderer.color = new Color(1, 1, 1, 0.4f-deathTimer/2);
        }
        if (deathTimer > 0.8f)
        {
            Destroy(this.gameObject);
        }
    }

    void Think()
    {
        //Set Next Active
        nextMove = Random.Range(-1, 2);

        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

        //Flip Sprite
        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 1f);
    }

    public void OnDamaged(float damage)
    {
        //체력 감소
        health -= damage;

        //체력이 없다면 사망 판정
        if (health <= 0 && deathFlag == false)
        {
            //플래그 변경
            deathFlag = true;

            //플레이어 경험치 획득
            player.GetComponent<PlayerMove>().expGain(exp + exp * DataController.GetGameValue(Constants.EXPGAIN_IDX));

            //돈 획득
            DataController.EarnMoney(money);

            //보물상자 드랍
            if (Random.Range(0, 100) <= dropChance_treasure*100)
            {
                Instantiate(treasure, transform.position, Quaternion.identity);
            }

            //회복 아이템 드랍
            if (Random.Range(0, 100) <= dropChance_heal * 100)
            {
                Instantiate(heal, transform.position, Quaternion.identity);
            }
            //플레이어 추적 중지
            FollowPlayer followPlayer;
            if (TryGetComponent<FollowPlayer>(out followPlayer))
            {
                GetComponent<FollowPlayer>().speed = 0f;
            }

            //Sprite Alpha
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);

            //Sprite Flip Y
            spriteRenderer.flipY = true;

            //Collider Disable
            capsuleCollider.enabled = false;

            //Die Effect Jump
            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
    }
}
