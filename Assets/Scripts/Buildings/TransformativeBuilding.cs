using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Recipe
{
    public List<Food> ingredients;
    public List<Food> outputs;
}

public class TransformativeBuilding : AbstractBuilding
{
    public List<Recipe> recipes;

    public Sprite Icon;

    public override void GiveOutput()
    {
        foreach (var recipe in recipes)
        {
            if (recipe.ingredients.All(f => bouffesTickActuel.Any(fd => fd == f)) && recipe.ingredients.Count == bouffesTickActuel.Count)
            {
                Debug.Log("Recipe found !");
                // Recipe.outputs.Count should always be equal to (or maybe lesser than) OutputTiles.Count
                for (int i = 0; i < recipe.outputs.Count; ++i)
                {
                    Tile tile = GridInstance.GetTile(ToWorldSpace(OutputTiles[i].Tile + OutputTiles[i].Direction));
                    Food newFood = Instantiate(recipe.outputs[i]); // TODO change position of food
                    if (tile.ContentObject == null)
                    {
                        // TODO add food drop "animation"
                        Destroy(bouffesTickActuel[0].gameObject); //temporary !
                        return;
                    }
                    tile?.ContentObject.bouffeTickSuivant.Add(
                            new FoodDelivery{
                                tile = tile.position,
                                dir = OutputTiles[i].Direction,
                                food = newFood
                                });
                    // TODO if tile.ContentObject.bouffeTickSuivant pas vide ET est Conveyor Belt, tout retirer et remplacer par du caca
                }
            }
            return;
        }
        // if we get here, this means the food is either invalid for recipe or we have no food at all : let's check those cases
        if (bouffesTickActuel.Count <= 0)
            return;
        else
        {
            // TODO instantiate caca, push caca to output index 0
        }
    }
}
