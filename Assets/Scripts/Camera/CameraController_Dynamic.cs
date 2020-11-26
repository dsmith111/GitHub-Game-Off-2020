using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//dynamic - camera zooms into ship only if close to planet objects.

public class CameraController_Dynamic : MonoBehaviour
{
    private Transform followTarget;
    private Rigidbody2D followTargetRB;
    private float cameraZ = -10;
    private new Camera camera;
    private float velocity = 0f;

    private CameraRange cameraRange;
    private float threshold = 151f; //used for knowing when ships too far to see and call UI change
    [HideInInspector] public bool isAboveThreshold;

    [HideInInspector] public bool enabled_dynamicCamera;
    private float targetSize;
    [Range(0, 20)] public float zoomSpeed = 1; //multiplier for camera zooming
    [SerializeField] float minZoomSize = 20f; //closest zoom amount the camera can get to the ship
    [SerializeField] float maxZoomSize = 60f;
    private float scaleCoefficient = 1.5f;

    private Vector3 dragOrigin;
    private bool isPanning;
    private GameObject orientationUIZoomedOut;

    private void Start()
    {
        followTarget = FindObjectOfType<ShipController>().transform;
        followTargetRB = followTarget.GetComponent<Rigidbody2D>();
        cameraZ = transform.position.z;
        camera = GetComponent<Camera>();
        cameraRange = FindObjectOfType<CameraRange>();
        enabled_dynamicCamera = true;
        targetSize = camera.orthographicSize; //sets zoom target to current camera size
        orientationUIZoomedOut = GameObject.Find("ZoomedOutOrientation");
    }


    private void Update()
    {
        FollowTarget();
        CheckThreshold();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            enabled_dynamicCamera = !enabled_dynamicCamera;
        }

        if (!enabled_dynamicCamera)
        {
            FreeCamera();
        }
        else
        {
            DynamicCamera();
        }
    }


    void FollowTarget() //Camera tracks target
    {
        if (followTarget)
        {
            transform.position = new Vector3(followTarget.position.x, followTarget.position.y, cameraZ);
        }
        else
        {
            return;
        }
    }


    void DynamicCamera() //Camera zooms in based on ships current speed
    {
        if (cameraRange.inRangeOfPlanet)
        {
            targetSize = minZoomSize;
        }
        else
        {
            targetSize += camera.orthographicSize;
        }
        targetSize = Mathf.Clamp(targetSize, minZoomSize, maxZoomSize);
        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, targetSize, ref velocity, zoomSpeed);
    }


    void FreeCamera() //scrolling in and out. scale scrolling outwards, right click panning, recenters on player when leaving
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel"); //scroll up - +0.1; down - -0.1;
        if (scrollInput != 0.0f)
        {
            targetSize -= scrollInput;
            if (scrollInput < 0)
            {
                targetSize *= scaleCoefficient;
            }
            else
            {
                targetSize /= scaleCoefficient;
            }
            targetSize = Mathf.Clamp(targetSize, minZoomSize, 100000f);
            camera.orthographicSize = Mathf.MoveTowards(camera.orthographicSize, targetSize, 10000f); //scrolls zoom
        }

        if (Input.GetMouseButton(1) && isPanning)
        {
            orientationUIZoomedOut.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            isPanning = true;
            return;
        }
        if (!Input.GetMouseButton(1))
        {
            isPanning = false;
            return;
        }

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * targetSize, pos.y * targetSize, 0);
        transform.Translate(move, Space.World);
    }


    void CheckThreshold()
    {
        if (camera.orthographicSize <= threshold)
        {
            isAboveThreshold = false;
        }
        else
        {
            isAboveThreshold = true;
        }
    }
}