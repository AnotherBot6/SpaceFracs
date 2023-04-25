using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScriptEarth : MonoBehaviour
{
    public TextMeshProUGUI dialougueText;
    public string[] lines;
    public float textSpeed = 0.01f;
    int index;

    public RaycastGun showText; // Mostrar operacion

    // Start is called before the first frame update
    void Start()
    {
        showText = FindObjectOfType<RaycastGun>();
        lines = new string[] { "Bienvenido a este nuevo nivel. (Preciona L para continuar)",
            "Para este nivel tienes una barra arriba que indica los objetos que debes arrastrar al portal,\" en orden de izquierda a derecha.",
            "Para agarrar objetos puedes disparar un rayo y mantenerlo con (Click izquierdo).",
            "Par moverte, apreta w para avanzar y s para frenar, con el mouse puedes ir de izquierda a derecha.\" Manten espacio para activar un pequeño turbo.", 
            "Tienes un limite del tiempo para coompletar todos los objetos."};
        dialougueText.text = string.Empty;
        startDialougue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (dialougueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialougueText.text = lines[index];
            }
        }
    }

    public void startDialougue()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialougueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialougueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
