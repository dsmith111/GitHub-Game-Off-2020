using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final_Planet_Tracker : MonoBehaviour
{

    private GameObject player;
    private RectTransform rect;
    private GameObject finalObject;
    private Vector3 relativePosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rect = gameObject.GetComponent<RectTransform>();
        finalObject = GameObject.FindGameObjectWithTag("final_object");
        
    }



    // Update is called once per frame
    void Update()
    {
        if(finalObject == null) { finalObject = GameObject.FindGameObjectWithTag("final_object"); }
        relativePosition = (player.transform.position - finalObject.transform.position);
        relativePosition = new Vector3(relativePosition.x, relativePosition.y, 0);

        float angle = Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg;
        rect.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));

    }
}

