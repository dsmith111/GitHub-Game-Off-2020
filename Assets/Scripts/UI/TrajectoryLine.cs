using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField]
    int numberOfPoints = 3;

    private GameObject[] pointArray;
    private Camera mainCamera;

    [SerializeField]
    GameObject trajectoryPoint;

    [SerializeField]
    float scaleD = 14.32f;
    [SerializeField]
    float scaleD2 = -0.2f;

    [SerializeField]
    int sizeScale = 1;


    private Vector2 lastVel;
    private Vector2 gAccel;
    private Vector2 lastPointVel;
    private Vector2 lastPointPos;
    private Vector2 pointSize;

    // Start is called before the first frame update
    void Start()
    {
        gAccel = new Vector2(0,0);
        lastVel = GetComponent<Rigidbody2D>().velocity;
        InitializePoints();
        pointSize.x = trajectoryPoint.transform.localScale.x;
        pointSize.y = trajectoryPoint.transform.localScale.y;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<TrajectoryLine>().numberOfPoints != pointArray.Length || !pointArray[0])
        {
            InitializePoints();
        }
        gAccel = new Vector2(0, 0);
        for (int i = 0; i < numberOfPoints; i++)
        {
            if (i == 0)
            {
                lastPointVel = GetComponent<Rigidbody2D>().velocity;
                lastPointPos = gameObject.transform.position;
            }
            Vector2 tempTrans = PointLoc(pointArray[i], i);
            if (tempTrans.x == Mathf.Infinity){ tempTrans.x = 0;}
            if (tempTrans.y == Mathf.Infinity) { tempTrans.y = 0; }
            pointArray[i].transform.position = new Vector3(tempTrans.x, tempTrans.y, 0);
            Vector3 scaledPoints = new Vector3(mainCamera.orthographicSize * pointSize.x, mainCamera.orthographicSize * pointSize.y, 0);
            Debug.Log(mainCamera.orthographicSize);
            pointArray[i].transform.localScale = scaledPoints;
        }
        lastVel = gameObject.GetComponent<Rigidbody2D>().velocity;
    }

    Vector2 PointLoc(GameObject point, int ind)
    {
        ind += 1;
        Vector2 cVel = gameObject.GetComponent<Rigidbody2D>().velocity;
        //gAccel = (cVel - lastVel)/Time.fixedDeltaTime;
        GravitationalAcceleration(lastPointPos);
        Vector2 nVel = lastPointVel + ( (gAccel/scaleD2) * Time.fixedDeltaTime);
        Vector2 dPos = ((nVel + lastPointVel) / 2) * scaleD * Time.fixedDeltaTime;
        Vector2 gO = gameObject.transform.position;
        Vector2 newPos = lastPointPos + dPos;


        lastPointVel = nVel;
        lastPointPos = newPos;

        return (newPos);
    }

    void InitializePoints()
    {
        pointArray = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            pointArray[i] = Instantiate(trajectoryPoint, gameObject.transform.position, gameObject.transform.rotation);
            // pointArray[i].transform.localScale = parentScale/sizeScale;
        }
    }

    GameObject NearestBody(GameObject[] planets)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
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
        GameObject[] FinalObjects= GameObject.FindGameObjectsWithTag("final_object");
        GameObject[] NearObjects = NearG.Concat(FinalObjects).ToArray();

        GameObject ClosestBody = NearestBody(NearObjects);
        Vector2 distance = (currentPosition - new Vector2 (ClosestBody.transform.position.x,ClosestBody.transform.position.y));

        float maxDistance = ClosestBody.GetComponent<GravitationalBody>().maxDistance;

        Vector2 relativePosition = currentPosition - ClosestBody.GetComponent<Rigidbody2D>().position;
        //the force of gravity will reduce by the distance squared
        float gravityFactor = 1f - (Mathf.Sqrt(distance.magnitude) / Mathf.Sqrt(maxDistance));

        //creates a vector that will force the otherbody toward this body, using the gravity factor times the mass of this body as the magnitude
        Vector2 gravitationalForce = relativePosition.normalized * (gravityFactor * ClosestBody.GetComponent<Rigidbody2D>().mass);
        gAccel.x = gravitationalForce.x;
        gAccel.y = gravitationalForce.y;

    }


}
