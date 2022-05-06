using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlatform : MonoBehaviour
{
    [SerializeField]
    private int upgradeIdx = 0;

    

    public string getUpgradeName()
    {
        return DataController.GetName(upgradeIdx);
    }

    void OnCollision2D(Collision2D collision)
    {
        //플레이어와 닿아있는 상태에서 윗방향키 누르면 업그레이드 구매 시도
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.UpArrow))
        {
            DataController.LobbyUpgrade(upgradeIdx);
        }
    }
}
