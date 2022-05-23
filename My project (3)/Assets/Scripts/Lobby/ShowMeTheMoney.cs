using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMeTheMoney : MonoBehaviour
{
    [SerializeField] private float amount;
    private bool isbool;
    private bool tri;
    void triggerOn()
    {
        tri = true;
    }
    private void Update()
    {
        if (tri == true && isbool == true && Input.GetAxisRaw("Vertical") == 1)
        {
            DataController.EarnMoney(amount);
            tri = false;
            Invoke("triggerOn", 1);
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
