using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private float money = 100f;
    public GameObject UpgradeText;
    public Transform hudPos;

    AudioSource audioSource;
    public AudioClip StepOnSound;

    private void Awake()
    {

        audioSource = this.gameObject.GetComponent<AudioSource>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어와 충돌했다면 랜덤 업그레이드 or 돈 획득
        if (collision.gameObject.tag == "Player")
        {
            this.audioSource.Play();
            if (DataController.RandomUpgrade() == false)
            {
                DataController.EarnMoney(money);
            }
            GameObject upText = Instantiate(UpgradeText);
            upText.transform.position = hudPos.position;
            Destroy(this.gameObject);
        }

    }
}
