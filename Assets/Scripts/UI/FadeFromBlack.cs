using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeFromBlack : MonoBehaviour
{


    public float FadeRate;
    private Image image;
    private float targetAlpha;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        targetAlpha = image.color.a;
        image.color = Color.black;
        Invoke("startFadeOut", 1);

    }

    IEnumerator FadeOut()
    {
        targetAlpha = 0.0f;
        Color curColor = image.color;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRate * Time.deltaTime);
            image.color = curColor;
            yield return null;
        }
    }

    void startFadeOut()
    {

        StartCoroutine(FadeOut());
    }
}