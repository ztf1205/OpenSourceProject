using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Player;
    bool Pressed;

    //윗 방향키 입력 시 (아직 구현이 안됨)
    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Pressed = true;
            Debug.Log("press");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌자가 플레이어이면
        if (collision.gameObject.tag == "Player")
        {
            //윗방향키나 w를 입력시 순간이동
            //if (Pressed)
           // {
           //다른 포탈 위치로 순간이동
                  Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
           // }
            // StartCoroutine(Teleport());
        }
    }

    // 이 함수를 이용하면 순간이동에 딜레이를 줄 수 있음
    //IEnumerator Teleport()
    // {
    //     yield return new WaitForSeconds(0);
    //     Player.transform.position = new Vector2(Portal.transform.position.x + 1, Portal.transform.position.y);
    // }
}
