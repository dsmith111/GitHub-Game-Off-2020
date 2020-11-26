using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreTMPro;
    public ScoreTracker scoreTracker;

    public string text = "Score: ";
    public int displayedScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreTMPro = GetComponent<TextMeshProUGUI>();
        scoreTracker = GameObject.FindObjectOfType<ScoreTracker>();
        scoreTMPro.text = text + displayedScore;
    }

    // Update is called once per frame
    void Update()
    {
        displayedScore = scoreTracker.currentScore;
        scoreTMPro.text = text + displayedScore;
    }
}
