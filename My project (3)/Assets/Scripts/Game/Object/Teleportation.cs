using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Player;
    bool Pressed;

    //�� ����Ű �Է� �� (���� ������ �ȵ�)
    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Pressed = true;
            Debug.Log("press");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //�浹�ڰ� �÷��̾��̸�
        if (collision.gameObject.tag == "Player")
        {
            //������Ű�� w�� �Է½� �����̵�
            //if (Pressed)
           // {
           //�ٸ� ��Ż ��ġ�� �����̵�
                  Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
           // }
            // StartCoroutine(Teleport());
        }
    }

    // �� �Լ��� �̿��ϸ� �����̵��� �����̸� �� �� ����
    //IEnumerator Teleport()
    // {
    //     yield return new WaitForSeconds(0);
    //     Player.transform.position = new Vector2(Portal.transform.position.x + 1, Portal.transform.position.y);
    // }
}
