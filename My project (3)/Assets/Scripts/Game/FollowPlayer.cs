using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

    }
}
