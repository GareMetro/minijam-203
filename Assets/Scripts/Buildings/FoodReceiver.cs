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
    public override void TickBuilding()
    {
        if (bouffeTickActuel.Count == 0)
        {
            satisfaction -= satisfactionDecayPerTick;
        }
        foreach (FoodDelivery delivery in bouffeTickActuel)
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

        if (satisfaction <= 0)
        {
            //TODO perdu mdr
        }
    }
}
