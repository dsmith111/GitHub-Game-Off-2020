using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Image = UnityEngine.UI.Image;

public class GravityIndicator : MonoBehaviour
{

    [SerializeField]
    public float offDist = 5;

    [SerializeField]
    float warningThreshold = 2;

    [SerializeField]
    float dangerThreshold = 4;

    private Vector2 gAccel;

    private GameObject player;
    private RectTransform rect;
    private Vector3 relativePosition;
    private GameObject ClosestBody;

    [SerializeField]
    public double accelMag = 0.4;
    private Vector2 distance;

    private Vector2 lastVel;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rect = gameObject.GetComponent<RectTransform>();
        lastVel = player.GetComponent<Rigidbody2D>().velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
        GravitationalAcceleration(player.transform.position);

        #region Angle Calculation
        relativePosition = new Vector3(relativePosition.x, relativePosition.y, 0);
        float angle = Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg;
        rect.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        #endregion

        #region Relative Velocity Color Change

        Vector2 currentVel = player.GetComponent<Rigidbody2D>().velocity;

        // Change color based on speed heading toward planet
        Vector2 relativeVelocity = (currentVel * relativePosition.normalized);
        bool isNegative = (relativeVelocity.x + relativeVelocity.y) <= 0;

        float acceleration = (currentVel - lastVel).magnitude;

        lastVel = currentVel;
        gameObject.GetComponentInChildren<Image>().color = new Color32(97, 255, 120, 89);

        if (isNegative)
        {

            // velocity = meters/second -> meters / velocity = time to impact
            // Cross-reference time to impact with max thrust speed to determine danger
            // We need to have time to change our velocity to zero prior to impact
            // acceleration * time? - currentVelocity = 0 --> time to deaccelerate = currentVelocity/Acceleration
            // If this time is GREATER than our time to impact. change to red

            float impactTime = distance.magnitude / relativeVelocity.magnitude;
            float deacTime = currentVel.magnitude / ((float)accelMag/2);

            if (deacTime > impactTime*1.95)
            {
                gameObject.GetComponentInChildren<Image>().color = new Color32(255, 101, 97, 89);
            }
            else if (deacTime < (impactTime*1.95) && (deacTime > impactTime*.80))
            {
                gameObject.GetComponentInChildren<Image>().color = new Color32(255, 254, 98, 89);
            }
            else
            {
                gameObject.GetComponentInChildren<Image>().color = new Color32(97, 255, 120, 89);
            }

        }

        #endregion

        if (relativePosition.magnitude <= offDist)
        {
            GetComponentInChildren<Image>().enabled = false;
        }
        else
        {
            GetComponentInChildren<Image>().enabled = true;
        }

    }


    GameObject NearestBody(GameObject[] planets, Vector2 position)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = position;
        foreach (GameObject potentialTarget in planets)
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

    void GravitationalAcceleration(Vector2 currentPosition)
    {
        GameObject[] NearG = GameObject.FindGameObjectsWithTag("gbody");
        GameObject[] FinalObjects = GameObject.FindGameObjectsWithTag("final_object");
        GameObject[] NearObjects = NearG.Concat(FinalObjects).ToArray();

        ClosestBody = NearestBody(NearObjects, currentPosition);
        distance = (currentPosition - new Vector2(ClosestBody.transform.position.x, ClosestBody.transform.position.y));

        float maxDistance = ClosestBody.GetComponent<GravitationalBody>().maxDistance;

        relativePosition = currentPosition - ClosestBody.GetComponent<Rigidbody2D>().position;
        //the force of gravity will reduce by the distance squared
        float gravityFactor = 1f - (Mathf.Sqrt(distance.magnitude) / Mathf.Sqrt(maxDistance));

        //creates a vector that will force the otherbody toward this body, using the gravity factor times the mass of this body as the magnitude
        Vector2 gravitationalForce = relativePosition.normalized * (gravityFactor * ClosestBody.GetComponent<Rigidbody2D>().mass);
        gAccel.x = gravitationalForce.x;
        gAccel.y = gravitationalForce.y;

    }

}
