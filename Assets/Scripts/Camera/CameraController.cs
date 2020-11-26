using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Transform followTarget;
    private float cameraZ = -10;
    private new Camera camera;

    private bool isZoomedOut;
    [Range(2, 30)]
    [SerializeField] float cameraSize_Close = 8f;
    [Range(40, 200)]
    [SerializeField] float cameraSize_Far = 60f;

    private void Start()
    {
        followTarget = FindObjectOfType<ShipController>().transform;
        cameraZ = transform.position.z;
        camera = GetComponent<Camera>();
        isZoomedOut = false;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ZoomToggle();
        }
        if (followTarget)
        {
            transform.position = new Vector3(followTarget.position.x, followTarget.position.y, cameraZ);
        }
    }

    void ZoomToggle()
    {
        isZoomedOut = !isZoomedOut;
        if (isZoomedOut)
        {
            camera.orthographicSize = cameraSize_Far;
        }
        else
        {
            camera.orthographicSize = cameraSize_Close;
        }
    }
}
