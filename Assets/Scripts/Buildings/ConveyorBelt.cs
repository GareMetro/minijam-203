using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


/*
Code du conveyor, plutot rudimentaire, ne gère pas encore plusieurs bouffes à la fois (pour faire du caca)
prend la première bouffe et l'envoie sur la tile d'après (TileSortie)

Pour placer auto un conveyor, il suffit de lui rentrer Position (vecteur2DInt de la grid) et rotation (0 vers le haut puis sens trigo)


*/
public class ConveyorBelt : AbstractBuilding
{
    [SerializeField] Food TEST;
    [SerializeField] bool TESTFIRST;

    [SerializeField] Mover mover;

    Vector2Int TileSortie; //tile vers où vont sortir 

    protected override void Start() {
        base.Start();

        TileSortie = ToWorldSpace(OutputTiles[0].Direction);

        if(TESTFIRST)
        {
            bouffeTickSuivant.Add(new FoodDelivery(Vector2Int.zero, Vector2Int.up, TEST));
        }
    }

    public override void TickBuilding() //1 tick par seconde
    {
        if(bouffeTickActuel.Count > 0)
        {
            Tile tile = GridInstance.GetTile(TileSortie);
            if (tile.ContentObject == null || tile == null)
            {
                // TODO add food drop "animation"
                Destroy(bouffeTickActuel[0].food.gameObject); //temporary !
                return;
            }
            // else we ask the mover to do things
            foreach (var item in bouffeTickActuel)
            {
                //if(item.tile == Vector2Int.zero)
                {
                    mover.MoveObject(item.food.transform);
                }
            }
            // and pass the food data-wise
            tile?.ContentObject.bouffeTickSuivant.Add(bouffeTickActuel[0]);
            // TODO if tile.ContentObject.bouffeTickSuivant pas vide ET est Conveyor Belt, tout retirer et remplacer par du caca
        }
    }
}
