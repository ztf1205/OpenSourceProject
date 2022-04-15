using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUp : MonoBehaviour
{
    [SerializeField]
    private int hpUp = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어와 충돌했다면 플레이어 최대 이속 증가 및 아이템 삭제
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerMove>().health += hpUp;
            Destroy(this.gameObject);
        }

    }

}
