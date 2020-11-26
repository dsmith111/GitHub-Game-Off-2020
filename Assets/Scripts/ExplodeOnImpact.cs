using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
    public float deathTime = 145f;
    float vI;
    float vF;

    [SerializeField]
    public int destroyForce = 11;

    [SerializeField]
    GameObject animatedObject;

    bool dying = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
            vI = GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        vF = GetComponent<Rigidbody2D>().velocity.magnitude;
        float deltaV = Mathf.Abs(vF - vI);
        float impactForce = (deltaV) * GetComponent<Rigidbody2D>().mass;
        Debug.Log("Force = " + impactForce);
        if(impactForce >= destroyForce && !dying)
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(animatedObject, gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation) as GameObject;

            Destroy(explosion, deathTime * Time.deltaTime);
            dying = true;

        }
   
    }

   


    }
