using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpLevel : MonoBehaviour
{
    Text ExpLv;
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ExpLv = GetComponent<Text>();
    }
    void Update() // Player Level, Exp UI
    {
        ExpLv.text = "Player Level = " + player.GetComponent<PlayerMove>().GetPlayerLevel() + "\nExp : " + player.GetComponent<PlayerMove>().GetPlayerExp();
    }
}
