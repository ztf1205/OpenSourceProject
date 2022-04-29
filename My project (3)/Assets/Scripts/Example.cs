//예제 코드

public class Example
{

    //데이터 컨트롤러 사용 예제코드입니다.
    void DataControllerUseExample()
    {
        //게임 시작 리셋
        DataController.GameStartReset();

        //업그레이드
        //1.단순 업그레이드
        DataController.GameUpgrade(Constants.MOVESPEED_IDX);
        //2.업그레이드 + 오류처리
        if (DataController.LobbyUpgrade(Constants.MOVESPEED_IDX))
        {
            //업그레이드 성공처리
        }
        else
        {
            //업그레이드 실패처리
        }

        //getter 사용-업그레이드는 인덱스로 접근
        DataController.GetMoney();
        DataController.GetGameValue(Constants.MOVESPEED_IDX);

        //돈 얻기
        float goldCoinMoney = 100f;
        DataController.EarnMoney(goldCoinMoney);

        //점수 얻기
        float enemyKillScore = 100f;
        DataController.AddScore(enemyKillScore);

        //하이스코어 체크
        DataController.HighscoreCheck();
    }

}
