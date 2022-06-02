using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyInfo : MonoBehaviour
{
    Text info;
    void Start() // 최고점수와 게임머니 표시
    {
        info = GetComponent<Text>();
        info.text = "★ BestRecord = " + DataController.GetHighscore().ToString() + " Seconds"
            + "\n<color=green>Money</color> : " + DataController.GetMoney().ToString();
    }
}
