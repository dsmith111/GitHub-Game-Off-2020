using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public bool gameIsPaused;
    public GameObject menuBG;
    public GameObject menuObjects;
    public GameObject menuSettings;

    public GuideImageUI guideImageUIScript;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
        menuBG.SetActive(false);
        menuObjects.SetActive(false);
        guideImageUIScript = FindObjectOfType<GuideImageUI>();
        menuSettings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0;
            menuBG.SetActive(true);
            menuObjects.SetActive(true);

        }
        else
        {
            Time.timeScale = 1;
            menuBG.SetActive(false);
            menuObjects.SetActive(false);
            menuSettings.SetActive(false);
        }
    }

    public void Resume()
    {
        gameIsPaused = false;
        PauseGame();
    }

    public void NewGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void GuideImage()
    {

        guideImageUIScript.Toggle();
    }

    public void Settings()
    {
        menuObjects.SetActive(false);
        menuSettings.SetActive(true);
    }

    public void SettingsBack()
    {
        menuObjects.SetActive(true);
        menuSettings.SetActive(false);
        PlayerPrefs.Save();

    }

    public void End()
    {
        Application.Quit();
    }
}
