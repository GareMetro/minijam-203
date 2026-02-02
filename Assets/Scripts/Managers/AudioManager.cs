using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public float generalVolume = 1;
    public float musicVolume = 1;
    public float sfxVolume = 1;

    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);

            if(TryGetComponent<AudioPlayer>(out AudioPlayer audio))
                Destroy(audio);
            return;
        }
        Instance = this;
        if (DoNotDestroyOnLoad)
            DontDestroyOnLoad(this);
    }
}
