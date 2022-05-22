using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlatform : MonoBehaviour
{
    [SerializeField]
    private int upgradeIdx = 0;
    private bool isbool;

    private void Update()
    {
        if (isbool == true && Input.GetAxisRaw("Vertical") == 1)
        {
            StartCoroutine(corou());
            
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
        }
    }
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        //플레이어와 닿아있는 상태에서 윗방향키 누르면 업그레이드 구매 시도
        if (isbool == true && Input.GetAxisRaw("Vertical") == 1)
        {
            DataController.LobbyUpgrade(upgradeIdx);
            Debug.Log("업그레이드 완료");
        }
    }*/
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isbool = false;
        }
    }
    /*void OnCollision2D(Collision2D collision)
    {
        //플레이어와 닿아있는 상태에서 윗방향키 누르면 업그레이드 구매 시도
        if (collision.gameObject.CompareTag("Player") && Input.GetAxisRaw("Vertical") == 1)
        {
            DataController.LobbyUpgrade(upgradeIdx);
            Debug.Log("업그레이드 완료");
        }
    }*/
    IEnumerator corou()
    {
        yield return new WaitForSeconds(0.6f);
        DataController.LobbyUpgrade(upgradeIdx);
    }
}
