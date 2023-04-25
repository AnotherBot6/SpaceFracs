using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
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
        lines = new string[] { "Bienvenido a este nuevo nivel. (Presiona L para continuar)",
            "El planeta Knowhere necesita que destruyas los asteroides que bloquean su orbita. (Click izquierdo)",
            "Puedes moverte con: a, w, s, d, z, space, q, e.",
            "Para esta primera misión debes destruir: " + showText.num1 + " " + showText.operation + " " + showText.num2 + " asteroides ",   // Operación 
            "Dirígete al centro espacial al terminar la misión."};
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
