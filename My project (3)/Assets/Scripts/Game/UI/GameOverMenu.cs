using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//게임오버시 게임오버 창을 나타내게 해주는 스크립트
public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUI;       //UI오브젝트 변수

    // Update is called once per frame
    void Update()
    {
        // 만일 게임오버 상태라면
        if (GameManager.gameIsOver == true)
        {
            EndGame();
        }
    }

    //게임 오버 시
    public void EndGame()
    {
         gameOverMenuUI.SetActive(true);
         Time.timeScale = 0f;
    }


    //재시작 버튼 함수
    public void retry()
    {
        DataController.GameStartReset();
        gameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby2");
        GameManager.gameIsOver = false;
    }

    //게임 종료 함수
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
