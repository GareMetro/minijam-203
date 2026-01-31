using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Recipe
{
    public List<BaseIngredient> ingredients;
    public List<BaseIngredient> outputs;
}

public class TransformativeBuilding : AbstractBuilding
{
    public List<Recipe> recipes;

    public Sprite Icon;

    public override void ProcessInputs()
    {
        base.ProcessInputs();

        foreach (var recipe in recipes)
        {
            if (recipe.ingredients.All(f => bouffesTickActuel.Any(fd => fd.baseIngredient == f)) && recipe.ingredients.Count == bouffesTickActuel.Count)
            {
                Debug.Log("Recipe found !");

                HandleRecipe(recipe);
                // Recipe.outputs.Count should always be equal to (or maybe lesser than) OutputTiles.Count
                //for (int i = 0; i < recipe.outputs.Count; ++i)
                //{
                //    //Tile tile = GridInstance.GetTile(ToWorldSpace(OutputTiles[i].Tile + OutputTiles[i].Direction));
                //    //Food newFood = Instantiate(recipe.outputs[i]); // TODO change position of food
                //    //if (tile.ContentObject == null)
                //    //{
                //    //    // TODO add food drop "animation"
                //    //    Destroy(bouffesTickActuel[0].gameObject); //temporary !
                //    //    return;
                //    //}
                //    //tile?.ContentObject.bouffeTickSuivant.Add(
                //    //        new FoodDelivery{
                //    //            tile = tile.position,
                //    //            dir = OutputTiles[i].Direction,
                //    //            food = newFood
                //    //            });
                //    // TODO if tile.ContentObject.bouffeTickSuivant pas vide ET est Conveyor Belt, tout retirer et remplacer par du caca
                //}
                return;
            }
        }
        // if we get here, this means the food is either invalid for recipe or we have no food at all : let's check those cases
        if (bouffesTickActuel.Count <= 0)
            return;
        else
        {
            StartCoroutine(CacaRoutine());
        }
    }


    public void HandleRecipe(Recipe recipe)
    {
        StartCoroutine(RecipeRoutine(recipe));
    }

    IEnumerator RecipeRoutine(Recipe recipe)
    {
        foreach (var item in bouffesTickActuel)
        {
            mover.MoveObject(item.transform, Grid.GridInstance.TickDuration);
        }

        yield return new WaitForSeconds(Grid.GridInstance.TickDuration / 2f);

        Boing();

        foreach (var item in bouffesTickActuel)
        {
            Destroy(item.gameObject);
        }
        bouffesTickActuel.Clear();

        for (int i = 0; i < recipe.outputs.Count; ++i)
        {
            GameObject Output = Instantiate(recipe.outputs[i].prefab, middle.transform.position, Quaternion.identity);
            bouffesTickActuel.Add(Output.GetComponent<Food>());
            mover.MoveObject(Output.transform, Grid.GridInstance.TickDuration / 2f);
        }

    }

    public override void HandleCaca()
    {
        StartCoroutine(CacaRoutine());
    }
}
