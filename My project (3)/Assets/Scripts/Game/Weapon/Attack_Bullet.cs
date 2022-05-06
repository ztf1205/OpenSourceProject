using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float bulletDamage = 100f;

    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.GetComponent<EnemyMove>().OnDamaged(bulletDamage + bulletDamage * DataController.GetGameValue(Constants.DAMAGE_IDX));
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);

        }

    }

}
