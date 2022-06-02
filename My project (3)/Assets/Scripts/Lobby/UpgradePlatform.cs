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

    AudioSource audioSource;
    public AudioClip StepOnSound;

    private void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    void triggerOn()
    {
        tri = true;
    }
    private void Update() // Log UI
    {
        if (tri == true && isbool == true && Input.GetAxisRaw("Vertical") == 1)
        {
            if(DataController.LobbyUpgrade(upgradeIdx) == true) // Upgrade 성공(true) 시
            {
                this.audioSource.Play();
                Txt.text = "Upgrade Success !!";
            }
            else if(DataController.GetLobbyUpgradeCost(upgradeIdx) > DataController.GetMoney()){ // 게임머니 부족할 시
                Txt.text = "Not Enough Money..";
            }
            else // Upgrade 불가능
            {
                Txt.text = "!!! Is Maximum Level !!!";
            }
            tri = false;
            Invoke("triggerOn", 1); // delay
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
