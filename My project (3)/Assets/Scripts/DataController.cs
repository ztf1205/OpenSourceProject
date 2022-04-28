using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<데이터 컨트롤러 현재 기능>
 * 
 * -게임 시작할 때 초기화
 * -게임에서 업그레이드 수행
 * 
 * -로비에서 돈이 있으면 업그레이드를 수행
 * 
 * -upgradeData.csv에서 업그레이드 정보 수정 가능합니다.(초기값,최대값,업글값,비용)
 * -이후 엑셀로 값 수정하도록 변경할 예정입니다
 * 
 * <추가할 것>
 * -데이터 정보 접근 가능하도록-설정,유저 데이터, 업그레이드 정보 getter 만들기
 * -설정하고 하이스코어, 돈 변경가능하도록
 * 
 * -패치나 버전 정보 넣어서 추가데이터 관리 가능하도록 변경(변경 가능성 있는 부분 명확하게 정하기)
 * 
 * -업그레이드 늘어날수록 비용 증가
 * -업그레이드 단계별 증가 수치
 * -무기별 업그레이드 추가
 * 
 */

public static class DataController
{

    //게임 시작할 때 업그레이드 초기화
    public static void GameStartReset()
    {
        int i;

        for (i = 0; i <= Constants.UPGRADE_MAXIDX; i++)
        {
            upgradeData[i].LoadUpgrade();
        }
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

    //로비에서 업그레이드 수행
    public static bool LobbyUpgrade(int upgradeIndex)
    {
        if (upgradeIndex >= 0 && upgradeIndex <= Constants.UPGRADE_MAXIDX)//유효한 인덱스라면
        {
            float cost = upgradeData[upgradeIndex].GetLobbyUpgradeCost();
            if (cost <= money)//돈이 충분하면 돈 지불하고 업그레이드
            {
                money -= cost;
                upgradeData[upgradeIndex].DoLobbyUpgrade();
                return true;//정상 종료
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
    private static float highscore = 0f;//최고 점수
    private static float money = 0f;//돈

    //업그레이드

    private class upgrade//업그레이드 관련 정보와 기능 모은 클래스
    {
        private int index;//인덱스
        private string name;//이름
        private float resetValue;//초기값

        private float gameValue;//게임 현재값
        private float gameMaxValue;//게임 최대값
        private float gameUpgradeValue;//게임 업글값

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

        //각 멤버변수들은 모두 Getter로 접근 가능
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

        }

        //업그레이드 저장
        public void SaveUpgrade()
        {
            PlayerPrefs.SetFloat(name, lobbyValue);//로비값만 저장한다.
        }

        //게임 업그레이드 수행
        public void DoGameUpgrade()
        {
            gameValue += gameUpgradeValue;
        }

        //로비 업그레이드 수행
        public void DoLobbyUpgrade()
        {
            lobbyValue += lobbyUpgradeValue;
            SaveUpgrade();
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
        upgradeData = new upgrade[Constants.UPGRADE_MAXIDX];
        StreamReader sr = new StreamReader(Application.dataPath + "/" + "upgradeData.csv");

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
            float.Parse(data[5]), float.Parse(data[6]), float.Parse(data[6]));//로비값-최대,업글,업글비용
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