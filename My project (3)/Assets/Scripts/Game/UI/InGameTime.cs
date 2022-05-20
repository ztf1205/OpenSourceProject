using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameTime : MonoBehaviour
{
    Text Time;
    // Start is called before the first frame update
    void Start()
    {
        Time = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Time.text = "Time : " + PlayerMove.GetGameTime();
    }
}
