using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceTrackerUI : MonoBehaviour
{
    public TextMeshProUGUI distanceTMPro;
    public DistanceTracker distanceTracker;

    public string text = "Km Traveled: ";
    public float distanceVal = 0;

    private double round;

    void Start()
    {
        distanceTMPro = GetComponent<TextMeshProUGUI>();
        distanceVal = distanceTracker.distanceTraveled;
        distanceTMPro.text = text + distanceVal;
    }

    void Update()
    {
        distanceVal = distanceTracker.distanceTraveled;
        round = System.Math.Round(distanceVal, 2);
        distanceTMPro.text = text + round;
    }
}
