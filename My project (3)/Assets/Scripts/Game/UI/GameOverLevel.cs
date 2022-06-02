using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*게임오버를 했을 때, 플레이어의 최종 레벨 표시*/
public class GameOverLevel : MonoBehaviour
{
    Text ExpLv;         //레벨 텍스트
    GameObject player;  //플레이어 변수

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ExpLv = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ExpLv.text = "Level = " + player.GetComponent<PlayerMove>().GetPlayerLevel();
    }
}
