using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaEntreEscenas : MonoBehaviour
{

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("opciones");
        Debug.Log(objs.Length);
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
