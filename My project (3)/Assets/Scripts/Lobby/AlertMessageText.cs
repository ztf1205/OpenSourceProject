using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Text ver.
public class AlertMessageText : MonoBehaviour
{
    [SerializeField] Text alertText;
    // Start is called before the first frame update
    void Start()
    {
        alertText.gameObject.SetActive(false);
    }

    // Update is called once per frame
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
