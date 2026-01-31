using UnityEngine;

public class Tile
{
    public AbstractBuilding ContentObject;

    public Vector2Int position;


    public Tile(Vector2Int _position)
    {
        position = _position;
    }
}
