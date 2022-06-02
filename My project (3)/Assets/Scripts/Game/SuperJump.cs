using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//점프 발판을 구현해 놓은 스크립트

public class SuperJump : MonoBehaviour
{
    [SerializeField]
    private float bounce_Force = 20f;  //점프 힘을 나타내는 변수

    AudioSource audioSource;        //효과음
    public AudioClip StepOnSound;   //효과음을 저장하는 변수

    private void Awake()
    {

        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    //플레이어와 충돌 시 슈퍼점프
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce_Force, ForceMode2D.Impulse);
            this.audioSource.Play();
        }
    }
}
 