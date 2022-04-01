using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Point
            bool isBronze = gameObject.name.Contains("Bronze");
            bool isSilver = gameObject.name.Contains("Sliver");
            bool isGold = gameObject.name.Contains("Gold");

            if (isBronze)
            {
                gameManager.stagePoint += 50;
            }
            else if (isSilver)
            {
                gameManager.stagePoint += 100;
            }
            else if (isGold)
            {
                gameManager.stagePoint += 300;
            }

            //Deactive Item
            gameObject.SetActive(false);
        }

    }

}
