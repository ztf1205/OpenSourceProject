using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction_Lobby : MonoBehaviour
{
    public void GameStart()
    {
        DataController.GameStartReset();//스탯 리셋
        SceneManager.LoadScene("Game");
    }


}
