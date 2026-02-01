using System.Collections;
using UnityEngine;

public class Launcher : AbstractBuilding
{
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

    protected override IEnumerator CacaRoutine()
    {
        foreach (var item in bouffesTickActuel)
        {
            mover.MoveObject(item.transform, Grid.GridInstance.TickDuration);
        }

        yield return new WaitForSeconds(Grid.GridInstance.TickDuration / 7f);

        BadBoing();

        foreach (var item in bouffesTickActuel)
        {
            Destroy(item.gameObject);
        }
        bouffesTickActuel.Clear();

        //Todo: produire caca

        GameObject caca = Instantiate(FoodManager.Instance.caca.prefab, middle.transform.position, Quaternion.identity);
        bouffesTickActuel.Add(caca.GetComponent<Food>());
        mover.MoveObject(caca.transform, Grid.GridInstance.TickDuration *  6f / 7f);
    }
}
