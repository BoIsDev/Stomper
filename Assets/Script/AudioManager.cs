using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source------------)")]
    
    public AudioSource musicSource;
    public AudioSource SFXSource;
 

     [Header("---------Audio Clip------------)")]
    public AudioClip background;
    public AudioClip jump;

    public AudioClip acttack;
    public AudioClip door;
    public AudioClip enemyDead;
    public AudioClip hit;

    public AudioClip pick;
    public AudioClip duck;


    void Start()
    {
      
         if (musicSource != null)
        {
            musicSource.clip = background;
            musicSource.Play();  
        }
    }

    

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }


     public void PauseMusic()
    {
        if(musicSource.isPlaying)
            musicSource.Pause();
    }

    // Phương thức tiếp tục phát nhạc
    public void UnpauseMusic()
    {
        if(!musicSource.isPlaying)
            musicSource.UnPause();
    }
}
