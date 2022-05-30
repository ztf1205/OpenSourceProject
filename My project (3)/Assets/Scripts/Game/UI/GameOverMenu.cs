using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameIsOver == true)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
         gameOverMenuUI.SetActive(true);
         Time.timeScale = 0f;
    }

    public void retry()
    {
        DataController.GameStartReset();
        gameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby2");
        GameManager.gameIsOver = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
