using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    public float speed = 6;
    public float turnspeed = 10;
    private Vector2 input;
    private float angle;
    private Quaternion rotation;
    private Transform cam;
    //test
    

     
    void Start()
    {
        //cam = Camera.main.transform;  
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;
        GetDirection();
        Rotate();
        Move();
    } 
    void GetDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y; 
    }

    void Rotate()
    {
        rotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,turnspeed*Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
