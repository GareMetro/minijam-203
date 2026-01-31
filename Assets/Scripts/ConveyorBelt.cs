using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ConveyorBelt : AbstractBuilding
{
    [SerializeField] Food TEST;
    [SerializeField] bool first;

    [SerializeField] Mover mover;

    [SerializeField] Vector2Int direction;

    Vector2Int TileSortie; //tile vers où vont sortir 

    Grid GridInstance;

    protected override void Start() {
        base.Start();

        TileSortie = ToWorldSpace(direction);
        GridInstance = Grid.GridInstance;

        if(first)
        {
            bouffeTickSuivant.Add(new FoodDelivery(Vector2Int.zero, Vector2Int.up, TEST));
        }
    }

    public override void TickBuilding() //1 tick par seconde
    {
        foreach (var item in bouffeTickActuel)
        {
            if(item.tile == Vector2Int.zero)
            {
                mover.MoveObject(item.food.transform);
            }
        }

        if(bouffeTickActuel.Count > 0)
        {
            Tile tile = GridInstance.GetTile(TileSortie);
            tile?.ContentObject.bouffeTickSuivant.Add(bouffeTickActuel[0]);
        }
    }
}
