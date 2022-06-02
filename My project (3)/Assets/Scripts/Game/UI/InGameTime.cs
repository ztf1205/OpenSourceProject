using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameTime : MonoBehaviour
{
    Text Time;
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Time = GetComponent<Text>();
    }
    void Update() // 소수점 한자리 gameTime UI
    {
        Time.text = "Time : " + string.Format("{0:N1}", player.GetComponent<PlayerMove>().GetGameTime()) + " Sec";
    }
}
