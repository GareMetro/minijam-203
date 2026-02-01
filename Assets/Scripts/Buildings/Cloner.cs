using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Cloner : AbstractBuilding
{

    [SerializeField] Mover originalMover;
    [SerializeField] Mover clonedMoverDroite;
    [SerializeField] Mover clonedMoverGauche;

    public override void ProcessInputs()
    {
        base.ProcessInputs();

        for (int i = bouffesTickActuel.Count - 1; i > 0; --i)
        {
            Destroy(bouffesTickActuel[i].gameObject);
        }

        StartCoroutine(ClonerRoutine());
    }

    public override void GiveOutput()
    {
        base.GiveOutput();
    }

    IEnumerator ClonerRoutine()
    {
        originalMover.MoveObject(bouffesTickActuel.First().transform, Grid.GridInstance.TickDuration);
        yield return new WaitForSeconds(Grid.GridInstance.TickDuration / 2.0f);

        Boing();

        GameObject obj1 = Instantiate(bouffesTickActuel.First().gameObject);
        bouffesTickActuel.Add(obj1.GetComponent<Food>());
        GameObject obj2 = Instantiate(bouffesTickActuel.First().gameObject);
        bouffesTickActuel.Add(obj2.GetComponent<Food>());
        
        clonedMoverDroite.MoveObject(obj1.transform, Grid.GridInstance.TickDuration / 2.0f);
        clonedMoverGauche.MoveObject(obj2.transform, Grid.GridInstance.TickDuration / 2.0f);
    }
}
