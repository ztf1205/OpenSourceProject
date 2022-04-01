using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    [SerializeField]
    private float speedUp = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�÷��̾�� �浹�ߴٸ� �÷��̾� �ִ� �̼� ���� �� ������ ����
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerMove>().maxSpeed += speedUp;
            Destroy(this.gameObject);
        }

    }

}
