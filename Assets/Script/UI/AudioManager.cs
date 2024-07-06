using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source------------)")]
    public AudioSource musicSource;
    public AudioSource SFXSource;
    public AudioSource SFXBoss;
     [Header("---------Audio Clip Player------------)")]
    public AudioClip background;
    public AudioClip jump;
    public AudioClip acttack;
    public AudioClip door;
    public AudioClip enemyDead;
    public AudioClip hit;
    public AudioClip pick;
    public AudioClip duck;
    [Header("---------Audio Clip Boss------------)")]
    public AudioClip attackCallMonster;
    public AudioClip attackFireOn;
    public AudioClip attackFireBall;
    public AudioClip attackSkillMonster;
    public AudioClip attackIceSkill;
    public AudioClip attackSurikenBig;
    public AudioClip attackSurikenSmall;
    public AudioClip plantShoot;
    public AudioClip spikeShoot;


    public static AudioManager Instance => instance;
    private static AudioManager instance;   

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
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

    public void PlaySFXEnviroment(AudioClip clip)
    {
        if (clip != null && SFXBoss != null)
        {
            SFXBoss.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFXBoss or AudioClip is missing.");
        }
    }
}
