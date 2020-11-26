using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject leftThruster;
    public GameObject rightThruster;
    public Rigidbody2D LeftThrusterRb;
    public Rigidbody2D RightThrusterRb;

    public float verticalInputAcceleration = 10;
    public float horizontalInputAcceleration = 20;
    public float strafeInputAcceleration = 10;
    public float maxSpeed = 10;
    public float maxRotationSpeed = 100;
    public float velocityDrag = 1;
    public float rotationDrag = 1;
    private Vector3 velocity;
    private float zRotationVelocity;

    private void Start()
    {
        LeftThrusterRb = leftThruster.GetComponent<Rigidbody2D>();
        RightThrusterRb = rightThruster.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

            Vector3 acceleration = Input.GetAxis("Vertical") * verticalInputAcceleration * transform.up;
            velocity += acceleration * Time.deltaTime;

            float zTurnAcceleration = -1 * Input.GetAxis("Horizontal") * horizontalInputAcceleration;
            zRotationVelocity += zTurnAcceleration * Time.deltaTime;

    }

    private void FixedUpdate()
    {
        velocity = velocity * (1 - Time.deltaTime * velocityDrag);
        //velocity = Vector3.ClampMagnitude(velocity, maxSpeed); //clamps to maxspeed
        zRotationVelocity = zRotationVelocity * (1 - Time.deltaTime * rotationDrag);
        zRotationVelocity = Mathf.Clamp(zRotationVelocity, -maxRotationSpeed, maxRotationSpeed);

        transform.position += velocity * Time.deltaTime;
        transform.Rotate(0, 0, zRotationVelocity * Time.deltaTime);
    }
}