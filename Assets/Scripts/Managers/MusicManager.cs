using UnityEngine;
using System.Collections;

public class MusicManager : SingletonUnity<MusicManager>
{

    public AudioSource musicSource;
    private float musicVolume = 1f;

    public float MusicVolume
    {
        get
        {
            return musicVolume;
        }
        set
        {
            musicVolume = value;
            musicSource.volume = musicVolume;
        }
    }
    
}

