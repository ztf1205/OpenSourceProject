using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatTextUI : MonoBehaviour
{
    Text stat;
    //public static int scoreValue;
    void Start()
    {
        stat = GetComponent<Text>();
        // 디버그용 임시 초기화
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        stat.text = "ExpGain : " + DataController.GetLobbyValue(0) + "\nDamage : " + DataController.GetLobbyValue(1) + "\nMoveSpeed : " + DataController.GetLobbyValue(2)
            + "\nCoolTime : " + DataController.GetLobbyValue(3) + "\nProjectileSize : " + DataController.GetLobbyValue(4) + "\nHealthMax : " + DataController.GetLobbyValue(5);
    }
}