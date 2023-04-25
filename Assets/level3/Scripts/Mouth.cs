using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouth : MonoBehaviour
{
    public static int errorCount = 0;
    public GameObject frac1UI, frac2UI;
    public static bool firstObjectDestroyed;
    public GameObject checkUI;
    public static Fraction currFrac1, currFrac2;
    // Start is called before the first frame update
    void Start()
    {
        checkUI.SetActive(false);
        firstObjectDestroyed= false;
        currFrac1= new Fraction();
        currFrac2= new Fraction();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Mouth.currFrac1 + Mouth.currFrac2).Equals(MercuryLevel.currResult))
        {
            Debug.Log("Correct");
            checkUI.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("GreenCheck");
        }
        else
        {
            Debug.Log("Incorrect");
            checkUI.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("RedCross");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object detected");
        other.gameObject.SetActive(false);
        string name = other.name;
        string text = MercuryLevel.possibleResults[int.Parse(name[name.Length - 1].ToString())].numerator.ToString();
        if (!firstObjectDestroyed)
        {
            frac1UI.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(MercuryLevel.possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].numerator.ToString());
            frac1UI.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(MercuryLevel.possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].denominator.ToString());
            currFrac1.numerator = MercuryLevel.possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].numerator;
            currFrac1.denominator = MercuryLevel.possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].denominator;
            firstObjectDestroyed = true;
            Debug.Log(currFrac1.ToString());
        }
        else
        {
            frac2UI.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(MercuryLevel.possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].numerator.ToString());
            frac2UI.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(MercuryLevel.possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].denominator.ToString());
            currFrac2.numerator = MercuryLevel.possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].numerator;
            currFrac2.denominator = MercuryLevel.possibleResults[int.Parse(name[name.Length - 1].ToString()) - 1].denominator;
            firstObjectDestroyed = false;
            checkUI.gameObject.SetActive(true);
            Debug.Log(currFrac2.ToString());
        }
    }
}
