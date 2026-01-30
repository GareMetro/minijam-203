using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HazardType
{
    
}

[System.Serializable]
public struct HazardInfo
{
    public HazardType hazardType;
    public int difficultyNeeded;
    public float weight;
}

public class HazardManager : Singleton<HazardManager>
{
    [SerializeField] float firstTimeNeeded = 50;
    [SerializeField] List<HazardInfo> foodDict;

    public void Initialize()
    {
        StartCoroutine(newHazardCoroutine(firstTimeNeeded));
    }

    IEnumerator newHazardCoroutine(float timeNeeded)
    {
        yield return new WaitForSeconds(timeNeeded);
        PlayNewHazard();
        StartCoroutine(newHazardCoroutine(timeNeeded /*/ TODO 0.9f ? - 5 ?*/));
    }

    private void PlayNewHazard()
    {
        // TODO get a random hazardInfo
        // switch case for event type, play what needs to be played here
    }
}
