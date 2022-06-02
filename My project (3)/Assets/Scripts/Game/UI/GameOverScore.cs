using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*게임오버 했을 시, 현재 최종 점수를 표시하는 스크립트*/
public class GameOverScore : MonoBehaviour
{
    Text Score;         //텍스트 스코어 변수

    // Start is called before the first frame update
    void Start()
    {
        Score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score : " + DataController.GetScore();
    }
}
