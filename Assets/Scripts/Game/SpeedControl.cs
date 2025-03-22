//The project is licensed under GPL-3.0, which requires all modifications and distributions to adhere to the same license.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControl : MonoBehaviour
{
    [SerializeField] Slider sliderSpeed; 

    void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        Time.timeScale = sliderSpeed.value;
    }

    public void ControlSpeed(float amount)
    {
        Time.timeScale = amount;
    }  
}
