using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.IntegerTime;

public abstract class AbstractBuilding : MonoBehaviour
 {

    public List<Tile> TilesList = new();
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
            tile = _tile;
            dir = _dir;
            food = _food;
        }
    }

    public List<Food> bouffeTickActuel = new();
    public List<FoodDelivery> bouffeTickSuivant = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual protected void Start()
    {
        Grid.GridInstance.OnTick+=TickBuilding;
    }

    virtual protected void OnDestroy()
    {
        Grid.GridInstance.OnTick-=TickBuilding;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        
    }

    public virtual void TickBuilding() //1 tick par seconde
    {
        Debug.Log("Not Implemented");
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
                return new Vector2Int(-tile.y, -tile.x);
            case 3:
                return new Vector2Int(-tile.y, tile.x);
        }
        throw new System.Exception("nique");
    }

}
