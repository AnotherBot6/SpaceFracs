using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private Vector3 screenPos, worldPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        screenPos = Input.mousePosition;
        screenPos.z = 15;
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.y = 0;
        Vector3 dir = (worldPos - transform.position).normalized;
        transform.forward = dir;
    }
}
