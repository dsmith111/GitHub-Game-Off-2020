using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLanding : MonoBehaviour
{
    [SerializeField] public Transform groundCheck;
    public LayerMask planetLayer;
    public bool isLanded = false;
    public float groundCheckRadius = .03f;
    private GameObject finalObject;

    private ShipController shipController;
    private GravitationalBody gravScript;

    // Start is called before the first frame update
    void Start()
    {
        shipController = FindObjectOfType<ShipController>();
        gravScript = GetComponent<GravitationalBody>();
        finalObject = GameObject.FindGameObjectWithTag("final_object");
    }



    // Update is called once per frame
    void Update()
    {
        //Set current velocity
        isLanded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, planetLayer);
        if (isLanded)
        {

            // Score multiplier

            if (finalObject == null) { finalObject = GameObject.FindGameObjectWithTag("final_object"); }
            float relativePosition = (gameObject.transform.position - finalObject.transform.position).magnitude;
            if (relativePosition < 500){
                //Final Planet Score Addition
                Debug.Log("You've Won!");
            }
            // Regular Planet Score Addition
            Debug.Log("Landed");
            // Landing velocity = last velocity
        }
        //Update last velocity
    }
}
