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
    private GameManager gameManager;
    private WinScreenUI winScreen;

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
        scoreTracker = FindObjectOfType<ScoreTracker>();
        planetManager = FindObjectOfType<PlanetManager>();
        gameManager = FindObjectOfType<GameManager>();
        winScreen = FindObjectOfType<WinScreenUI>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        previousVelocityMagnitude = shipController.shipRb.velocity.magnitude;
        double previousVelocityMagnitude_Rounded = System.Math.Round(previousVelocityMagnitude, 2);

        isLanded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, planetLayer);

        if (finalObject == null) { finalObject = GameObject.FindGameObjectWithTag("final_object"); }
        landedOnFinalPlanet = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, finalObjectLayer);

        if (isLanded)
        {
            PlanetManager[] planets = FindObjectsOfType<PlanetManager>();
            planetManager = NearestBody(planets, new Vector2(transform.position.x, transform.position.y));
            if (planetManager.landingCounter == 1 && !planetManager.givenScore)
            {
                planetManager.givenScore = true;
                int scoreToAdd = LandScoring(previousVelocityMagnitude_Rounded);
                Debug.Log("Score to add: " + scoreToAdd);
                scoreTracker.currentScore += scoreToAdd;
                gameManager.totalLandings++;
            }
        }

        if (landedOnFinalPlanet)
        {
            //int finalPlanetLandingCounter = finalObject.GetComponent<PlanetManager>().landingCounter;
            if (finalObject.GetComponent<PlanetManager>().landingCounter == 1)
            {
                int scoreToAdd = LandScoring(previousVelocityMagnitude_Rounded);
                Debug.Log("Score to add: " + scoreToAdd);
                scoreTracker.currentScore += scoreToAdd;
                gameManager.totalLandings++;
                winScreen.WinScreen();
            } 
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
    PlanetManager NearestBody(PlanetManager[] planets, Vector2 position)
    {
        PlanetManager bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = position;
        foreach (PlanetManager potentialTarget in planets)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}