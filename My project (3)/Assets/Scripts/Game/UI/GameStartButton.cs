using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    //맨 처음 로비에서 스타트 버튼 입력시 로비2 씬으로 이동
    public void LoadMenu()
    {
        DataController.GameStartReset();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby2");
    }

}
