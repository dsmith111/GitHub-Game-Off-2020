using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraModeIndicatorUI : MonoBehaviour
{
    private TextMeshProUGUI cameraIndicatorTMPro;
    private CameraController_Dynamic camera;

    private string text = "Camera: ";
    private string lockText = "Locked";
    private string freeText = "Free";

    // Start is called before the first frame update
    void Start()
    {
        cameraIndicatorTMPro = GetComponent<TextMeshProUGUI>();
        camera = GameObject.FindObjectOfType<CameraController_Dynamic>();
        cameraIndicatorTMPro.text = text + lockText;
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.enabled_dynamicCamera)
        {
            cameraIndicatorTMPro.text = text + lockText;
        }
        else
        {
            cameraIndicatorTMPro.text = text + freeText;
        }
    }
}
