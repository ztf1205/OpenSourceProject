using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameTime : MonoBehaviour
{
    Text Time;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Time = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Time.text = "Time : " + string.Format("{0:N1}", player.GetComponent<PlayerMove>().GetGameTime()) + "Sec";
    }
}
