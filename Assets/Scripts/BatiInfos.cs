using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct BatiInfo
{
    [SerializeField] AbstractBuilding batiPrefab;
    [SerializeField] GameObject holoPrefab;
    [SerializeField] Sprite icon; // jsp si on utilisera hehe
}

[CreateAssetMenu(fileName = "BatiInfos", menuName = "Scriptable Objects/BatiInfos")]
public class BatiInfos : ScriptableObject
{
    [SerializeField]
    public List<BatiInfo> batiInfos;
}
