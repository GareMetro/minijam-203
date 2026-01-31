using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FoodManager foodManager;
    public HazardManager hazardManager;
    //Grid reference
    public AudioManager audioManager;

    private void Awake() 
    {
        audioManager = AudioManager.Instance;
        foodManager.Initialize();
        hazardManager.Initialize();
    }
}
