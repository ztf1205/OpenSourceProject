using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<데이터 컨트롤러 현재 기능>
 * 
 * -데이터 getter로 얻어오기
 * 
 * -게임 시작할 때 초기화
 * -게임에서 업그레이드 수행
 * -돈 얻기
 * -점수 추가
 * -최고점수 체크 및 자동 갱신
 * 
 * -랜덤 업그레이드
 * 
 * -로비에서 돈 충분하면 업그레이드 수행
 * 
 * -upgradeData.csv에서 업그레이드 정보 수정 가능
 * 
 * 
 * 
 * 
 * 
 * <추가할 것>
 * -패치나 버전 정보 넣어서 추가데이터 관리 가능하도록 변경(변경 가능성 있는 부분 명확하게 정하기)
 * 
 * -업그레이드 늘어날수록 비용 증가
 * -업그레이드 단계별 증가 수치
 * -무기별 업그레이드 추가
 * 
 */

public static class DataController
{
    //게임 시작시의 초기화
    public static void GameStartReset()
    {
        int i;

        //업그레이드 초기화
        for (i = 0; i <= Constants.UPGRADE_MAXIDX; i++)
        {
            upgradeData[i].LoadUpgrade();
        }

        //현재 점수 초기화
        score = 0f;
    }

    //게임에서 업그레이드 수행
    public static bool GameUpgrade(int upgradeIndex)
    {
        if (upgradeIndex >= 0 && upgradeIndex <= Constants.UPGRADE_MAXIDX)//유효한 인덱스라면
        {
            upgradeData[upgradeIndex].DoGameUpgrade();//업그레이드 수행
            return true;//정상 종료
        }
        else//유효하지 않은 인덱스라면 오류처리
        {
            Debug.Log("유효하지 않은 업그레이드 인덱스입니다.");
            return false;//비정상 종료
        }
    }

    //게임에서 랜덤한 업그레이드 수행
    public static bool RandomUpgrade()
    {
        int[] idxArr = new int[Constants.UPGRADE_MAXIDX + 1];
        int maxIdx = -1;

        //업그레이드가 가능한 업그레이드 인덱스의 배열 제작
        for (int i = 0; i <= Constants.UPGRADE_MAXIDX; i++)
        {
            if (DataController.upgradeData[i].IsMaxGameUpgrade())
            {
                idxArr[++maxIdx] = i;
            }
        }

        //풀업이라면 false 반환
        if (maxIdx == -1)
        {
            return false;
        }
        else//풀업이 아니라면 업그레이드 진행 후 true 반환
        {
            //배열 중에서 랜덤한 원소 뽑아서 해당 인덱스로 업그레이드 진행
            int randIdx = idxArr[UnityEngine.Random.Range(0, maxIdx)];
            DataController.upgradeData[randIdx].DoGameUpgrade();

            //true 반환
            return true;
        }



    }

    //입력하는 돈 만큼 돈 추가하고 저장
    public static void EarnMoney(float earnMoney)
    {
        money += earnMoney;
        SaveData();
    }

    public static void AddScore(float addScore)
    {
        score += addScore;
        SaveData();
    }

    //점수에 따라 하이스코어 체크하고 갱신여부 반환
    public static bool HighscoreCheck()
    {
        if (score > highscore)//기록 갱신
        {
            highscore = score;
            SaveData();
            return true;
        }
        else//기록 미갱신
        {
            return false;
        }
    }

    //로비에서 업그레이드 수행
    public static bool LobbyUpgrade(int upgradeIndex)
    {
        if (upgradeIndex >= 0 && upgradeIndex <= Constants.UPGRADE_MAXIDX)//유효한 인덱스라면
        {
            float cost = upgradeData[upgradeIndex].GetLobbyUpgradeCost();
            if (cost <= money)//돈이 충분하면 돈 지불하고 업그레이드
            {
                if (upgradeData[upgradeIndex].DoLobbyUpgrade())//업그레이드 성공했다면
                {
                    money -= cost;//돈 지불
                    SaveData();//변경 데이터 저장
                    return true;//정상 종료
                }
                else//이미 풀 업그레이드라면
                {
                    Debug.Log("이미 모든 업그레이드를 했습니다.");
                    return false;//비정상 종료
                }
            }
            else//돈이 충분하지 않으면 오류처리
            {
                Debug.Log("업그레이드 비용이 충분하지 않습니다.");
                return false;//비정상 종료
            }
        }
        else//유효하지 않은 인덱스라면 오류처리
        {
            Debug.Log("유효하지 않은 업그레이드 인덱스입니다.");
            return false;//비정상 종료
        }
    }




