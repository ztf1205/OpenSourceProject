using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject player;
    public float speed = -1f;//음수는 즉시 이동, 0은 정지

    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameIsPaused == false && GameManager.gameIsOver == false)
        {
            if (speed < 0)
            {
                transform.position = player.transform.position;
            }
            else if (speed > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
            }

        }
    }
}
