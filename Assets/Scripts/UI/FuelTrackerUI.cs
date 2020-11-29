using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelTrackerUI : MonoBehaviour
{
    public TextMeshProUGUI fuelTMPro;
    public ShipController shipController;

    public string text = "Fuel: ";
    public float fuel;
    public float baseFuel;

    private double round;

    // Start is called before the first frame update
    void Start()
    {
        fuelTMPro = GetComponent<TextMeshProUGUI>();
        baseFuel = shipController.maxFuel;
        fuel = shipController.remainingFuel;
        //fuel = (fuel * 100)/baseFuel; //percentage
        fuelTMPro.text = text + fuel + " / " + baseFuel;
    }

    // Update is called once per frame
    void Update()
    {
        baseFuel = shipController.maxFuel;
        //fuel = (shipController.remainingFuel * 100)/baseFuel;
        fuel = shipController.remainingFuel;
        round = System.Math.Round(fuel, 2);
        fuelTMPro.text = text + round + " / " + baseFuel;
    }
}
