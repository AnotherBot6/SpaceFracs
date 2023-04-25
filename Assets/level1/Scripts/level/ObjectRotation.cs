using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rspeed,translateSpeed;
    public Transform center;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 45 * Time.deltaTime * rspeed);
        transform.RotateAround(center.position, Vector3.up, translateSpeed * Time.deltaTime);



    }
}
