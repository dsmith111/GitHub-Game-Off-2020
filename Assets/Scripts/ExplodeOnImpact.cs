using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
    public float deathTime = 145f;
    float vI;
    float vF;

    public AudioSource impactAudio;
    private VolumeManager volumeManager;
    private float maxVol = 1;
    [SerializeField]
    public int destroyForce = 11;

    [SerializeField]
    GameObject animatedObject;

    bool dying = false;
    
    // Start is called before the first frame update
    void Start()
    {
        volumeManager = FindObjectOfType<VolumeManager>();
        maxVol = impactAudio.volume;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            vI = GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!volumeManager)
        {
            volumeManager = FindObjectOfType<VolumeManager>();
        }
        vF = GetComponent<Rigidbody2D>().velocity.magnitude;
        float deltaV = Mathf.Abs(vF - vI);
        float impactForce = (deltaV) * GetComponent<Rigidbody2D>().mass;
        Debug.Log("Force = " + impactForce);

        if(impactForce >= 0.05 * destroyForce && impactForce < destroyForce)
        {
            if (!impactAudio.isPlaying)
            {

                impactAudio.volume = maxVol * volumeManager.impactVol;
                impactAudio.Play();
            }
        }
        if(impactForce >= destroyForce && !dying)
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(animatedObject, gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation) as GameObject;
            AudioSource[] sounds = explosion.GetComponentsInChildren<AudioSource>();
            for(int i = 0; i < sounds.Length; i++)
            {
                sounds[i].volume = sounds[i].volume * volumeManager.explosionVol;
            }
            Destroy(explosion, deathTime * Time.deltaTime);
            dying = true;

        }
   
    }


    }
