using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrientationUI : MonoBehaviour
{
    private GameObject orientationZoomedIn;
    private GameObject orientationZoomedOut;

    private RectTransform zIRect;
    private RectTransform zORect;

    private ShipController shipController;
    [SerializeField] CameraController_Dynamic camController;

    private bool toggle;

    private void Start()
    {
        orientationZoomedIn = GameObject.Find("ZoomedInOrientation");
        orientationZoomedOut = GameObject.Find("ZoomedOutOrientation");

        zIRect = orientationZoomedIn.GetComponent<RectTransform>();
        zORect = orientationZoomedOut.GetComponent<RectTransform>();

        shipController = FindObjectOfType<ShipController>();
        camController = FindObjectOfType<CameraController_Dynamic>();
    }
    private void FixedUpdate()
    {
        if (camController.isAboveThreshold)
        {
            ToggleFarCamera();
        }
        if (!camController.isAboveThreshold)
        {
            ToggleCloseCamera();
        }

        if(orientationZoomedIn.activeInHierarchy == true)
        {
            zIRect.transform.rotation = shipController.transform.rotation;
        }

        if(orientationZoomedOut.activeInHierarchy == true)
        {
            zORect.transform.rotation = shipController.transform.rotation;
        }
    }

    void ToggleCloseCamera()
    {
        orientationZoomedIn.SetActive(true);
        orientationZoomedOut.SetActive(false);
    }

    void ToggleFarCamera()
    {
        orientationZoomedIn.SetActive(false);
        orientationZoomedOut.SetActive(true);
    }
}