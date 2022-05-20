using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpLevel : MonoBehaviour
{
    Text ExpLv;
    // Start is called before the first frame update
    void Start()
    {
        ExpLv = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ExpLv.text = "Player Level = " + PlayerMove.GetPlayerLevel() + "\nExp : " + PlayerMove.GetPlayerExp();
    }
}
