using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Example : MonoBehaviour
{
    [SerializeField]
    MenuCreator myMenu;

    [SerializeField] private GameObject dummyCanvas;

    void Start()
    {
        dummyCanvas.SetActive(true);
        myMenu.ShowMenu();
        dummyCanvas.SetActive(!dummyCanvas.activeSelf);
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            //myMenu.ShowMenu();
            //dummyCanvas.SetActive(!dummyCanvas.activeSelf);

            //ResetAndHideMenuWithBackButton();

            SceneManager.LoadScene("LevelSelector");
            Debug.Log("Esc pressed...");
        }
    }
}
