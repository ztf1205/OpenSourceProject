using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradePlatform : MonoBehaviour
{
    [SerializeField]
    private int upgradeIdx = 0;
    private bool isbool;
    private bool tri;
    public Text Txt;
    void triggerOn()
    {
        tri = true;
    }
    private void Update()
    {
        if (tri == true && isbool == true && Input.GetAxisRaw("Vertical") == 1)
        {
            if(DataController.LobbyUpgrade(upgradeIdx) == true)
            {
                Txt.text = "Upgrade Success !!";
            }
            else
            {
                Txt.text = "Upgrade Fail..";
            }
            tri = false;
            Invoke("triggerOn", 1);
        }
    }
    public string getUpgradeName()
    {
        return DataController.GetName(upgradeIdx);
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
