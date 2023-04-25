using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScriptH : MonoBehaviour
{
    public TextMeshProUGUI dialougueText;
    public string[] lines;
    public float textSpeed = 0.01f;
    int index;

    public RaycastGunH showText; // Mostrar operacion


    // Start is called before the first frame update
    void Start()
    {
        showText = FindObjectOfType<RaycastGunH>();
        lines = new string[] { 
            "El planeta Knowhere requiere tu ayuda una última vez destruyendo: " 
            + showText.num1 + " " + showText.operation1 + " " + showText.num2 + " " + showText.operation2 + " " + showText.num3 + " asteroides ",   // Operación 
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