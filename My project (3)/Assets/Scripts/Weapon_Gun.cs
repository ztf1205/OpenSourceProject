using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Gun : MonoBehaviour
{
    public GameObject attack_Bullet;
    public AudioClip audioGunFire;
    float attackCycle = 0.6f;
    float timer = 0f;
    AudioSource audioSource;

    void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioGunFire;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //�ð��� ��ٰ� ����Ŭ�� ���� Ÿ�̸� �ʱ�ȭ�ϰ� �Ҹ� �߻��ϸ� ȿ���� ���
        if (timer >= attackCycle)
        {
            //�ð��ʱ�ȭ
            timer = 0f;

            //�Ҹ� ����, ��ġ �Ҵ�, ȸ��
            GameObject bullet = Instantiate(attack_Bullet);
            bullet.transform.position = this.transform.position;

            //�Ҹ� ���콺 �������� ȸ��
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bullet.transform.rotation = Quaternion.Euler(0f, 0f,
                Mathf.Atan2(mousePos.x - this.transform.position.x, mousePos.y - this.transform.position.y) * Mathf.Rad2Deg * -1 + 90);

            //ȿ���� ���
            audioSource.Play();

        }

    }
}
