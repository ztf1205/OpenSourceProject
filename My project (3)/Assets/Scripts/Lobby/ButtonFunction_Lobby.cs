using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction_Lobby : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }


}
