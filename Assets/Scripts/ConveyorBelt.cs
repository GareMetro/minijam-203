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

    //Parametre de base, à priori ne pas toucher, par défaut les conveyor envoie toujous en haut dans leur ref local (la rotation est prise en compte pour savoir ou envoyer la bouffe)
    [SerializeField] Vector2Int direction;

    Vector2Int TileSortie; //tile vers où vont sortir 

    Grid GridInstance;

    protected override void Start() {
        base.Start();

        TileSortie = ToWorldSpace(direction);
        GridInstance = Grid.GridInstance;

        if(TESTFIRST)
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
