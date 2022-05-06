using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int nextMove;

    private float health = 100f;
    private float enemyDamage = 25f;

    private float exp = 1f;

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

        Invoke("Think", 5);
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
        Invoke("Think", 5);
    }

    public void OnDamaged(float damage)
    {
        //체력 감소
        health -= damage;

        //체력이 없다면 사망 판정
        if (health <= 0)
        {
            //플레이어 경험치 획득
            player.GetComponent<PlayerMove>().expGain(exp);

            //Sprite Alpha
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);

            //Sprite Flip Y
            spriteRenderer.flipY = true;

            //Collider Disable
            capsuleCollider.enabled = false;

            //Die Effect Jump
            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

            //Destroy
            Invoke("DeActive", 5);
        }
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
