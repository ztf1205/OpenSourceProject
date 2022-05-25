using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    [SerializeField]
    private float bounce_Force = 20f;

    AudioSource audioSource;
    public AudioClip StepOnSound;

    private void Awake()
    {

        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce_Force, ForceMode2D.Impulse);
            this.audioSource.Play();
        }
    }
}
 