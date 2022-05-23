using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyInfo : MonoBehaviour
{
    Text info;
    // Start is called before the first frame update
    void Start()
    {
        info = GetComponent<Text>();
        info.text = "BestRecord = " + DataController.GetHighscore().ToString() + "Seconds"
            + "\nMoney : " + DataController.GetMoney().ToString();
    }
}
