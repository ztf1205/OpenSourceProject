using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthPoint : MonoBehaviour
{
    Text HP;
    void Start()
    {
        HP = GetComponent<Text>();
    }
    void Update() // HP UI
    {
        HP.text = "♥ HP - " + PlayerMove.GetHealthPoint();
    }
}
