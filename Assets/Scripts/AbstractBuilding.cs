using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.IntegerTime;
using System.ComponentModel;

/*

Phase 1 du tick : copie toutes les bouffes de bouffeTickSuivant vers bouffeTickActuel
Phase 2 du tick : gère tout ce qu'il y a dans sa liste bouffeTickActuel et les envoient vers les bouffeTickSuivant de ses voisins

Uniquement le conveyor a été réalisé, il ne gère pas plusieurs bouffe en entrée (il devrait)
Les bâtiments ne gèrent pas si ils sont en train d'envoyer de la bouffe vers un endroit invalide (sur le mur d'un four, sur l'avant d'un conveyor par exemple)



*/
public class AbstractBuilding : MonoBehaviour
 {

    [HideInInspector] public List<Tile> TilesList = new();
    public Vector2Int Position = Vector2Int.zero;
    public int Rotation = 0;// sens trigo

    //Sert au plaçeur de batiment pour savoir où vont être les tiles de ce bâtiment
    public List<Vector2Int> LocalTiles = new();

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

    public List<FoodDelivery> bouffeTickActuel = new();
    public List<FoodDelivery> bouffeTickSuivant = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual protected void Start()
    {
        Grid.GridInstance.OnTick+=TickBuilding;
        Grid.GridInstance.NextTick+=NextTick;
    }

    virtual protected void OnDestroy()
    {
        Grid.GridInstance.OnTick-=TickBuilding;
        Grid.GridInstance.NextTick-=NextTick;

    }

    // Update is called once per frame
    virtual protected void Update()
    {

    }

    public virtual void TickBuilding() //1 tick par seconde
    {
        Debug.Log("Not Implemented");
    }

     public virtual void NextTick() //1 tick par seconde
    {
        bouffeTickActuel.Clear();
        foreach(var item in bouffeTickSuivant)
        {
            bouffeTickActuel.Add(item);
        }
        bouffeTickSuivant.Clear();
    }

    public void AddDelivery(Vector2Int from, Vector2Int to, Food food)
    {
        to = ToLocalSpace(to);
        from = ToLocalSpace(from);
        bouffeTickSuivant.Add(new FoodDelivery(to, to - from, food));
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

}
