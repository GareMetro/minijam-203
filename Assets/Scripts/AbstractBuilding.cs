using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class AbstractBuilding : MonoBehaviour
 {

    public List<Tile> TilesList;
    public Tile Position;

    //Sert au plaçeur de batiment pour savoir où vont être les tiles de ce bâtiment
    public List<Vector2Int> LocalTiles;

    /*
    0 = droite
    1 = haut
    2 = gauche
    3 = droite
    */
    public int rotation;
    public List<Food> bouffeTickActuel;
    public List<Food> bouffeTickSuivant;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Grid.GridInstance.OnTick+=TickBuilding;
    }

    void OnDestroy()
    {
        Grid.GridInstance.OnTick-=TickBuilding;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TickBuilding() //1 tick par seconde
    {
        Debug.Log("Not Implemented");
    }

}
