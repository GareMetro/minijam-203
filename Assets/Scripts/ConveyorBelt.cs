using UnityEngine;

public class ConveyorBelt : AbstractBuilding
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TickBuilding() //1 tick par seconde
    {
        Debug.Log("Not Implemented");
    }
}
