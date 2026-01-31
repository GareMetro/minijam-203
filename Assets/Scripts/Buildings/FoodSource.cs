using System;
using System.Collections;
using UnityEngine;

public class FoodSource : AbstractBuilding
{
    [SerializeField] FoodInfo food;

    [SerializeField] Transform spawnPoint;

    [SerializeField] Mover mover;

    public override void GiveOutput()
    {
        base.GiveOutput();

        foreach (var item in bouffesTickActuel)
        {
            ;
        }

        StartCoroutine(ProduceItem());
    }

    IEnumerator ProduceItem()
    {
        yield return new WaitForSeconds(Grid.GridInstance.TickDuration / 2f);

        GameObject g = Instantiate(food.prefab, spawnPoint.position, Quaternion.identity);

        mover.MoveObject(g.transform);
    }

}
