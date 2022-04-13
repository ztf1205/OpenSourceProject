using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParallax : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public float speed;

    private void Update()
    {
        Vector2 nextVec = new Vector2();

        if (Input.GetKey(KeyCode.A))
        {
            nextVec.x--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            nextVec.x++;
        }

        Vector2 p = Rigidbody.velocity;
        p.x = nextVec.x * speed;
        Rigidbody.velocity = p;
    }
}
