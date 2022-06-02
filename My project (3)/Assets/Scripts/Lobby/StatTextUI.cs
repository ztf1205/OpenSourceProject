using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatTextUI : MonoBehaviour
{
    Text stat;
    void Start()
    {
        stat = GetComponent<Text>();
        // 디버그용 임시 초기화
        PlayerPrefs.DeleteAll();
    }
    void Update()
    {
        // LobbyUpgradeValue 출력 UI
        stat.text = "ExpGain : +" + DataController.GetLobbyValue(0) + "\nDamage : +" + DataController.GetLobbyValue(1) + "\nMoveSpeed : +" + DataController.GetLobbyValue(2)
            + "\nCoolTime : +" + DataController.GetLobbyValue(3) + "\nProjectileSize : +" + DataController.GetLobbyValue(4) + "\nMaxHealth : +" + DataController.GetLobbyValue(5)
            + "\n\n<color=green>Money</color> : " + DataController.GetMoney().ToString();
    }
}