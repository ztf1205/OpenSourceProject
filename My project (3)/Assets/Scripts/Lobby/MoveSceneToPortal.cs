using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceneToPortal : MonoBehaviour
{
    public string nextScene;
    public GameObject Player;
    private int Flag = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 윗 방향키 입력시 Scene 이동
            if (Flag == 1)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌자가 플레이어이면
        if (collision.gameObject.tag == "Player")
        {
            Flag = 1;
        }
    }
    /*public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
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
