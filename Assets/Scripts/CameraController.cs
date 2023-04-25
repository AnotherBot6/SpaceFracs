using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offsetpos;
    public float moveSpeed = 5;
    public float turnSpeed = 10;
    public float smoothSpeed = 0.5f;
    private Quaternion rotation;
    private Vector3 targetPos;
    bool smoothRotating = false;

    // Update is called once per frame
    void Update()
    {
        MoveWithTarget();
        LookTarget();
        if (Input.GetKeyDown(KeyCode.G) && !smoothRotating)
        {
            StartCoroutine("RotateAroundTarget", 45);
        }
        if (Input.GetKeyDown(KeyCode.H) && !smoothRotating)
        {
            StartCoroutine("RotateAroundTarget", -45);
        }
    }

    void MoveWithTarget()
    {
        targetPos = target.position + offsetpos;
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
    void LookTarget()
    {
        rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }

    IEnumerator RotateAroundTarget(float angle)
    {
        Vector3 vel = Vector3.zero;
        Vector3 targetoffsetPos = Quaternion.Euler(0, angle, 0) * offsetpos;
        float dist = Vector3.Distance(offsetpos, targetoffsetPos);
        while(dist > 0.02f)
        {
            offsetpos = Vector3.SmoothDamp(offsetpos, targetoffsetPos, ref vel,smoothSpeed);
            dist = Vector3.Distance(offsetpos, targetoffsetPos);
            yield return null;
        }
        smoothRotating = false;
        offsetpos = targetoffsetPos;
    }
}
