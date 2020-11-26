using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    #region vars

    [HideInInspector] public ThrottleUI throttleUI;
    [HideInInspector] public SatelliteController satelliteController;

    [HideInInspector] public Rigidbody2D shipRb;
    [HideInInspector] public Vector2 velocity = Vector2.zero;
    private Vector2 lastPosition = Vector2.zero;

    //Thrust
    [SerializeField] float throttle;
    private float maxThrottle = 100;
    [Tooltip("Throttle Input Multiplier")] [SerializeField] float throttleCoefficient = .25f;
    private float throttleMovementMultiplier = 1.5f; //For holding shift while adjusting throttle to move it faster.
    [Space(10)]

    //Rotation
    [SerializeField] float horizontalInputAcceleration = 2;
    private float zRotationVelocity;
    [Space(10)]

    public float maxFuel;
    public float remainingFuel;
    [Space(10)]

    [HideInInspector] public float currentSpeed;

    #endregion

    private void Start()
    {
        shipRb = GetComponent<Rigidbody2D>();
        throttleUI.SetMaxValue(maxThrottle);
        remainingFuel = maxFuel;
    }

    private void Update()
    {
        currentSpeed = shipRb.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        float inputY = Input.GetAxis("Vertical");
        float inputX = Input.GetAxis("Horizontal");

        #region throttle
        //Throttle control
        throttle += inputY; //throttle value moves with vertical input
        throttleUI.SetThrottle(throttle); //adjusts UI to match
        if(throttle >= maxThrottle) //limits max throttle
        {
            throttle = maxThrottle;
        }
        if(throttle <= 0) //limits min throttle
        {
            throttle = 0;
        }
        if (Input.GetKey(KeyCode.LeftShift)) //moves throttle value faster
        {
            throttle += inputY * throttleMovementMultiplier;
        }

        //Forward force
        shipRb.AddForce(transform.up * throttle * throttleCoefficient);
        #endregion

        #region rotation
        //Rotation
        //float zTurnAcceleration = -1 * inputX * horizontalInputAcceleration;
        transform.Rotate(0, 0, -inputX * horizontalInputAcceleration); 
        #endregion

        #region extra shit
        //Cut Throttle when fuel is empty
        if (throttle > 0 && remainingFuel > 0)
        {
            remainingFuel -= 1;
        }
        if(remainingFuel <= 0)
        {
            throttle = 0;
        }
        #endregion
    }

    //Throttle UI
    public void ThrottleSlider(float newThrottle) {throttle = newThrottle;}
}
