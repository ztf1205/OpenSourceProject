using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FloatingText : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed; // 투명도 변환속도
    public float destroyTime;
    TextMeshPro text;
    Color alpha;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        Invoke("DestroyObject", destroyTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
