using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//[System.Serializable]
//public enum BaseIngredient
//{
//    RawSteak,
//    CookedSteak,
//    RawRice,
//    CookedRice,
//    SteakWithRice
//    // TODO : add other ingredients
//}

[CreateAssetMenu(fileName = "BaseIngredient", menuName = "Scriptable Objects/BaseIngredient")]
public class BaseIngredient : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField] public Sprite icon;
    [SerializeField] public GameObject prefab;
}


[System.Serializable]
public struct FoodIO
{
    public BaseIngredient ingredient;
    public Vector2Int position;
}

[System.Serializable]
public struct FoodInfo
{
    // public List<BaseIngredient> neededIngredients; // furta : not needed if we set the foodinfo for now
    
    public List<FoodIO> inputs;
    public FoodIO output;
    
    public int numberOfStepsNeeded;

    public float timeBeforeNextOrder;
    public GameObject prefab; // furta : is it really needed ?
}

public class FoodManager : Singleton<FoodManager>
{
    [SerializeField] float timeBeforeFirstOrder = 50;
    [SerializeField] List<FoodInfo> foodDict;
    [SerializeField] List<BaseIngredient> givenIngredients;
    [SerializeField] GameObject ingredientEntryPrefab;
    [SerializeField] GameObject foodOutputPrefab;

    private int currentFoodOrder = -1;

    public void Initialize()
    {
        StartCoroutine(newOrderCoroutine(timeBeforeFirstOrder));
    }

    IEnumerator newOrderCoroutine(float timeNeeded)
    {
        yield return new WaitForSeconds(timeNeeded);
        currentFoodOrder++;

        if (currentFoodOrder == foodDict.Count)
        {
            // TODO : add victory
            GameManager.Instance.Victory();
            yield return null;
        }
        else
        {
            AddNewFoodOrder();
            StartCoroutine(newOrderCoroutine(foodDict[currentFoodOrder].timeBeforeNextOrder));
        }
    }

    private void AddNewFoodOrder()
    {
        // TODO Instantiate the next foodOutput on the grid
        
        // we're adding the currentFoodOrder
        foreach (var foodInput in foodDict[currentFoodOrder].inputs)
        {
            // instanciate a foodInput building at foodInput.position
        }
        
        FoodIO nextFoodOutput = foodDict[currentFoodOrder].output;
        // instanciate a foodOutput building at nextFoodOutput.position
        
        // add inputs at their position
        
        // TODO if foodOutput needs an ingredient we don't have, instantiate the ingredient(s) on the grid
        // TODO add the ingredient to the list givenIngredients
    }
}
