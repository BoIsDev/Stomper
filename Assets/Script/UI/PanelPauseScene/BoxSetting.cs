using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boxSetting : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sliderMusic;
    public Slider sliderSFX;

    public void updateMusichBar(float currentValue, float maxValue)
    {
        sliderMusic.value = currentValue / maxValue;
    }

    public void updateSFXBar(float currentValue, float maxValue)
    {
        sliderSFX.value = currentValue / maxValue;
    }


}
