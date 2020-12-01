using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    #region vars

    public ThrottleUI throttleUI;
    public SatelliteController satelliteController;
    public Rigidbody2D shipRb;
    public GameManager gameManager;
    private ShipLanding shipLanding;

    //Thrust
    public AudioSource thrustAudio;
    //Preset
    [SerializeField]
    [Range(0, 1)]
    float maxVol = 1;
    [Header("Settings Volume")]
    [Range(0, 1)]
    public float maxThrustVol = 1;
    private VolumeManager volumeManager;

    [SerializeField]
    [Range(0f, 1)]
    float minVol = 0;
    public float throttle;
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
    private float previousFuel;

    [HideInInspector] public float currentSpeed;
    private float previousSpeed;

    #endregion

    private void Start()
    {
        shipRb = GetComponent<Rigidbody2D>();
        shipLanding = FindObjectOfType<ShipLanding>();
        satelliteController = FindObjectOfType<SatelliteController>();
        gameManager = FindObjectOfType<GameManager>();
        volumeManager = FindObjectOfType<VolumeManager>();
        throttleUI.SetMaxValue(maxThrottle);
        remainingFuel = maxFuel;
        previousFuel = remainingFuel;
    }

    private void Update()
    {   if(maxVol < minVol)
        {
            maxVol = 0;
            minVol = 0;
        }
        currentSpeed = shipRb.velocity.magnitude;

        //sends fastest speed traveled to gameManager
        if (gameManager.fastestSpeedMet < currentSpeed)
        {
            gameManager.fastestSpeedMet = currentSpeed;
        }

        //sends total refuel count to gameManager
        if(previousFuel < remainingFuel)
        {
            gameManager.totalRefuelTracker += 50f;
        }
        //sends total spent fuel to gameManager
        if(previousFuel > remainingFuel)
        {
            float fuelDifference = previousFuel - remainingFuel;
            gameManager.totalFuelSpentTracker += fuelDifference;
        }
        previousFuel = remainingFuel;
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

        if(throttle > 0)
        {
            if (!volumeManager)
            {
                volumeManager = FindObjectOfType<VolumeManager>();
            }
            if (maxThrustVol != volumeManager.thrustVol)
            {
                maxThrustVol = volumeManager.thrustVol;
            }
            if (!thrustAudio.isPlaying)
            {
                thrustAudio.Play();
            }
            float curVol = Mathf.Lerp(minVol, maxVol, throttle / maxThrottle);
            thrustAudio.volume = curVol * maxThrustVol;
        }
        else
        {
            thrustAudio.Stop();
        }

        //Forward force
        shipRb.AddForce(transform.up * throttle * throttleCoefficient);
        #endregion

        #region rotation
        //Rotation
        if (!shipLanding.isLanded)
        {
            transform.Rotate(0, 0, -inputX * horizontalInputAcceleration); 
        }

        #endregion

        #region extra shit
        //Cut Throttle when fuel is empty
        if (throttle > 0 && remainingFuel > 0)
        {
            remainingFuel -= throttle * .25f;
        }
        if(remainingFuel <= 0)
        {
            throttle = 0;
            remainingFuel = 0;
        }
        #endregion
    }

    //Throttle UI
    public void ThrottleSlider(float newThrottle) {throttle = newThrottle;}
}
