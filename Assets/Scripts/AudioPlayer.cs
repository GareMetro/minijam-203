using System.Collections.Generic;
using UnityEngine;

public enum ReducedSoundType
{
    None,
    Assembler,
    Cloner,
    Cutter,
    FoodReceiver,
    Furnace,
    Launcher,
    Mixer
}

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public ReducedSoundType reducedSoundType;
    public bool isMusic = false;
    public bool loop = false;
    public int playOnStart = -1;
    AudioSource selfSource;
    public List<AudioClip> audioClips;
    public static Dictionary<ReducedSoundType, int> soundPlayerNumbers;
    public float selfVolumeMod = 1f;

    private void Awake() 
    {
        selfSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (reducedSoundType != ReducedSoundType.None)
        {
            if (soundPlayerNumbers.ContainsKey(reducedSoundType))
                soundPlayerNumbers[reducedSoundType]++;
            else
                soundPlayerNumbers.Add(reducedSoundType, 1);
        }

        if (playOnStart >= 0 && playOnStart < audioClips.Count)
        {
            PlaySound(playOnStart);
        }
    }

    void OnDestroy()
    {
        if (reducedSoundType != ReducedSoundType.None)
        {
            soundPlayerNumbers[reducedSoundType]--;
        }
    }

    void Update()
    {
        if (isMusic)
            selfSource.volume = AudioManager.Instance.generalVolume * AudioManager.Instance.musicVolume;
    }

    public void PlaySound(int clipId)
    {
        selfSource.volume = AudioManager.Instance.generalVolume * selfVolumeMod * (isMusic ? AudioManager.Instance.musicVolume : AudioManager.Instance.sfxVolume);
        if (reducedSoundType != ReducedSoundType.None)
            selfSource.volume /= soundPlayerNumbers[reducedSoundType];
        if (loop)
        {
            selfSource.loop = true;
            selfSource.clip = audioClips[clipId];
            selfSource.Play();
        }
        else
            selfSource.PlayOneShot(audioClips[clipId]);
    }
}
