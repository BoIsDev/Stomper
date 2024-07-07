using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private AudioMixer audioMixer;
  
    void Start()
    {
        SetMucsicVolume();
        SetSFXVolume();
    }

    // Update is called once per frame
   public void SetMucsicVolume()
    {
        float volume = sliderMusic.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 10);
    }
   
    public void SetSFXVolume()
    {
        float volume2 = sliderSFX.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume2) * 10);
    }
}
