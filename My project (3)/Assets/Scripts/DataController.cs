using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    //settings
    private float volume_music;
    private float volume_effect;

    //user data
    private float highscore;
    private float money;

    //upgrade
    private class upgrade
    {
        private string name;
        private float value;
        private float resetValue;
        private float maxValue;
        private float gameUpgradeValue;
        private float lobbyUpgradeValue;

        public upgrade(string _name, float _resetValue, float _maxValue, float _gameUpgradeValue, float _lobbyUpgradeValue)
        {
            name = _name;
            resetValue = _resetValue;
            maxValue = _maxValue;
            gameUpgradeValue = _gameUpgradeValue;
            lobbyUpgradeValue = _lobbyUpgradeValue;

            LoadUpgrade();
        }

        public string GetName() { return name; }
        public float GetValue() { return value; }
        public float GetResetValue() { return resetValue; }
        public float GetMaxValue() { return maxValue; }
        public float GetGameUpgradeValue() { return gameUpgradeValue; }
        public float GetLobbyUpgradeValue() { return lobbyUpgradeValue; }

        public void LoadUpgrade()
        {
            if (PlayerPrefs.HasKey(name))
            {
                value = PlayerPrefs.GetFloat(name);
            }
            else
            {
                value = resetValue;
                SaveUpgrade();
            }
            
        }

        public void SaveUpgrade()
        {
            PlayerPrefs.SetFloat(name, value);
        }

        public void DoGameUpgrade()
        {
            value += gameUpgradeValue;
        }

        public void doLobbyUpgrade()
        {
            value += lobbyUpgradeValue;
            SaveUpgrade();
        }

    };

    private static upgrade[] upgradeData;
    private static Hashtable upgradeHt;
    private static bool buildUpgradeFlag = false;
    private static int upgradeMaxIndex = -1;


    private void Awake()
    {
        if (buildUpgradeFlag == false)
        {
            buildUpgradeFlag = true;
            MakeUpgradeHt();
            BuildUpgradeData();
        }
        LoadData();
    }

    private void MakeUpgradeHt()
    {
        upgradeHt = new Hashtable();
        upgradeMaxIndex = -1;
        upgradeHt.Add("expGain", ++upgradeMaxIndex);
        upgradeHt.Add("damageIncrease", ++upgradeMaxIndex);
        upgradeHt.Add("moveSpeedIncrease", ++upgradeMaxIndex);
        upgradeHt.Add("cooltimeReduce", ++upgradeMaxIndex);
        upgradeHt.Add("projectileIncrease", ++upgradeMaxIndex);
        upgradeHt.Add("healthMax", ++upgradeMaxIndex);
        
    }

    private void BuildUpgradeData()
    {
        upgradeData = new upgrade[upgradeMaxIndex];
        upgradeData[(int)upgradeHt["expGain"]] = new upgrade("expGain", 1f, 5f, 1f, 1f);
        upgradeData[(int)upgradeHt["damageIncrease"]] = new upgrade("damageIncrease", 1f, 5f, 1f, 1f);
        upgradeData[(int)upgradeHt["moveSpeedIncrease"]] = new upgrade("moveSpeedIncrease", 1f, 5f, 1f, 1f);
        upgradeData[(int)upgradeHt["cooltimeReduce"]] = new upgrade("cooltimeReduce", 1f, 5f, 1f, 1f);
        upgradeData[(int)upgradeHt["projectileIncrease"]] = new upgrade("projectileIncrease", 1f, 5f, 1f, 1f);
        upgradeData[(int)upgradeHt["healthMax"]] = new upgrade("healthMax", 100f, 200f, 10f, 10f);

    }



    public void SaveData()
    {
        if (PlayerPrefs.HasKey("volume_music") == false)
        {
            ResetData();
        }

        PlayerPrefs.SetFloat("volume_music", volume_music);
        PlayerPrefs.SetFloat("volume_effect", volume_effect);

        PlayerPrefs.SetFloat("highscore", highscore);
        PlayerPrefs.SetFloat("money", money);

    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("volume_music") == false)
        {
            ResetData();
            SaveData();
        }

        volume_music = PlayerPrefs.GetFloat("volume_music");
        volume_effect = PlayerPrefs.GetFloat("volume_effect");

        highscore = PlayerPrefs.GetFloat("highscore");
        money = PlayerPrefs.GetFloat("money");
    }


    private void ResetData()
    {
        volume_music = 100f;
        volume_effect = 100f;

        highscore = 0f;
        money = 0;
    }

    public void GameUpgrade(string upgradeName)
    {
        if (upgradeHt.Contains(upgradeName))
        {
            upgradeData[(int)upgradeHt[upgradeName]].DoGameUpgrade();
        }
    }

    public void LobbyUpgrade(string upgradeName)
    {
        if (upgradeHt.Contains(upgradeName))
        {
            upgradeData[(int)upgradeHt[upgradeName]].doLobbyUpgrade();
        }
    }

}
