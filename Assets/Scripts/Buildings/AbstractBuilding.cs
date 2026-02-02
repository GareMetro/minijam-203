using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.IntegerTime;
using System;
using System.Reflection;
using DG.Tweening;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using System.Linq;

// Permet de gérer les cas d'envoi invalides
[System.Serializable]
public struct InOutInfo
{
    public Vector2Int Tile;
    public Vector2Int Direction;
}

/*

Phase 1 du tick : copie toutes les bouffes de bouffeTickSuivant vers bouffeTickActuel
Phase 2 du tick : gère tout ce qu'il y a dans sa liste bouffeTickActuel et l'envoie vers les bouffeTickSuivant de ses voisins

Uniquement le conveyor a été réalisé, il ne gère pas plusieurs bouffe en entrée (il devrait)
Les bâtiments ne gèrent pas si ils sont en train d'envoyer de la bouffe vers un endroit invalide (sur le mur d'un four, sur l'avant d'un conveyor par exemple)



*/

public abstract class AbstractBuilding : MonoBehaviour
 {
    public BatiInfo batiInfo;

    public List<Tile> TilesList = new();
    
    [Header("Instance Current State")]
    public Vector2Int Position = Vector2Int.zero;
    public int Rotation = 0;// sens trigo

    
    [Header("Building Configuration")]
    [Tooltip("Sert au placeur de bâtiment pour savoir où vont être les tiles de ce bâtiment")]
    public List<Vector2Int> LocalTiles = new();
    
    [Tooltip("Sert pour savoir quelles tiles du bâtiment peuvent recevoir de la bouffe, et dans quelle direction")]
    public List<InOutInfo> InputTiles = new();
    
    [Tooltip("Sert pour savoir quelles tiles du bâtiment peuvent expulser de la bouffe, et dans quelle direction")]
    public List<InOutInfo> OutputTiles = new();

    [Header("Assignable")]
    [SerializeField] protected Mover mover;
    [SerializeField] protected Transform middle;
    [SerializeField] protected BuildingAnimator buildingAnimator;

    protected AudioPlayer audioPlayer;

    [Serializable]
    public struct FoodDelivery
    {
        public Vector2Int tile;
        public Vector2Int dir;
        public Food food;

        public FoodDelivery(Vector2Int _tile, Vector2Int _dir, Food _food)
        {
            tile = _tile; //tile sur laquelle la bouffe va être
            dir = _dir;  //direction depuis la quelle elle est arrivée sur la tile
            food = _food; //ref vers la bouffe (modele + FoodInfo)
        }
    }

    public List<Food> bouffesTickActuel = new();
    public List<FoodDelivery> bouffeTickSuivant = new();

    protected Grid GridInstance {get => GetGridInstance(); set => _gridInstance = value;}
    protected Grid _gridInstance;

    protected Grid GetGridInstance()
    {
        if (_gridInstance == null)
            _gridInstance = Grid.GridInstance;
        return _gridInstance;
    }

    private void Awake() 
    {
        audioPlayer = GetComponent<AudioPlayer>();
        GridInstance = Grid.GridInstance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual protected void Start()
    {
        Grid.GridInstance.GiveOutput+=GiveOutput;
        Grid.GridInstance.ProcessInputs+=ProcessInputs;
    }

    virtual protected void OnDestroy()
    {
        Grid.GridInstance.GiveOutput-=GiveOutput;
        Grid.GridInstance.ProcessInputs-=ProcessInputs;

        foreach (var item in bouffesTickActuel)
        {
            if(item)
                Destroy(item.gameObject);
        }

        foreach (var item in bouffeTickSuivant)
        {
            if(item.food)
                Destroy(item.food.gameObject);
        }
    }

    // Update is called once per frame
    virtual protected void Update()
    {

    }

    public virtual void GiveOutput() //1 tick par seconde
    {
        
        // donner ce qu'il y a besoins au autres

        for (int i = 0; i < bouffesTickActuel.Count && i < OutputTiles.Count; ++i)
        {
            Vector2Int output = ToWorldSpace(OutputTiles[i].Tile);
            if(Grid.GridInstance.TryGetObjectAt(output, out AbstractBuilding building))
            {
                building.AddDelivery(output, DirToWorldSpace(OutputTiles[i].Direction), bouffesTickActuel[i]);
            }
            else
            {
                Vector2Int dir = DirToWorldSpace(OutputTiles[i].Direction);
                bouffesTickActuel[i].DisapearFalling(new Vector3(dir.x, 0f, dir.y));
            }
        }

        bouffesTickActuel.Clear();
    }

    public virtual void ProcessInputs() //1 tick par seconde
    {
        //récupérer ce qu'on nous donne ce tick

        foreach (var item in bouffeTickSuivant)
        {
            if(InputTiles.Any((t) => t.Tile == ToLocalSpace(item.tile) && t.Direction == DirToLocalSpace(item.dir)))
            {
                bouffesTickActuel.Add(item.food);
            }
            else
            {
                //todo animation?
                item.food.Disapear();
            }
        }

        bouffeTickSuivant.Clear();
    }

    public virtual void HandleCaca()
    {
        foreach (var item in bouffeTickSuivant)
        {
            Destroy(item.food.gameObject);
        }
    }

    public void AddDelivery(Vector2Int to, Vector2Int dir, Food food)
    {
        bouffeTickSuivant.Add(new FoodDelivery(to, dir, food));
    }

    public Vector2Int ToLocalSpace(Vector2Int tile)
    {
        tile -= Position;

        switch(Rotation % 4)
        {
            case 0:
                return tile;
            case 1:
                return new Vector2Int(tile.y, -tile.x);
            case 2:
                return new Vector2Int(-tile.x, -tile.y);
            case 3:
                return new Vector2Int(-tile.y, tile.x);
        }
        throw new System.Exception("nique");
    }

    public Vector2Int ToWorldSpace(Vector2Int tile)
    {
        switch(Rotation % 4)
        {
            case 1:
                tile = new Vector2Int(-tile.y, tile.x); break;
            case 2:
                tile = new Vector2Int(-tile.x, -tile.y); break;
            case 3:
                tile = new Vector2Int(tile.y, -tile.x); break;
        }

        return tile + Position;
    }

    public Vector2Int DirToLocalSpace(Vector2Int tile)
    {

        switch(Rotation % 4)
        {
            case 0:
                return tile;
            case 1:
                return new Vector2Int(tile.y, -tile.x);
            case 2:
                return new Vector2Int(-tile.x, -tile.y);
            case 3:
                return new Vector2Int(-tile.y, tile.x);
        }
        throw new System.Exception("nique");
    }

    public Vector2Int DirToWorldSpace(Vector2Int tile)
    {
        switch(Rotation % 4)
        {
            case 1:
                tile = new Vector2Int(-tile.y, tile.x); break;
            case 2:
                tile = new Vector2Int(-tile.x, -tile.y); break;
            case 3:
                tile = new Vector2Int(tile.y, -tile.x); break;
        }
        return tile;
    }

    protected virtual IEnumerator CacaRoutine()
    {
        foreach (var item in bouffesTickActuel)
        {
            mover.MoveObject(item.transform, Grid.GridInstance.TickDuration);
        }

        yield return new WaitForSeconds(Grid.GridInstance.TickDuration / 2f);

        BadBoing();

        foreach (var item in bouffesTickActuel)
        {
            Destroy(item.gameObject);
        }
        bouffesTickActuel.Clear();

        //Todo: produire caca

        GameObject caca = Instantiate(FoodManager.Instance.caca.prefab, middle.transform.position, Quaternion.identity);
        caca.transform.localScale *= Grid.GridInstance.tileSize * 0.8f;
        bouffesTickActuel.Add(caca.GetComponent<Food>());
        mover.MoveObject(caca.transform, Grid.GridInstance.TickDuration / 2f);
    }

    public void Boing()
    {
        //Jouer le son du bâtiment
        if (audioPlayer)
        {
            int soundClips = audioPlayer.audioClips.Count;
            audioPlayer.PlaySound(Random.Range(0, soundClips));
        }

        transform.DOPunchScale(transform.localScale * 0.2f, 0.3f, 6, 1);
    }

    public void BadBoing()
    {
        transform.DOShakeRotation(0.3f,15f);
    }

}
