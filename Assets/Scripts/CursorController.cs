using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;
    public static Texture2D shooter;

    public void Awake()
    {
        instance= this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState= CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void setShootCursor()
    {
        Cursor.SetCursor(shooter, new Vector2(shooter.width/2,shooter.height/2), CursorMode.ForceSoftware); 
    }

    public static void setDefaultCursor()
    {
        Cursor.SetCursor(null, Vector3.zero, CursorMode.ForceSoftware);
    }
}
