using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMeTheMoney : MonoBehaviour
{
    [SerializeField] private float amount;
    private bool isbool; // Player와의 충돌검사를 위한 boolean
    private bool tri; // delay를 주기위한 trigger
    void triggerOn()
    {
        tri = true;
    }
    private void Update()
    {
        // Player와 충돌이고 윗방향키 입력 시
        if (tri == true && isbool == true && Input.GetAxisRaw("Vertical") == 1)
        {
            DataController.EarnMoney(amount);
            tri = false;
            Invoke("triggerOn", 1); // delay
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isbool = true;
            tri = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isbool = false;
            tri = false;
        }
    }
}
