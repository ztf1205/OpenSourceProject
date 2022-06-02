using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//순간이동 포탈을 구현해 놓은 스크립트

public class Teleportation : MonoBehaviour
{
    public GameObject Portal;       //포탈 오브젝트 변수
    GameObject Player;              // 플레이어 변수
    private int P_Flag = 0;         // 포탈에 들어왔는지 체크하는 변수

    //윗 방향키 입력 시 
    private void Update()
    {
        if ((Input.GetAxisRaw("Vertical") == 1))
        {
            //윗방향키나 w를 입력시 순간이동
            if (P_Flag == 1)
            {
                //다른 포탈 위치로 순간이동
                StartCoroutine(Teleport());
            }
        }
    }

    void Start()
    {
        //플레이어 태그를 찾아 오브젝트 변수에 삽입
        Player = GameObject.FindWithTag("Player");

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌자가 플레이어이면
        if (collision.gameObject.tag == "Player")
        {

            P_Flag = 1;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //나오면 
        if (collision.gameObject.tag == "Player")
        {
            P_Flag = 0;
        }
    }

    // 이 함수를 이용하면 순간이동에 딜레이를 줄 수 있음
    IEnumerator Teleport()
     {
        yield return new WaitForSeconds(0.6f); //0.6초의 딜레이
        Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y - GetComponent<BoxCollider2D>().size.y + Player.GetComponent<CapsuleCollider2D>().size.y / 2);
    }
}
