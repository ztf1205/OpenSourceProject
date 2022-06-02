using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FloatingText : MonoBehaviour
{
    public float moveSpeed; // text 상승속도
    public float alphaSpeed; // 투명도 변환속도
    public float destroyTime; // object 제거속도
    TextMeshPro text;
    Color alpha;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        Invoke("DestroyObject", destroyTime);
    }

    void Update() // deltaTime * moveSpeed 만큼 매 frame 갱신하여 위로(y) 떠오르는 효과적용
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    private void DestroyObject() // 제거
    {
        Destroy(gameObject);
    }
}
