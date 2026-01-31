using System;
using UnityEngine;

public class FoodReceiver : AbstractBuilding
{
    public Food requiredFood;

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
    public override void ProcessInputs()
    {
        if (bouffeTickSuivant.Count == 0)
        {
            satisfaction -= satisfactionDecayPerTick;
        }
        foreach (FoodDelivery delivery in bouffeTickSuivant)
        {
            if (delivery.food == requiredFood)
            {
                satisfaction += satisfactionPerGoodFood;
            }
            else
            {
                satisfaction -= satisfactionPerBadFood;
            }
        }

        satisfaction = Math.Min(satisfaction, 1f); //1 == 100% = max SATISFAIT

        if (satisfaction <= 0)
        {
            GameManager.Instance.Defeat();
        }
    }

    public override void GiveOutput()
    {
        foreach (var item in bouffesTickActuel)
        {
            Destroy(item.gameObject);
        }
        bouffesTickActuel.Clear();
    }
}
