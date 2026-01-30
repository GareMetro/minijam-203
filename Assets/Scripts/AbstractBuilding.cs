using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class AbstractBuilding : MonoBehaviour
{
    public Vector2Int Size;

    public List<Tile> TilesList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TickBuilding()
    {
        Debug.Log("Not Implemented");
    }

}
