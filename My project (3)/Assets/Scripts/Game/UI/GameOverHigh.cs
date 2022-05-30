using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHigh : MonoBehaviour
{
    Text HighScore;
    
    // Start is called before the first frame update
    void Start()
    {
        HighScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        HighScore.text = "HighScore : " + DataController.GetHighscore();
    }
}
