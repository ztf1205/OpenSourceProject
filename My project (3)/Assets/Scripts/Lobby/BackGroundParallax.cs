using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParallax : MonoBehaviour
{
    public PlayerParallax unit;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }
    // LateUpdate => Update중 마지막으로 호출
    private void LateUpdate() // background를 gameObject들의 속도와 다르게 움직여 원근감 효과 적용
    {
        Vector3 newPos = startPos + ((unit.transform.position - startPos) * 0.9f);
        newPos.z = startPos.z;
        transform.position = newPos;
    }
}
