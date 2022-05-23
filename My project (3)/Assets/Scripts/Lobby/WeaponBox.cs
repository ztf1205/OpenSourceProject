using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    public GameObject weapon;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(weapon);
        Destroy(this.gameObject);
    }
}
