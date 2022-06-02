using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*일시정지 메뉴를 담당하는 스크립트*/

public class PauseMenu : MonoBehaviour
{   

    public GameObject pauseMenuUI;      //퍼즈메뉴UI를 담당하는 오브젝트
    
    void Update()
    {
        //만일 ESC키 입력시 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //이미 일시정지 상태면 
            if (GameManager.GameIsPaused)
            {
                Resume(); //Resume()함수를 통해 버튼 입력시 계속 진행
            }
            //아니면
            else
            {
                Pause();    //정지
            }
        }
    }
    
    //게임을 계속 진행 시켜주는 함수
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.GameIsPaused = false;
    }

    //게임을 일시정지 시켜주는 함수
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameManager.GameIsPaused = true;
    }

    //버튼 클릭 시 로비2로 이동하는 함수
    public void LoadMenu()
    {
        DataController.GameStartReset();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby2");
        GameManager.GameIsPaused = false;
    }

    //버튼 클릭 시, 게임 종료 함수
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
