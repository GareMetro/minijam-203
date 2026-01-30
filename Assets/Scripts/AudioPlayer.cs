using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public bool isMusic = false;
    AudioSource selfSource;
    public List<AudioClip> audioClips;

    private void Awake() 
    {
        selfSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isMusic)
            selfSource.volume = AudioManager.Instance.generalVolume * AudioManager.Instance.musicVolume;
    }

    public void PlaySound(int clipId)
    {
        selfSource.volume = AudioManager.Instance.generalVolume * (isMusic ? AudioManager.Instance.musicVolume : AudioManager.Instance.sfxVolume);
        selfSource.PlayOneShot(audioClips[clipId]);
    }
}
