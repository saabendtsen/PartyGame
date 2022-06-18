using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sensitivity : MonoBehaviour
{
    public TextMeshProUGUI sliderValue;
    public Slider slider;
    public static float sensitivity = 2.5f;

    public void Update()
    {
        sliderValue.text = slider.value.ToString("0.00");
    }

    public void SetSensitivity(float newsensitivity)
    {
        sensitivity = newsensitivity;
    }


}
