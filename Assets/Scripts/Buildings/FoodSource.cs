using System;
using System.Collections;
using UnityEngine;

public class FoodSource : AbstractBuilding
{
    [SerializeField] public BaseIngredient food;

    [SerializeField] Transform spawnPoint;

    public override void ProcessInputs()
    {
        foreach (var item in bouffeTickSuivant)
        {
            Destroy(item.food.gameObject);
        }
        bouffeTickSuivant.Clear();

        StartCoroutine(ProduceItem());
    }

    public override void GiveOutput()
    {
        base.GiveOutput();
    }


    IEnumerator ProduceItem()
    {
        yield return new WaitForSeconds(Grid.GridInstance.TickDuration / 2f);

        GameObject g = Instantiate(food.prefab, spawnPoint.position, Quaternion.identity);
        g.transform.localScale *= Grid.GridInstance.tileSize * 0.65f;

        Boing();

        bouffesTickActuel.Add(g.GetComponent<Food>());

        mover.MoveObject(g.transform, Grid.GridInstance.TickDuration / 2f);
    }

}
