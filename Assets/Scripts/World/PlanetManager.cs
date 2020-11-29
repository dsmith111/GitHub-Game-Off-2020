using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [Tooltip("Increases each time the planet is landed on.")] public int landingCounter;
    [Tooltip("Total amount of Fuel the planet can offer.")] public float fuelQuantity;

    private ShipLanding shipLanding;
    private ShipController shipController;

    private void Start()
    {
        shipLanding = FindObjectOfType<ShipLanding>();
        shipController = FindObjectOfType<ShipController>();
    }

    private void FixedUpdate()
    {
        if (shipLanding.isLanded && fuelQuantity != 0)
        {
            fuelQuantity--;
            shipController.remainingFuel += 50f;
            if (fuelQuantity <= 0)
            {
                fuelQuantity = 0;
                return;
            }

            if(shipController.remainingFuel >= shipController.maxFuel)
            {
                shipController.remainingFuel = shipController.maxFuel;
            }
        }

        if (shipLanding.isLanded || shipLanding.landedOnFinalPlanet)
        {
            landingCounter++;
        }
    }
}
