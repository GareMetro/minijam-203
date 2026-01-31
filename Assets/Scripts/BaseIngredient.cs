using UnityEngine;

[CreateAssetMenu(fileName = "BaseIngredient", menuName = "Scriptable Objects/BaseIngredient")]
public class BaseIngredient : ScriptableObject
{
    [SerializeField] public string foodName;
    [SerializeField] public Sprite icon;
    [SerializeField] public GameObject prefab;
}