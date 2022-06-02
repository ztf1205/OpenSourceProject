using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStat : MonoBehaviour
{
    Text stat;
    void Start()
    {
        stat = GetComponent<Text>();
    }
    void Update()
    {
        // GameUpgradeValue 출력 UI
        stat.text = "ExpGain : +" + DataController.GetGameValue(0) + "\nDamage : +" + DataController.GetGameValue(1) + "\nMoveSpeed : +" + DataController.GetGameValue(2)
            + "\nCoolTime : +" + DataController.GetGameValue(3) + "\nProjectileSize : +" + DataController.GetGameValue(4) + "\nMaxHealth : +" + DataController.GetGameValue(5)
            + "\n\n<color=green>Money</color> : " + DataController.GetMoney().ToString();
    }
}
