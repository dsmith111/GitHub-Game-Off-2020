using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfFuelUI : MonoBehaviour
{
    private ShipController shipController;
    [SerializeField] GameObject outOfFuelMessage;

    // Start is called before the first frame update
    void Start()
    {
        outOfFuelMessage = GameObject.Find("Out Of Fuel");
        shipController = GameObject.FindObjectOfType<ShipController>();
        outOfFuelMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(shipController.remainingFuel <= 0)
        {
            outOfFuelMessage.SetActive(true);
        }
        else
        {
            outOfFuelMessage.SetActive(false);
        }
    }
}
