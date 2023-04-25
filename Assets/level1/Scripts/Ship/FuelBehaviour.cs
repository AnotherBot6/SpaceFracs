using FLFlight;
using FLFlight.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelBehaviour : MonoBehaviour
{
    public Image fuelBar;
    public TextMeshProUGUI fuelText;
    public float fuelConsuption = 0.05f;
    private float currentSpeed, fuelSubs;
    public static double currFuel;
    public static bool outOfFuel = false;
    // Start is called before the first frame update
    void Start()
    {
        outOfFuel = false;
        fuelSubs = 100;
        fuelText.text = "Power %";

    }

    // Update is called once per frame
    void Update()
    {
        if(PausaB.juegoPausado == false)
        {
            //Fuel bar : 380width - 100% power
            currentSpeed = Ship.PlayerShip.Velocity.magnitude;
            float fuelToConsume = (currentSpeed * fuelConsuption) / 195f;
            if (fuelToConsume > 0)
            {
                fuelBar.rectTransform.sizeDelta = new Vector2(fuelBar.rectTransform.sizeDelta.x - (fuelToConsume * 380f / 100f), fuelBar.rectTransform.sizeDelta.y);
                fuelSubs = (fuelBar.rectTransform.sizeDelta.x - (fuelToConsume * 380f / 100f)) * 100 / 380;
            }
            currFuel = Math.Round(fuelSubs, 0);
            fuelText.text = Math.Round(fuelSubs, 0).ToString() + " %";
            if (fuelBar.rectTransform.sizeDelta.x < 0)
            {
                outOfFuel = true;
                
                Debug.Log("Perdiste");
            }
           
        }
    }
}
