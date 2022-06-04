using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*땅에 있는 적의 움직임, 상태를 표시하는 스크립트*/
public class EnemyMove : MonoBehaviour
{
    public GameObject treasure;
    public GameObject heal;
    public bool isElite = false;

    public int nextMove;

    private float dropChance_treasure = 0.0f;
    private float dropChance_heal = 0.05f;

    private float health = 100f;        //적 체력
    private float enemyDamage = 25f;    //적의 공격력
    private float exp = 1f;             //처지 시, 주는 경험치
    private float money = 1f;           //처지 시, 주는 돈

    private bool deathFlag = false;     //죽었는지 체크하는 변수
    private float deathTimer = 0f; 

    //데미지 받을시
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

        Invoke("Think", 5f);        //딜레이 시간 5초
    }

    private void Start()
    {
        //엘리트몹 세팅
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
        //움직임
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //땅인지 공중인지 체크
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
        //사망처리
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
        //다음 움직임을 생각하는 함수
        nextMove = Random.Range(-1, 2);

        //애니메이션
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

            //알파값을 조정하여 데미지 받은 것을 시각적 표현
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);

            //Sprite Flip Y
            spriteRenderer.flipY = true;

            //충돌조건 삭제
            capsuleCollider.enabled = false;

            //죽었을 때 점프하며 죽음
            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
    }
}
