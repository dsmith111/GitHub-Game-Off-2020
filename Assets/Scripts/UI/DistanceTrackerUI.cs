using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceTrackerUI : MonoBehaviour
{
    public TextMeshProUGUI distanceTMPro;
    public DistanceTracker distanceTracker;

    public string text = "Meters Traveled: ";
    public float distanceVal = 0;

    void Start()
    {
        distanceTMPro = GetComponent<TextMeshProUGUI>();
        distanceVal = distanceTracker.distanceTraveled;
        distanceTMPro.text = text + distanceVal;
    }

    void Update()
    {
        distanceVal = distanceTracker.distanceTraveled;
        distanceTMPro.text = text + distanceVal;
    }
}
