using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ConveyorBelt : AbstractBuilding
{
    [SerializeField] Food TEST;

    [SerializeField] Mover mover;

    protected override void Start() {
        base.Start();

        bouffeTickSuivant.Add(new FoodDelivery(Vector2Int.zero, Vector2Int.up, TEST));
    }

    public override void TickBuilding() //1 tick par seconde
    {
        

        foreach (var item in bouffeTickSuivant)
        {
            if(item.tile == Vector2Int.zero)
            {
                mover.MoveObject(item.food.transform);
            }
        }
        bouffeTickSuivant.Clear();
    }
}
