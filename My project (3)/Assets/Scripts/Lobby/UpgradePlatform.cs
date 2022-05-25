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
    private void Update()
    {
        if (tri == true && isbool == true && Input.GetAxisRaw("Vertical") == 1)
        {
            if(DataController.LobbyUpgrade(upgradeIdx) == true)
            {
                this.audioSource.Play();
                Txt.text = "Upgrade Success !!";
            }
            else if(DataController.GetLobbyUpgradeCost(upgradeIdx) > DataController.GetMoney()){
                Txt.text = "Not Enough Money..";
            }
            else
            {
                Txt.text = "!!! Is Maximum Level !!!";
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
