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

    public void PlaySound(int clipId)
    {
        selfSource.volume = AudioManager.Instance.generalVolume * (isMusic ? AudioManager.Instance.musicVolume : AudioManager.Instance.sfxVolume);
        selfSource.PlayOneShot(audioClips[clipId]);
    }
}
