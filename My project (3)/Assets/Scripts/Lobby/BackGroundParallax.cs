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

    private void LateUpdate()
    {
        Vector3 newPos = startPos + ((unit.transform.position - startPos) * 0.9f);
        newPos.z = startPos.z;
        transform.position = newPos;
    }
}
