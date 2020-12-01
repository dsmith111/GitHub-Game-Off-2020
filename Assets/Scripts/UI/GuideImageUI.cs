using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideImageUI : MonoBehaviour
{
    public MenuUI menuUI;
    public bool guideImageOpen;
    public GameObject guideImage;

    // Start is called before the first frame update
    void Start()
    {
        menuUI = FindObjectOfType<MenuUI>();
    }

    // Update is called once per frame
    public void Toggle()
    {
        guideImageOpen = !guideImageOpen;
    }

    private void Update()
    {
        if (guideImageOpen)
        {
            guideImage.SetActive(true);
        }
        else
        {
            guideImage.SetActive(false);
        }

        if (!menuUI.gameIsPaused)
        {
            guideImage.SetActive(false);
            guideImageOpen = false;
        }
    }
}
