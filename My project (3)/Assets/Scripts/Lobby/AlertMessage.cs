using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AlertMessage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI alertText;
    void Start()
    {
        alertText.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            alertText.gameObject.SetActive(true);
        }   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            alertText.gameObject.SetActive(false);
        }
    }
}
