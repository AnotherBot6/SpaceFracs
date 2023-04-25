using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class MercuryLevel : MonoBehaviour
{
    public enum DifficultyLevel { Easy, Medium, Hard }

    public DifficultyLevel difficulty;
    public GameObject bubbleFrac;
    public GameObject frac1, frac2, frac3, frac4;
    public static GameObject frac1UI, frac2UI, OperatorUI;
    public static Fraction currResult;
    private List<GameObject> fracs = new List<GameObject>();
    private List<string> operators = new List<string>();
    public static List<Fraction> possibleResults = new List<Fraction>();
    public static int operationsComplete = 0;
    // Start is called before the first frame update
    void Start()
    {
        currResult = new Fraction();
        fracs.Add(frac1);
        fracs.Add(frac2);
        fracs.Add(frac3);
        if (difficulty == DifficultyLevel.Hard)
        {
            fracs.Add(frac4);
            operators.Add("/");
            operators.Add("-");
            operators.Add("+");
            operators.Add("*");
            operators.Add("-");
        }
        else if (difficulty == DifficultyLevel.Easy)
        {
            operators.Add("*");
            operators.Add("+");
            operators.Add("*");
            operators.Add("+");
        }
        else
        {
            operators.Add("+");
            operators.Add("-");
            operators.Add("*");
            operators.Add("/");
        }
        fracs.Add(bubbleFrac);
        operationsComplete = 0;
        foreach (Transform child in bubbleFrac.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in frac1.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in frac2.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in frac3.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in frac4.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        bool operationComplete = false;
        //if(operationsComplete > 3)
        //{
        if (operationsComplete == 0)
        {
            possibleResults = GetRandomFractions("+");
            currResult = possibleResults[possibleResults.Count - 1];
            Debug.Log(possibleResults[0].ToString() + "   " + possibleResults[1].ToString() + "  " + possibleResults[2].ToString() + "  " + possibleResults[3].ToString());
            int index = 0;
            foreach (GameObject frac in fracs)
            {

                if (possibleResults[index].numerator.ToString().Length > 1)
                {
                    frac.transform.GetChild(0).gameObject.SetActive(false);
                    frac.transform.GetChild(1).gameObject.SetActive(true);
                    frac.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(possibleResults[index].numerator.ToString()[0].ToString());
                    frac.transform.GetChild(1).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(possibleResults[index].numerator.ToString()[1].ToString());
                }
                else
                {
                    frac.transform.GetChild(0).gameObject.SetActive(true);
                    frac.transform.GetChild(1).gameObject.SetActive(false);
                    frac.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(possibleResults[index].numerator.ToString());
                }
                if (possibleResults[index].denominator.ToString().Length > 1)
                {
                    frac.transform.GetChild(2).gameObject.SetActive(false);
                    frac.transform.GetChild(3).gameObject.SetActive(true);
                    frac.transform.GetChild(3).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(possibleResults[index].denominator.ToString()[0].ToString());
                    frac.transform.GetChild(3).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(possibleResults[index].denominator.ToString()[1].ToString());
                }
                else
                {
                    frac.transform.GetChild(2).gameObject.SetActive(true);
                    frac.transform.GetChild(3).gameObject.SetActive(false);
                    frac.transform.GetChild(2).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(possibleResults[index].denominator.ToString());
                }
                index++;
            }

            operationsComplete++;
        }


        //}
    }
    private Fraction GetRandomFraction()
    {
        int numerator = Random.Range(1, 10);
        int denominator = Random.Range(2, 10);
        while (denominator == numerator)
            denominator = Random.Range(2, 10);
        return new Fraction(numerator, denominator);
    }

    private List<Fraction> GetRandomFractions(string op)
    {
        List<Fraction> fractions = new List<Fraction>();
        Fraction frac1 = GetRandomFraction();
        Fraction frac2 = GetRandomFraction();
        Fraction frac3 = GetRandomFraction();
        Fraction fracResult = new Fraction(1, 1);
        if (op == "+")
        {
            fracResult = frac1 + frac2;
        }
        else if (op == "-")
        {
            fracResult = frac1 - frac2;
        }
        else if (op == "*")
        {
            fracResult = frac1 * frac2;
        }
        else if (op == "/")
        {
            fracResult = frac1 / frac2;
        }
        fracResult.Simplify();
        fractions.Add(frac1);
        fractions.Add(frac2);
        fractions.Add(frac3);
        if (difficulty == DifficultyLevel.Hard)
        {
            Fraction frac = GetRandomFraction();
            fractions.Add(frac);
            frac4.SetActive(true);
        }
        ShuffleList(fractions);
        fractions.Add(fracResult);
        return fractions;
    }


    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
public struct Fraction
{
    public int numerator;
    public int denominator;

    public Fraction(int n, int d)
    {
        numerator = n;
        denominator = d;
    }

    public override string ToString()
    {
        return numerator.ToString() + "/" + denominator.ToString();
    }

    public float ToFloat()
    {
        return (float)numerator / (float)denominator;
    }

    public bool Equals(Fraction other)
    {
        return this.ToFloat() == other.ToFloat();
    }

    // Sobrecarga del operador +
    public static Fraction operator +(Fraction f1, Fraction f2)
    {
        int newNumerator = f1.numerator * f2.denominator + f2.numerator * f1.denominator;
        int newDenominator = f1.denominator * f2.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    // Sobrecarga del operador -
    public static Fraction operator -(Fraction f1, Fraction f2)
    {
        int newNumerator = f1.numerator * f2.denominator - f2.numerator * f1.denominator;
        int newDenominator = f1.denominator * f2.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    // Sobrecarga del operador *
    public static Fraction operator *(Fraction f1, Fraction f2)
    {
        int newNumerator = f1.numerator * f2.numerator;
        int newDenominator = f1.denominator * f2.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    // Sobrecarga del operador /
    public static Fraction operator /(Fraction f1, Fraction f2)
    {
        int newNumerator = f1.numerator * f2.denominator;
        int newDenominator = f1.denominator * f2.numerator;
        return new Fraction(newNumerator, newDenominator);
    }

    //Simplificar fracciones
    public void Simplify()
    {
        int mcd = MCD(numerator, denominator);
        numerator /= mcd;
        denominator /= mcd;
    }

    private static int MCD(int a, int b)
    {
        // Algoritmo de Euclides para encontrar el MCD
        while (b != 0)
        {
            int t = b;
            b = a % b;
            a = t;
        }
        return a;
    }
}