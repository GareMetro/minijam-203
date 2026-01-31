using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[Serializable]
public struct BatiInfo
{
    [SerializeField] public AbstractBuilding batiPrefab;
    [SerializeField] public GameObject holoPrefab;
    [SerializeField] public Sprite icon; // jsp si on utilisera hehe
    [SerializeField] public InputActionReference inputAction;
}

[CreateAssetMenu(fileName = "BatiInfos", menuName = "Scriptable Objects/BatiInfos")]
public class BatiInfos : ScriptableObject
{
    [SerializeField]
    public List<BatiInfo> batiInfos;
}
