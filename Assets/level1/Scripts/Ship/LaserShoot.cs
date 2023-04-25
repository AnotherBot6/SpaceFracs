using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPref;
    public Transform laserOrigin;
    // Start is called before the first frame update
    void Start()
    {
        laserPref.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            laserPref.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            laserPref.SetActive(false);
        }
    }
}
 