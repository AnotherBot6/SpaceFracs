using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaEsceanas : MonoBehaviour
{

    [SerializeField] private GameObject Lose;

    [SerializeField] private GameObject Win;

    public int NumEscenaSig;

    // Start is called before the first frame update
    public void Restart()
    {
        Lose.SetActive(true);
        if (Cursor.visible == false)
        {
            CursorController.setDefaultCursor();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene(NumEscenaSig);
        }

    }

    public void Siguiente()
    {
        
        Win.SetActive(true);
        if (Cursor.visible == false)
        {
            CursorController.setDefaultCursor();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene(NumEscenaSig);
        }

       
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
