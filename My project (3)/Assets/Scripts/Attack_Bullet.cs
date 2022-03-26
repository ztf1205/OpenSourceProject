using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Bullet : MonoBehaviour
{
    float speed = 10f;
    int damage = 50;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyMove enemyMove = collision.GetComponent<EnemyMove>();
            enemyMove.OnDamaged();
        }

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);

        }

    }

}
