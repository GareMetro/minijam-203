using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[Serializable]
[CreateAssetMenu(fileName = "BatiInfo", menuName = "Scriptable Objects/BatiInfo")]
public class BatiInfo : ScriptableObject
{
    [SerializeField] public AbstractBuilding batiPrefab;
    [SerializeField] public GameObject holoPrefab;
    [SerializeField] public Sprite icon;
}

//[CreateAssetMenu(fileName = "BatiInfos", menuName = "Scriptable Objects/BatiInfos")]
//public class BatiInfos : ScriptableObject
//{
//    [SerializeField]
//    public List<BatiInfo> batiInfos;
//}
