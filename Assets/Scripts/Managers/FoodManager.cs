using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;

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
    
    //public int numberOfStepsNeeded;

    public float timeBeforeNextOrder;
    //public GameObject prefab;
}

public class FoodManager : Singleton<FoodManager>
{
    [SerializeField] float timeBeforeFirstOrder = 50;
    [SerializeField] List<FoodInfo> foodDict;
    [SerializeField] List<Vector2Int> Walls;
    [SerializeField] List<BaseIngredient> givenIngredients;
    [SerializeField] public BaseIngredient caca;
    //[SerializeField] GameObject ingredientEntryPrefab;
    //[SerializeField] GameObject foodOutputPrefab;
    [SerializeField] private FoodSource inputBuildingPrefab;
    [SerializeField] private FoodReceiver outputBuildingPrefab;
    [SerializeField] private Wall wallPrefab;

    private List<GameObject> CurrentReceivers;
    [SerializeField] private float satisfactionWinThreshold;
    
    private int currentFoodOrderIndex = -1;

    public void Initialize()
    {
        CurrentReceivers = new();

        StartCoroutine(wallPlacementCoroutine(timeBeforeFirstOrder));
        StartCoroutine(newOrderCoroutine(timeBeforeFirstOrder));
    }

    IEnumerator wallPlacementCoroutine(float timeNeeded)
    {

        yield return new WaitForSeconds(timeNeeded);

        foreach (Vector2Int pos in Walls)
        {
            Grid.GridInstance.AddObject(wallPrefab, pos, 0, true);
        }

        yield return null;
    }
        
        
        
    IEnumerator newOrderCoroutine(float timeNeeded)
    {
        yield return new WaitForSeconds(timeNeeded);
        currentFoodOrderIndex++;

        if (currentFoodOrderIndex >= foodDict.Count)
        {
            currentFoodOrderIndex = foodDict.Count;
            bool shouldWin = true;
            foreach (GameObject receiver in CurrentReceivers)
            {
                if (receiver.GetComponent<FoodReceiver>().satisfaction < satisfactionWinThreshold)
                {
                    shouldWin = false;
                    break;

                }
            }
            if (shouldWin)
            {
                GameManager.Instance.Victory();
                yield return null;
            }
            else
            {
                StartCoroutine(newOrderCoroutine(1));
            }
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
            //inputBuildingPrefab.food = foodInput.ingredient;
            Grid.GridInstance.AddObject(inputBuildingPrefab, foodInput.position, foodInput.rotation,true);
            Grid.GridInstance.TryGetObjectAt(foodInput.position, out AbstractBuilding inputer);
            ((FoodSource) inputer).food = foodInput.ingredient;
        }
        
        FoodIO nextFoodOutput = foodDict[currentFoodOrderIndex].output;
        CurrentReceivers.Add(Grid.GridInstance.AddObject(outputBuildingPrefab, nextFoodOutput.position, nextFoodOutput.rotation,true));
        Grid.GridInstance.TryGetObjectAt(nextFoodOutput.position, out AbstractBuilding receiver);
        ((FoodReceiver) receiver).requiredFood = nextFoodOutput.ingredient;


        // instanciate a foodOutput building at nextFoodOutput.position

        // add inputs at their position

        // TODO if foodOutput needs an ingredient we don't have, instantiate the ingredient(s) on the grid
        // TODO add the ingredient to the list givenIngredients
    }
}
