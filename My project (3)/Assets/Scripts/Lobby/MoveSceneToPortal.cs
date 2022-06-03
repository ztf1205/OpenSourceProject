using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceneToPortal : MonoBehaviour
{
    public string nextScene;
    public GameObject Player;
    private int Flag = 0;
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            // Flag On이면 윗 방향키 입력시 Scene 이동
            if (Flag == 1)
            {
                SceneManager.LoadScene(nextScene);
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
    // gameObject에 변화가 없으면 충돌검사 중지
    /*public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            // 윗 방향키 입력시 Scene 이동
            if (Flag == 1)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }*/
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Flag = 0;
        }
    }
}
