using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    private ShipController shipController;
    private DistanceTracker distanceTracker;

    public int currentScore;

    private float waitTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        shipController = GameObject.FindObjectOfType<ShipController>();
        distanceTracker = GameObject.FindObjectOfType<DistanceTracker>();

        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        waitTime = waitTime - Time.deltaTime;
        if (waitTime <= 0)
        {
            currentScore++;
            waitTime = 5f;
        }
    }
}
