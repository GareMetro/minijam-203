using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public float generalVolume = 1;
    public float musicVolume = 1;
    public float sfxVolume = 1;

    protected override void Awake()
    {
        AudioPlayer.soundPlayerNumbers = new();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        if (DoNotDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
    }
}
