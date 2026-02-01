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




[System.Serializable]
public struct FoodIO
{
    public BaseIngredient ingredient;
    public Vector2Int position;
    public int rotation;
}

[System.Serializable]
public struct FoodInfo
{
    public List<FoodIO> inputs;
    public FoodIO output;
    
    public int numberOfStepsNeeded;

    public float timeBeforeNextOrder;
    public GameObject prefab;
}

public class FoodManager : Singleton<FoodManager>
{
    [SerializeField] float timeBeforeFirstOrder = 50;
    [SerializeField] List<FoodInfo> foodDict;
    [SerializeField] List<BaseIngredient> givenIngredients;
    [SerializeField] public BaseIngredient caca;
    [SerializeField] GameObject ingredientEntryPrefab;
    [SerializeField] GameObject foodOutputPrefab;
    [SerializeField] private FoodSource inputBuildingPrefab;
    [SerializeField] private FoodReceiver outputBuildingPrefab;
    

    private int currentFoodOrderIndex = -1;

    public void Initialize()
    {
        StartCoroutine(newOrderCoroutine(timeBeforeFirstOrder));
    }

    IEnumerator newOrderCoroutine(float timeNeeded)
    {
        yield return new WaitForSeconds(timeNeeded);
        currentFoodOrderIndex++;

        if (currentFoodOrderIndex == foodDict.Count)
        {
            GameManager.Instance.Victory();
            yield return null;
        }
        else
        {
            AddNewFoodOrder();
            StartCoroutine(newOrderCoroutine(foodDict[currentFoodOrderIndex].timeBeforeNextOrder));
        }
    }

    private void AddNewFoodOrder()
    {
        // TODO Instantiate the next foodOutput on the grid
        
        // we're adding the currentFoodOrderIndex
        foreach (var foodInput in foodDict[currentFoodOrderIndex].inputs)
        {
            if(!foodInput.ingredient)
                continue;
            // instanciate a foodInput building at foodInput.position
            inputBuildingPrefab.food = foodInput.ingredient;
            Grid.GridInstance.AddObject(inputBuildingPrefab, foodInput.position, foodInput.rotation);
        }
        
        FoodIO nextFoodOutput = foodDict[currentFoodOrderIndex].output;
        outputBuildingPrefab.requiredFood = nextFoodOutput.ingredient;
        Grid.GridInstance.AddObject(outputBuildingPrefab, nextFoodOutput.position, nextFoodOutput.rotation);
        // instanciate a foodOutput building at nextFoodOutput.position
        
        // add inputs at their position
        
        // TODO if foodOutput needs an ingredient we don't have, instantiate the ingredient(s) on the grid
        // TODO add the ingredient to the list givenIngredients
    }
}
