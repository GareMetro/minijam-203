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
    [SerializeField] Transform middle;


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

        foreach (var item in bouffesTickActuel)
        {
            mover.MoveObject(item.transform, Grid.GridInstance.TickDuration);
        }
    }


    public override void HandleCaca()
    {
        StartCoroutine(CacaRoutine());
    }

    IEnumerator CacaRoutine()
    {
        foreach (var item in bouffesTickActuel)
        {
            mover.MoveObject(item.transform, Grid.GridInstance.TickDuration);
        }

        yield return new WaitForSeconds(Grid.GridInstance.TickDuration / 2f);


        foreach (var item in bouffesTickActuel)
        {
            Destroy(item.gameObject);
        }
        bouffesTickActuel.Clear();

        //Todo: produire caca

        GameObject caca = Instantiate(FoodManager.Instance.caca.prefab, middle.transform.position, Quaternion.identity);
        bouffesTickActuel.Add(caca.GetComponent<Food>());
        mover.MoveObject(caca.transform, Grid.GridInstance.TickDuration / 2f);

    }

}
