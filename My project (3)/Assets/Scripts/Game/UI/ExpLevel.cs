using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpLevel : MonoBehaviour
{
    Text ExpLv;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ExpLv = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ExpLv.text = "Player Level = " + player.GetComponent<PlayerMove>().GetPlayerLevel() + "\nExp : " + player.GetComponent<PlayerMove>().GetPlayerExp();
    }
}
