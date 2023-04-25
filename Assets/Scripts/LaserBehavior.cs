using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public LineRenderer laserLine;
    private Vector3 screenPos, worldPos;
    private GameObject planetToMove;
    private bool canMovePlanet;
    // Start is called before the first frame update
    void Start()
    {
        //laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        screenPos = Input.mousePosition;
        screenPos.z = 15;
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.y = 0;
        Vector3 dir = (worldPos - transform.position).normalized;
        Debug.DrawRay(transform.position, worldPos, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            laserLine.SetPosition(0, transform.position);
            laserLine.gameObject.SetActive(true);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, worldPos, out hit, 50))
            {
                laserLine.SetPosition(1, hit.point);
                if (hit.collider.tag == "Section1" || hit.collider.tag == "Section2")
                {
                    planetToMove = hit.transform.gameObject;
                    canMovePlanet = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            laserLine.gameObject.SetActive(false);
            canMovePlanet = false;
        }

        if (canMovePlanet)
            planetToMove.transform.position = worldPos;
    }
}
