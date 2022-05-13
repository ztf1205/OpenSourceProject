using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatTextUI : MonoBehaviour
{
    Text stat;
    public static int scoreValue;
    // Start is called before the first frame update
    void Start()
    {
        stat = GetComponent<Text>();
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        stat.text = "ExpGain : " + DataController.GetLobbyValue(0) + "\nDamage : " + DataController.GetLobbyValue(1) + "\nMoveSpeed : " + DataController.GetLobbyValue(2)
            + "\nCoolTime : " + DataController.GetLobbyValue(3) + "\nProjectileSize : " + DataController.GetLobbyValue(4) + "\nHealthMax : " + DataController.GetLobbyValue(5);
    }
}