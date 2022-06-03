using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    public GameObject Player;
    private int Flag = 0;
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            // Flag On이면 윗 방향키 입력시 게임 종료
            if (Flag == 1)
            {
                Debug.Log("Quit...");
                Application.Quit();
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌자가 플레이어이면 Flag On
        if (collision.gameObject.tag == "Player")
        {
            Flag = 1;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Flag = 0;
        }
    }
}