    //세팅
    private static float volume_music = 100f;//음악 볼륨
    private static float volume_effect = 100f;//효과음 볼륨

    //유저 데이터
    private static float score = 0f;//현재 점수
    private static float highscore = 0f;//최고 점수
    private static float money = 0f;//돈


    //세팅과 유저 데이터 getter
    public static float GetVolume_music()
    {
        return volume_music;
    }
    public static float GetVolume_effect()
    {
        return volume_effect;
    }
    public static float GetScore()
    {
        return score;
    }
    public static float GetHighscore()
    {
        return highscore;
    }
    public static float GetMoney()
    {
        return money;
    }

    //업그레이드 getter
    public static string GetName(int upgradeIndex)
    {
        return upgradeData[upgradeIndex].GetName();
    }
    public static float GetResetValue(int upgradeIndex)
    {
        return upgradeData[upgradeIndex].GetResetValue();
    }
    public static float GetGameValue(int upgradeIndex)
    {
        return upgradeData[upgradeIndex].GetGameValue();
    }
    public static float GetGameMaxValue(int upgradeIndex)
    {
        return upgradeData[upgradeIndex].GetGameMaxValue();
    }
    public static float GetGameUpgradeValue(int upgradeIndex)
    {
        return upgradeData[upgradeIndex].GetGameUpgradeValue();
    }
    public static float GetLobbyValue(int upgradeIndex)
    {
        return upgradeData[upgradeIndex].GetLobbyValue();
    }
    public static float GetLobbyMaxValue(int upgradeIndex)
    {
        return upgradeData[upgradeIndex].GetLobbyMaxValue();
    }
    public static float GetLobbyUpgradeValue(int upgradeIndex)
    {
        return upgradeData[upgradeIndex].GetLobbyUpgradeValue();
    }
    public static float GetLobbyUpgradeCost(int upgradeIndex)
    {
        return upgradeData[upgradeIndex].GetLobbyUpgradeCost();
    }


    //업그레이드

    private class upgrade//업그레이드 관련 정보와 기능 모은 클래스
    {
        private int index;//인덱스
        private string name;//이름
        private float resetValue;//초기값

        private float gameValue;//게임 현재값
        private float gameMaxValue;//게임 최대값
        private float gameUpgradeValue;//게임 업글값

        private float totalMaxValue;//게임과 로비를 합친 최대값

        private float lobbyValue;//로비 현재값
        private float lobbyMaxValue;//로비 최대값
        private float lobbyUpgradeValue;//로비 업글값
        private float lobbyUpgradeCost;//로비 업글비용

        public upgrade(int index, string name, float resetValue,
            float gameMaxValue, float gameUpgradeValue,
            float lobbyMaxValue, float lobbyUpgradeValue, float lobbyUpgradeCost)//생성자를 통해 초기화한다
        {
            this.index = index;
            this.name = name;
            this.resetValue = resetValue;

            this.gameMaxValue = gameMaxValue;
            this.gameUpgradeValue = gameUpgradeValue;

            this.lobbyMaxValue = lobbyMaxValue;
            this.lobbyUpgradeValue = lobbyUpgradeValue;
            this.lobbyUpgradeCost = lobbyUpgradeCost;

            LoadUpgrade();//현재값은 저장 데이터 로드
        }

