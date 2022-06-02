using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    public GameObject weapon;
    public void OnTriggerEnter2D(Collider2D collision) // 충돌 시 weapon prefab 생성 후 gameObject 제거
    {
        Instantiate(weapon);
        Destroy(this.gameObject);
    }
}
