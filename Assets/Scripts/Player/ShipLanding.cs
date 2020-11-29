using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLanding : MonoBehaviour
{
    [SerializeField] public Transform groundCheck;
    public LayerMask planetLayer;
    public LayerMask finalObjectLayer;
    public bool isLanded = false;
    public bool landedOnFinalPlanet = false;
    public float groundCheckRadius = .03f;
    private GameObject finalObject;
    private ScoreTracker scoreTracker;
    private PlanetManager planetManager;

    private ShipController shipController;
    private float previousVelocityMagnitude;

    [Space(10)]
    [SerializeField] double smoothLandingSpeedLimit = 3f;
    [SerializeField] double moderateLandingSpeedLimit = 7f;
    [SerializeField] double roughLandingSpeedLimit = 14f;


    // Start is called before the first frame update
    void Start()
    {
        shipController = FindObjectOfType<ShipController>();
        finalObject = GameObject.FindGameObjectWithTag("final_object");
        scoreTracker = GameObject.FindObjectOfType<ScoreTracker>();
        planetManager = GameObject.FindObjectOfType<PlanetManager>();
    }



    // Update is called once per frame
    void Update()
    {
        previousVelocityMagnitude = shipController.shipRb.velocity.magnitude;
        double previousVelocityMagnitude_Rounded = System.Math.Round(previousVelocityMagnitude, 2);

        isLanded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, planetLayer);

        if (finalObject == null) { finalObject = GameObject.FindGameObjectWithTag("final_object"); }
        landedOnFinalPlanet = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, finalObjectLayer);

        if (isLanded)
        {
            if (planetManager.landingCounter == 1)
            {
                int scoreToAdd = LandScoring(previousVelocityMagnitude_Rounded);
                Debug.Log("Score to add: " + scoreToAdd);
                scoreTracker.currentScore += scoreToAdd;
            }
        }

        if (landedOnFinalPlanet)
        {
            if(planetManager.landingCounter == 1)
            {
                int scoreToAdd = LandScoring(previousVelocityMagnitude_Rounded);
                Debug.Log("Score to add: " + scoreToAdd);
                scoreTracker.currentScore += scoreToAdd;
            }
            //win screen
            Debug.Log("you win");
        }
        else
        {
            return;
        }
    }

    int LandScoring(double landingMagnitude)
    {
        Debug.Log("Landing Magnitude is: " + landingMagnitude);

        if(landingMagnitude <= smoothLandingSpeedLimit)
        {
            Debug.Log("Smooth Landing");
            return 1000;
        }else if(landingMagnitude <= moderateLandingSpeedLimit)
        {
            Debug.Log("Moderate Landing");
            return 750;
        }else if(landingMagnitude <= roughLandingSpeedLimit)
        {
            Debug.Log("Rough Landing");
            return 500;
        }

        return 0;
    }
}