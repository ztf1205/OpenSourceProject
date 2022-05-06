using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject attack_Bullet;
    [SerializeField]
    private AudioClip audioGunFire;
    [SerializeField]
    private float attackCycle = 0.6f;

    private float timer = 0f;
    private AudioSource audioSource;

    void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioGunFire;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //시간을 재다가 싸이클이 돌면 타이머 초기화하고 불릿 발사하며 효과음 재생
        if (timer >= attackCycle - attackCycle * DataController.GetGameValue(Constants.COOLTIME_IDX))
        {
            //시간초기화
            timer = 0f;

            Attack();

        }

    }

    private void Attack()
    {
        //불릿 생성, 위치 할당, 회전
        GameObject bullet = Instantiate(attack_Bullet);
        bullet.transform.position = this.transform.position;

        //불릿 마우스 방향으로 회전
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bullet.transform.rotation = Quaternion.Euler(0f, 0f,
            Mathf.Atan2(mousePos.x - this.transform.position.x, mousePos.y - this.transform.position.y) * Mathf.Rad2Deg * -1 + 90);

        bullet.transform.localScale=new Vector3(bullet.transform.localScale.x+ bullet.transform.localScale.x * DataController.GetGameValue(Constants.PROJECTILESIZE_IDX)
            , bullet.transform.localScale.y + bullet.transform.localScale.y * DataController.GetGameValue(Constants.PROJECTILESIZE_IDX)
            , bullet.transform.localScale.z);

        //효과음 재생
        audioSource.Play();
    }

}
