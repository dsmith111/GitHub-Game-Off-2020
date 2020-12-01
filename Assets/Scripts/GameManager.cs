using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private ScoreTracker scoreTracker;
    private DistanceTracker distanceTracker;



    public int totalScore;
    public float totalDistanceTraveled;
    public float totalRefuelTracker;
    public float totalFuelSpentTracker;
    public float fastestSpeedMet;
    public int totalLandings;

    private void Start()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
        distanceTracker = FindObjectOfType<DistanceTracker>();
    }

    private void Update()
    {
        totalScore = scoreTracker.currentScore;
        totalDistanceTraveled = distanceTracker.distanceTraveled;
        //total fuel tracker - data sent from ship controller
        //fastest speed met - data sent from ship controller
        //total landing - data sent from ShipLanding
    }


    //final grade calculator
}
