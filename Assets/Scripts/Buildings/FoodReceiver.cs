using UnityEditor;
using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FoodReceiver : AbstractBuilding
{
    private BaseIngredient _requiredFood;
    public BaseIngredient requiredFood
    {
        get { return _requiredFood;}
        set
        {
            _requiredFood = value;
            requiredFoodIcon.sprite = value.icon;
        }
    }

    public float satisfaction = 0.5f;

    [SerializeField] private RectTransform progressBar;

    [SerializeField]
    //Combien on gagne par bonne bouffe
    private float satisfactionPerGoodFood;
    
    [SerializeField]
    //Combien on perd par mauvaise bouffe
    private float satisfactionPerBadFood;
    
    [SerializeField]
    //Combien on perd quand il y a rien
    private float satisfactionDecayPerTick;

    [SerializeField] private Image requiredFoodIcon;

    protected override void Start()
    {
        base.Start();
        requiredFoodIcon.transform.Rotate(Vector3.forward, 90 * (2 - Rotation));
    }
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

        satisfaction = Math.Clamp(satisfaction, 0f, 1f); //1 == 100% = max SATISFAIT

        progressBar.anchorMax = new Vector2(satisfaction, 1);
    }

    public override void GiveOutput()
    {
        if (bouffesTickActuel.Count != 0)
        {
            //Jouer le son du bâtiment
            if (TryGetComponent<AudioPlayer>(out AudioPlayer player))
            {
                int soundClips = player.audioClips.Count;
                player.PlaySound(Random.Range(0, soundClips));
            }
        }
        
        foreach (var item in bouffesTickActuel)
        {
            Destroy(item.gameObject);
            Boing();
        }
        bouffesTickActuel.Clear();
        
    }
    
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if(Grid.GridInstance)
            Handles.Label(transform.position + Vector3.up * Grid.GridInstance.tileSize * 2f, "sat: " + satisfaction.ToString());
    }
#endif
}
