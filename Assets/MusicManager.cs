using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] playList;
    public VolumeManager volumeManager;
    public ShipController shipController;
    // Preset
    [SerializeField]
    [Range(0, 1)]
    public float volMax = 1;
    [Header("Settings Volume")]
    [Range(0, 1)]
    public float maxMusicVol = 1;

    // Triggers For Music
    private bool launched = false;
    private bool playing = false;
    private int track = 0;

    void Start()
    {
        volumeManager = FindObjectOfType<VolumeManager>();
        shipController = FindObjectOfType<ShipController>();
    }

    // Update is called once per frame
    void Update() {

        if (!volumeManager)
        {
            volumeManager = FindObjectOfType<VolumeManager>();
        }
        if (maxMusicVol != volumeManager.musicVol) {
            maxMusicVol = volumeManager.musicVol;
            playList[track].volume = maxMusicVol * volMax;
        }

        if (shipController.throttle > 0) { launched = true; }
        if(launched && !playing){
            //Start up playlist coroutine
            playing = true;
            StartCoroutine(PlayList());
        }

    }
    IEnumerator PlayList()
    {
        for (track = 0; track < playList.Length; track++)
        {
            volMax = playList[track].volume;
            playList[track].volume = maxMusicVol * volMax;
            playList[track].Play();
            yield return new WaitForSeconds(playList[track].clip.length);
        }
        
    }
}
