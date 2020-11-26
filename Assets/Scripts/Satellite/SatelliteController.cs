using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    [HideInInspector] public ShipController shipController;
    [HideInInspector] public DockingRangeChecker dockingRangeChecker;
    private Rigidbody2D satelliteRb;
    private Vector2 satellitePos;
    private Vector2 velocity = Vector2.zero;
    private Vector2 previousShipPos;
    private bool inSatelliteAtmos;
    private bool isDocked;
    [Tooltip("How fast the ship will slow to match satellite speed")][SerializeField] float decelerationTime = 2f;

    void Start()
    {
        satelliteRb = GetComponent<Rigidbody2D>();
        satellitePos = satelliteRb.position;
        dockingRangeChecker = gameObject.GetComponentInChildren<DockingRangeChecker>();
        inSatelliteAtmos = false;
        shipController = GameObject.FindObjectOfType<ShipController>();
        isDocked = false;
    }

    void FixedUpdate()
    {
        if (inSatelliteAtmos)
        {
            InSatelliteRange();
            
        }
        else { return; }
    }

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

    void InSatelliteRange()
    {
        if (!inSatelliteAtmos) { return; }
        else
        {
            //matches ships speed to satellite
            shipController.shipRb.velocity = Vector2.SmoothDamp(shipController.shipRb.velocity, satelliteRb.velocity, ref velocity, decelerationTime);

            //speed check
            float magnitudeRange = 40f; //ship speed must be within this speed range of the satellite
            float relativeMagnitude = shipController.shipRb.velocity.magnitude - satelliteRb.velocity.magnitude;

            if (relativeMagnitude <= magnitudeRange && dockingRangeChecker.isInDockingRange == true)
            {
                shipController.transform.position = new Vector2(satelliteRb.position.x, satelliteRb.position.y);
                isDocked = true;

                //refuel ship
                shipController.remainingFuel += 1f;
                if (shipController.remainingFuel >= shipController.maxFuel)
                {
                    shipController.remainingFuel = shipController.maxFuel;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {

                
            }
        }
    }
}