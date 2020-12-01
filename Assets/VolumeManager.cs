using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public float musicVol = 1;
    public float thrustVol = 1;
    public float impactVol = 1;
    public float explosionVol = 1;
    private GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        musicVol = PlayerPrefs.HasKey("MusicVol") ? PlayerPrefs.GetFloat("MusicVol") : musicVol;
        thrustVol = PlayerPrefs.HasKey("ThrustVol") ? PlayerPrefs.GetFloat("ThrustVol") : thrustVol;
        impactVol = PlayerPrefs.HasKey("ImpactVol") ? PlayerPrefs.GetFloat("ImpactVol") : impactVol;
        explosionVol = PlayerPrefs.HasKey("ExplosionVol") ? PlayerPrefs.GetFloat("ExplosionVol") : explosionVol;
        settingsMenu = GameObject.FindGameObjectWithTag("settings");
        foreach (Transform child in settingsMenu.transform)
        {
            Slider slider = child.gameObject.GetComponent<Slider>();
            Debug.Log(slider);
            switch (child.gameObject.name)
            {
                case "MusicVolume":
                    slider.value = musicVol;
                    break;

                case "ThrustSFX":
                    slider.value = thrustVol ;
                    break;

                case "ImpactSFX":
                    slider.value = impactVol;
                    break;

                case "ExplosionSFX":
                    slider.value = explosionVol;
                    break;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        settingsMenu = GameObject.FindGameObjectWithTag("settings");
        foreach (Transform child in settingsMenu.transform)
        {
            Slider slider = child.gameObject.GetComponent<Slider>();
            Debug.Log(slider);
            switch(child.gameObject.name){
                case "MusicVolume":
                    if(musicVol!= slider.value) {
                        PlayerPrefs.SetFloat("MusicVol", slider.value);
                    }
                    musicVol = slider.value;
                        break;

                case "ThrustSFX":
                    if (thrustVol != slider.value)
                    {
                        PlayerPrefs.SetFloat("ThrustVol", slider.value);
                    }
                    thrustVol = slider.value;
                    break;

                case "ImpactSFX":
                    if (impactVol != slider.value)
                    {
                        PlayerPrefs.SetFloat("ImpactVol", slider.value);
                    }
                    impactVol = slider.value;
                    break;

                case "ExplosionSFX":
                    if (explosionVol != slider.value)
                    {
                        PlayerPrefs.SetFloat("ExplosionVol", slider.value);
                    }
                    explosionVol = slider.value;
                    break;

            }
        }
    }
}
