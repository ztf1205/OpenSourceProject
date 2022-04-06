using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUp : MonoBehaviour
{
    [SerializeField]
    private int hpUp = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //�÷��̾�� �浹�ߴٸ� �÷��̾� �ִ� �̼� ���� �� ������ ����
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerMove>().health += hpUp;
            Destroy(this.gameObject);
        }

    }

}
