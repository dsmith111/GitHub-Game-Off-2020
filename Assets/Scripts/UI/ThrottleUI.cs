using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrottleUI : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxValue(float throttle)
    {
        slider.maxValue = throttle;
        slider.value = 0;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetThrottle(float throttle)
    {
        slider.value = throttle;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
