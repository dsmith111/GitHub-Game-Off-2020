using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLauncher : MonoBehaviour
{
    //[HideInInspector]
    //public ShipController shipController;
    ////[HideInInspector]
    //public bool isLaunched;

    //private bool isPressed;
    //private float releaseDelay;
    //[SerializeField] float maxDragDistance = 3f;

    //private Rigidbody2D rb;
    //private SpringJoint2D sj;
    //private Rigidbody2D hookRb;
    //private LineRenderer lr;
    //private TrailRenderer tr;

    //// Start is called before the first frame update
    //void Awake()
    //{
    //    shipController = GetComponent<ShipController>();

    //    rb = GetComponent<Rigidbody2D>();
    //    sj = GetComponent<SpringJoint2D>();
    //    hookRb = sj.connectedBody;
    //    lr = GetComponent<LineRenderer>();
    //    tr = GetComponent<TrailRenderer>();

    //    isLaunched = false;
    //    lr.enabled = false;
    //    tr.enabled = false;

    //    releaseDelay = 1 / (sj.frequency * 4);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (!isLaunched)
    //    {
    //        if (isPressed)
    //        {
    //            DragBall();
    //        }
    //    }
    //    else
    //    {
    //        return;
    //        //put something here to sleep the launcher? currently if you
    //        //click right where the hook is, the script will still function.
    //    }
    //}

    //private void DragBall()
    //{
    //    SetLineRendererPosition();

    //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    float distance = Vector2.Distance(mousePosition, hookRb.position);

    //    if (distance > maxDragDistance)
    //    {
    //        Vector2 direction = (mousePosition - hookRb.position).normalized;
    //        rb.position = hookRb.position + direction * maxDragDistance;
    //    }
    //    else
    //    {
    //        rb.position = mousePosition;
    //    }
    //}

    //private void SetLineRendererPosition()
    //{
    //    Vector3[] positions = new Vector3[2];
    //    positions[0] = rb.position;
    //    positions[1] = hookRb.position;
    //    lr.SetPositions(positions);
    //}

    //private IEnumerator Release()
    //{
    //    yield return new WaitForSeconds(releaseDelay);
    //    sj.enabled = false;
    //}

    //private void OnMouseDown()
    //{
    //    isPressed = true;
    //    rb.isKinematic = true;
    //    lr.enabled = true;
    //}

    //private void OnMouseUp()
    //{
    //    isPressed = false;
    //    rb.isKinematic = false;
    //    StartCoroutine(Release());
    //    isLaunched = true;
    //    lr.enabled = false;
    //    tr.enabled = true;
    //}
}
