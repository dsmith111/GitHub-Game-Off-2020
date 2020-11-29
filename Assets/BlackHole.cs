using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    [HideInInspector] [SerializeField] Rigidbody2D shipRb;

    private void Awake()
    {
        shipRb = GameObject.Find("Ship New").GetComponent<Rigidbody2D>();
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        //Vector2 direction = new Vector2((float)Random.Range(-10, 10), (float)Random.Range(-10, 10));
        //float force = (float)Random.Range(-10, 10);


        if (other.gameObject.tag == "Player")
        {

            shipRb.transform.position = GameObject.FindWithTag("whitehole").transform.position;

        }
    }

}
