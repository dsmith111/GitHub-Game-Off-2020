using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinScreenUI : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Win Screen UI Objects")]
    public GameObject winScreen_bg;
    public GameObject rearImage;
    public GameObject textObjects;
    [Space(10)]

    [Header("Report Text Objects")]
    public TextMeshProUGUI totalScore;
    public TextMeshProUGUI distanceTraveled;
    public TextMeshProUGUI totalFuelConsumed;
    public TextMeshProUGUI totalRefuelAmount;
    public TextMeshProUGUI fastestSpeedMet;
    public TextMeshProUGUI totalLandings;
    [Space(10)]

    [Header("Final Ranking Object")]
    public TextMeshProUGUI finalRanking;

    [Space(10)]
    [SerializeField] float valueSum;
    [SerializeField] float valueBase = 100;
    [SerializeField] float valueAvg;
    [SerializeField] int valueFinal;
    [SerializeField] string letterGrade;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Time.timeScale = 1;
        winScreen_bg.SetActive(false);
        rearImage.SetActive(false);
        textObjects.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //function called from ShipLanding if landed on final planet
    public void WinScreen()
    {
        //breif wait time?
        //Time.timeScale = 0; //place at end i think
        winScreen_bg.SetActive(true);
        rearImage.SetActive(true);
        textObjects.SetActive(true);

        //display total score
        SetScoreText();
        //display total distance
        SetDistanceText();
        //display fuel
        SetFuelText();
        //display speed
        SetSpeedText();
        //display total landings
        SetTotalLandingsText();

        //calculate final grade
        GradeCalculator();
        //display final grade
        SetFinalGradeText();
    }

    void GradeCalculator()
    {
        valueSum = gameManager.totalScore + gameManager.totalDistanceTraveled + gameManager.totalFuelSpentTracker + gameManager.totalRefuelTracker + gameManager.fastestSpeedMet + (gameManager.totalLandings * 500);
        valueAvg = valueSum / valueBase;
        valueFinal = Mathf.RoundToInt(valueAvg);

    }

    void SetFinalGradeText()
    {
        //finalRanking.text = "Final Ranking: " + valueFinal;
        if(valueFinal >= 75000)
        {
            letterGrade = "S";
        }
        if (valueFinal <= 75000)
        {
            letterGrade = "A";
        }
        if (valueFinal <= 60000)
        {
            letterGrade = "B";
        }
        if (valueFinal <= 40000)
        {
            letterGrade = "C";
        }
        if (valueFinal <= 30000)
        {
            letterGrade = "D";
        }
        if (valueFinal < 10000)
        {
            letterGrade = "F";
        }

        finalRanking.text = "Final Ranking: " + letterGrade;
    }

    void SetScoreText()
    {
        totalScore.text = "Total Score: " + gameManager.totalScore;
    }

    void SetDistanceText()
    {
        double round = System.Math.Round(gameManager.totalDistanceTraveled, 2);
        distanceTraveled.text = "Distance Traveled: " + round + " Kilometers";
    }

    void SetFuelText()
    {
        double round = System.Math.Round(gameManager.totalFuelSpentTracker, 2);
        totalFuelConsumed.text = "Total Fuel Consumed: " + round + " Gallons";
    }

    void SetRefuelText()
    {
        double round = System.Math.Round(gameManager.totalRefuelTracker, 2);
        totalRefuelAmount.text = "Total Fuel from Refuel: " + round + " Gallons";
    }

    void SetSpeedText()
    {
        //fix this
        double round = System.Math.Round(gameManager.fastestSpeedMet, 2);
        fastestSpeedMet.text = "Fastest Speed Met: " + round + " Km/s";
    }

    void SetTotalLandingsText()
    {
        totalLandings.text = "Total Landings: " + gameManager.totalLandings;
    }


}
