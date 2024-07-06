using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HealthCavans : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public void updateHealthBar( float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}
