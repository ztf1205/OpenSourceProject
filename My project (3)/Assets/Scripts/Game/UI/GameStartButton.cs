using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    public void LoadMenu()
    {
        DataController.GameStartReset();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby2");
        GameManager.GameIsPaused = false;
    }

}
