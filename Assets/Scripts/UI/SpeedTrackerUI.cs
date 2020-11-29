using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedTrackerUI : MonoBehaviour
{
    public TextMeshProUGUI speedTMPro;
    public ShipController shipController;

    public string text = "Km/s: ";
    public float speed;
    private double round;

    // Start is called before the first frame update
    void Start()
    {
        speedTMPro = GetComponent<TextMeshProUGUI>();
        speed = shipController.currentSpeed;
        speedTMPro.text = text + speed;
    }

    // Update is called once per frame
    void Update()
    {
        speed = shipController.currentSpeed;
        round = System.Math.Round(speed, 2);
        speedTMPro.text = text + round;
    }
}
