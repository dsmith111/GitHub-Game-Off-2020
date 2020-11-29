using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class GravitationalBody : MonoBehaviour
{
    [SerializeField]
    public float maxDistance;

    [SerializeField]
    public float startingMass;
    
    [SerializeField]
    public Vector2 initialVelocity;

    [SerializeField]
    public float initialAngularDrag = 0.1f;

    //I use a static list of bodies so that we don't need to Find them every frame
    static List<Rigidbody2D> attractableBodies = new List<Rigidbody2D>();

    void Start()
    {

        SetupRigidbody2D();
        //Add this gravitational body to the list, so that all other gravitational bodies can be effected by it
        attractableBodies.Add(GetComponent<Rigidbody2D>());

    }

    void SetupRigidbody2D()
    {

        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Rigidbody2D>().drag = 0f;
        GetComponent<Rigidbody2D>().angularDrag = initialAngularDrag;
        GetComponent<Rigidbody2D>().mass = startingMass;
        GetComponent<Rigidbody2D>().velocity = initialVelocity;

    }

    void FixedUpdate()
    {

        foreach (Rigidbody2D otherBody in attractableBodies)
        {

            if (otherBody == null)
                continue;

            //We arn't going to add a gravitational pull to our own body
            if (otherBody == GetComponent<Rigidbody2D>())
                continue;

            otherBody.AddForce(DetermineGravitationalForce(otherBody));

        }

    }

    Vector2 DetermineGravitationalForce(Rigidbody2D otherBody)
    {

        Vector2 relativePosition = GetComponent<Rigidbody2D>().position - otherBody.position;

        float distance = Mathf.Clamp(relativePosition.magnitude, 0, maxDistance);

        //the force of gravity will reduce by the distance squared
        float gravityFactor = 1f - (Mathf.Sqrt(distance) / Mathf.Sqrt(maxDistance));

        //creates a vector that will force the otherbody toward this body, using the gravity factor times the mass of this body as the magnitude
        Vector2 gravitationalForce = relativePosition.normalized * (gravityFactor * GetComponent<Rigidbody2D>().mass);

        return gravitationalForce;

    }

}
