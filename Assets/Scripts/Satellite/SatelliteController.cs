using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    [HideInInspector] public ShipController shipController;
    [HideInInspector] public DockingRangeChecker dockingRangeChecker;
    [HideInInspector] private GameObject[] trajectoryDots;
    [HideInInspector] private GravityIndicator gravityIndicator;

    private ScoreTracker scoreTracker;
    //[SerializeField] int scoreForDocking = 500;

    private Rigidbody2D satelliteRb;
    private Vector2 velocity = Vector2.zero;
    private Vector2 previousShipPos;
    private bool inSatelliteAtmos;
    [HideInInspector] public bool isDocked;
    [Tooltip("How fast the ship will slow to match satellite speed")][SerializeField] float decelerationTime = 2f;

    private bool undocking;
    private bool refuelComplete;
    [SerializeField] GameObject atmosphereHor;
    [SerializeField] GameObject atmosphereVert;

    void Start()
    {
        satelliteRb = GetComponent<Rigidbody2D>();
        dockingRangeChecker = gameObject.GetComponentInChildren<DockingRangeChecker>();
        shipController = GameObject.FindObjectOfType<ShipController>();
        gravityIndicator = GameObject.FindObjectOfType<GravityIndicator>();
        scoreTracker = GameObject.FindObjectOfType<ScoreTracker>();
        inSatelliteAtmos = false;
        isDocked = false;
        refuelComplete = false;
    }

    void FixedUpdate()
    {
        InSatelliteRange();

        if (refuelComplete || undocking)
        {
            SatelliteStatusChange();
        }

        if (isDocked)
        {
            gravityIndicator.enabled = false;
        }
        else
        {
            gravityIndicator.enabled = true;
        }

        if(dockingRangeChecker.isInDockingRange && !refuelComplete)
        {
            scoreTracker.currentScore++;
        }
    }

    #region Triggers for Entering satellite Atmosphere
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inSatelliteAtmos = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inSatelliteAtmos = false;
        }
    }
    #endregion

    #region Called Functions
    void Decelerate()
    {
        shipController.shipRb.velocity = Vector2.SmoothDamp(shipController.shipRb.velocity, satelliteRb.velocity, ref velocity, decelerationTime);
    }

    void DockingSpeedCheck()
    {
        float magnitudeRange = 30f;
        float relativeMagnitude = shipController.shipRb.velocity.magnitude - satelliteRb.velocity.magnitude;

        if (relativeMagnitude <= magnitudeRange)
        {
            isDocked = true;
        }
    }

    void LockPosition()
    {
        shipController.transform.position = new Vector2(satelliteRb.position.x, satelliteRb.position.y);
    }

    void Refuel()
    {
        shipController.remainingFuel += 50f;

        if (shipController.remainingFuel >= shipController.maxFuel)
        {
            shipController.remainingFuel = shipController.maxFuel;
            refuelComplete = true;
        }
    }

    void SatelliteStatusChange()
    {
        //blue: 89D3D6
        //yellow: FFFC84 alpha 164

        atmosphereHor.GetComponent<SpriteRenderer>().color = new Color(0.7843137f, 0.3921569f, 0.3921569f, 255);
        atmosphereVert.GetComponent<SpriteRenderer>().color = new Color(0.7843137f, 0.3921569f, 0.3921569f, 255);
    }
    #endregion

    void InSatelliteRange()
    {
        if (inSatelliteAtmos && !undocking)
        {
            Decelerate();

            if (dockingRangeChecker.isInDockingRange && shipController.throttle < 1f)
            {
                DockingSpeedCheck();
                if (isDocked)
                {
                    LockPosition();
                    Refuel();
                }
            }else if(shipController.throttle >= 1f)
            {
                undocking = true;
            }
        }
        else if((inSatelliteAtmos && undocking) || refuelComplete)
        {
            gameObject.GetComponent<GravitationalBody>().enabled = false;
        }
    }
}