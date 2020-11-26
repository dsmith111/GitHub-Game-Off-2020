using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Scroll : MonoBehaviour
{
    private Transform followTarget;
    private float cameraZ = -10;
    private new Camera camera;
    public bool isAboveThreshold = false;

    //Zooming vars
    [Tooltip("Size at which scaling factor changes")] [SerializeField] float threshold = 150f;
    private float zoomSpeed;
    private float smoothSpeed;
    private float targetSize;
    [Space(10)]
    [SerializeField] float slowZoomSpeed = 40;
    [SerializeField] float fastZoomSpeed = 2000;
    [Space(10)]
    [SerializeField] float slowSmoothSpeed = 100f;
    [SerializeField] float fastSmoothSpeed = 10000f;
    [Space(10)]
    [SerializeField] float minSize = 4f;
    [SerializeField] float maxSize = 50000f;


    // Start is called before the first frame update
    void Start()
    {
        followTarget = FindObjectOfType<ShipController>().transform;
        cameraZ = transform.position.z;
        camera = GetComponent<Camera>();
        targetSize = camera.orthographicSize;
        zoomSpeed = slowZoomSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");

        //ScrollWheel changes the target of the camera's zoom amount
        if(zoomInput != 0.0f)
        {
            EnableScrollControl(zoomInput);
        }
        //zooming amount slows if close enough to the ship
        if(camera.orthographicSize <= threshold)
        {
            isAboveThreshold = false;
            smoothSpeed = slowSmoothSpeed;
            zoomSpeed = slowZoomSpeed;
        }
        else //and scales higher once futher away from the ship
        {
            isAboveThreshold = true;
            smoothSpeed = fastSmoothSpeed;
            zoomSpeed = fastZoomSpeed;
        }
        //sets camera size to where we tell it
        camera.orthographicSize = Mathf.MoveTowards(camera.orthographicSize, targetSize, smoothSpeed * Time.deltaTime);

        //matches cameras position to target's position
        if (followTarget)
        {
            transform.position = new Vector3(followTarget.position.x, followTarget.position.y, cameraZ);
        }
    }


    void EnableScrollControl(float zoomInput)
    {
        targetSize -= zoomInput * zoomSpeed;
        targetSize = Mathf.Clamp(targetSize, minSize, maxSize);
    }
}