        //Getter
        public string GetName()
        {
            return name;
        }
        public float GetResetValue()
        {
            return resetValue;
        }
        public float GetGameValue()
        {
            return gameValue;
        }
        public float GetGameMaxValue()
        {
            return gameMaxValue;
        }
        public float GetGameUpgradeValue()
        {
            return gameUpgradeValue;
        }
        public float GetLobbyValue()
        {
            return lobbyValue;
        }
        public float GetLobbyMaxValue()
        {
            return lobbyMaxValue;
        }
        public float GetLobbyUpgradeValue()
        {
            return lobbyUpgradeValue;
        }
        public float GetLobbyUpgradeCost()
        {
            return lobbyUpgradeCost;
        }

        public bool IsMaxGameUpgrade()
        {
            return gameValue >= totalMaxValue;
        }
        
        //업그레이드 로드
        public void LoadUpgrade()
        {
            if (PlayerPrefs.HasKey(name))//데이터가 있으면
            {
                lobbyValue = PlayerPrefs.GetFloat(name);//로드
            }
            else//데이터가 없으면
            {
                lobbyValue = resetValue;//초기값으로 세팅
                SaveUpgrade();//저장
            }
            gameValue = lobbyValue;//게임값을 로비값으로 초기화
            totalMaxValue = gameValue + gameMaxValue;//최대값 초기화

        }

        //업그레이드 저장
        public void SaveUpgrade()
        {
            PlayerPrefs.SetFloat(name, lobbyValue);//로비값만 저장한다.
        }

        //게임 업그레이드 수행
        public bool DoGameUpgrade()
        {
            //max값보다 작은 상태면 업그레이드 시행하고 시행 여부 반환
            if (IsMaxGameUpgrade() == false)
            {
                gameValue += gameUpgradeValue;
                return true;
            }
            else
            {
                return false;
            }
        }

        //로비 업그레이드 수행
        public bool DoLobbyUpgrade()
        {
            
            //max값보다 작은 상태면 업그레이드 시행하고 시행 여부 반환
            if (lobbyValue < lobbyMaxValue)
            {
                lobbyValue += lobbyUpgradeValue;
                SaveUpgrade();
                return true;
            }
            else
            {
                return false;
            }
        }

    };

    private static upgrade[] upgradeData;//업그레이드 데이터 배열

    static DataController()//정적 생성자, 게임 실행시 최초 1회 시행
    {
        //변수들 초기화 및 로드
        BuildUpgradeData();
        LoadData();
    }


    //csv 파일 이용해서 upgradeData 빌드
    private static void BuildUpgradeData()
    {
        upgradeData = new upgrade[Constants.UPGRADE_MAXIDX + 1];
        StreamReader sr = new StreamReader(Application.dataPath + "/Data/upgradeData.csv");

        string line = sr.ReadLine();//가장 윗줄 버리기
        for (int tmpIdx = 0; tmpIdx <= Constants.UPGRADE_MAXIDX; tmpIdx++)
        {
            // 한 줄씩 읽어온다.
            line = sr.ReadLine();
            // 쉼표( , )를 기준으로 데이터를 분리한다.
            string[] data = line.Split(',');

            upgradeData[tmpIdx] = new upgrade(
            int.Parse(data[0]), data[1], float.Parse(data[2]),//인덱스, 이름, 초기값
            float.Parse(data[3]), float.Parse(data[4]),//게임값-최대,업글
            float.Parse(data[5]), float.Parse(data[6]), float.Parse(data[7]));//로비값-최대,업글,업글비용
        }

    }


    private static void LoadData()
    {
        //기존 데이터 없으면 초기화
        if (PlayerPrefs.HasKey("volume_music") == false)
        {
            SaveData();
        }

        volume_music = PlayerPrefs.GetFloat("volume_music");
        volume_effect = PlayerPrefs.GetFloat("volume_effect");

        highscore = PlayerPrefs.GetFloat("highscore");
        money = PlayerPrefs.GetFloat("money");
    }

    private static void SaveData()
    {

        PlayerPrefs.SetFloat("volume_music", volume_music);
        PlayerPrefs.SetFloat("volume_effect", volume_effect);

        PlayerPrefs.SetFloat("highscore", highscore);
        PlayerPrefs.SetFloat("money", money);

    }

}