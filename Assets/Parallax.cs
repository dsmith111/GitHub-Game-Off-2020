using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    private float length, startposx, startposy;
    public GameObject cam;
    [SerializeField]
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startposx = transform.position.x;
        startposy = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        float tempx = (cam.transform.position.x * (1 - parallaxEffect));
        float tempy = (cam.transform.position.y * (1 - parallaxEffect));
        float distx = (cam.transform.position.x * parallaxEffect);
        float disty = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startposx + distx, startposy + disty, transform.position.z);
        if (tempx > startposx + length) startposx += length;
        else if (tempx < startposx - length) startposx -= length;
        if (tempy > startposy + length) startposy += length;
        else if (tempy < startposy - length) startposy -= length;

    }
}
