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

    protected override void Start() {
        base.Start();

        if(TESTFIRST)
        {
            bouffeTickSuivant.Add(new FoodDelivery(Vector2Int.zero, Vector2Int.up, TEST));
        }
    }

    public override void ProcessInputs()
    {
        base.ProcessInputs();

        if (bouffesTickActuel.Count > 1)
        {
            HandleCaca();
            return;
        }

        foreach (var item in bouffesTickActuel)
        {
            mover.MoveObject(item.transform, Grid.GridInstance.TickDuration);
        }
    }


    public override void HandleCaca()
    {
        StartCoroutine(CacaRoutine());
    }
}
