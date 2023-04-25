using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PausaB : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;

    [SerializeField] private GameObject botonControles;

    [SerializeField] private GameObject menuPausa;

    [SerializeField] private GameObject menuControles;

    public static bool juegoPausado = false;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado) { Reanudar(); }
            else { Pausa(); }
        }
    }
    public void Pausa() {
        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        CursorController.setDefaultCursor();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Controles()
    {
        juegoPausado = true;
        Time.timeScale = 0f;
        botonControles.SetActive(false);
        menuControles.SetActive(true);
        CursorController.setDefaultCursor();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Reanudar()
    {
        Time.timeScale = 1f;
        juegoPausado = false;
        botonPausa.SetActive(true);
        botonControles.SetActive(true);
        menuPausa.SetActive(false);
        menuControles.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        



    }
    public void Settings() { 
        
    
    }
    public void Quit()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(GameObject.FindWithTag("opciones"));
        SceneManager.LoadScene(1);



    }


}
