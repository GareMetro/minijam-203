using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum BaseIngredient
{
    
}

[System.Serializable]
public struct FoodInfo
{
    public List<BaseIngredient> neededIngredients;
    public int numberOfStepsNeeded;
    public GameObject prefab;
}

public class FoodManager : Singleton<FoodManager>
{
    [SerializeField] float firstTimeNeeded = 50;
    [SerializeField] List<FoodInfo> foodDict;
    [SerializeField] List<BaseIngredient> givenIngredients;
    [SerializeField] GameObject ingredientEntryPrefab;
    [SerializeField] GameObject foodOutputPrefab;

    public void Initialize()
    {
        StartCoroutine(newOrderCoroutine(firstTimeNeeded));
    }

    IEnumerator newOrderCoroutine(float timeNeeded)
    {
        yield return new WaitForSeconds(timeNeeded);
        AddNewFoodOrder();
        StartCoroutine(newOrderCoroutine(timeNeeded /*/ TODO 0.9f ? - 5 ?*/));
    }

    private void AddNewFoodOrder()
    {
        // TODO Instantiate a random foodOutput on the grid
        // TODO if foodOutput needs an ingredient we don't have, instantiate the ingredient(s) on the grid
        // TODO add the ingredient to the list givenIngredients
    }
}
