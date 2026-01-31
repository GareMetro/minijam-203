using UnityEditor;
using System;
using UnityEngine;

public class FoodReceiver : AbstractBuilding
{
    public BaseIngredient requiredFood;

    private float satisfaction = 0.5f;

    [SerializeField]
    //Combien on gagne par bonne bouffe
    private float satisfactionPerGoodFood;
    
    [SerializeField]
    //Combien on perd par mauvaise bouffe
    private float satisfactionPerBadFood;
    
    [SerializeField]
    //Combien on perd quand il y a rien
    private float satisfactionDecayPerTick;


    [SerializeField] Mover mover;

    public override void ProcessInputs()
    {
        base.ProcessInputs();

        if (bouffesTickActuel.Count == 0)
        {
            satisfaction -= satisfactionDecayPerTick;
        }
        foreach (Food food in bouffesTickActuel)
        {
            if (food.baseIngredient == requiredFood)
            {
                satisfaction += satisfactionPerGoodFood;
            }
            else
            {
                satisfaction -= satisfactionPerBadFood;
            }

            
            mover.MoveObject(food.transform, Grid.GridInstance.TickDuration / 2f);
        }

        satisfaction = Math.Min(satisfaction, 1f); //1 == 100% = max SATISFAIT

        if (satisfaction <= 0)
        {
            //GameManager.Instance.Defeat();
        }

    }

    public override void GiveOutput()
    {
        foreach (var item in bouffesTickActuel)
        {
            Destroy(item.gameObject);
        }
        bouffesTickActuel.Clear();
        Boing();
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.Label(transform.position + Vector3.up * Grid.GridInstance.tileSize * 2f, "sat: " + satisfaction.ToString());
    }
#endif
}
