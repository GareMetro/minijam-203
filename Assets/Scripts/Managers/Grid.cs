using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{
    public static Grid GridInstance;

    [SerializeField]
    public Vector2Int Size;

    public float tileSize = 10f;

    public Material tileEvenMat;
    public Material tileOddMat;

    private List<List<Tile>> PlayGrid;

    [SerializeField]
    public Vector2Int SelectedTile;

    private Dictionary<Vector2Int, Coroutine> coroutines = new();
    [SerializeField]
    public float TickDuration = 1;

    public List<AbstractBuilding> TESTBUILDINGS;
    public List<Vector2Int> TESTBUILDINGSPOS;

    public UnityAction GiveOutput;
    public UnityAction ProcessInputs;

    private IEnumerator TickBuildings()
    {
        while(true)
        {
            GiveOutput?.Invoke();
            ProcessInputs?.Invoke();
            yield return new WaitForSeconds(TickDuration);
        }
    }


    private void Awake()
    {
        if (GridInstance)
        {
            Destroy(gameObject);
            return;
        }
        
        GridInstance = this;
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int i = 0; i < Size.x; ++i)
        {
            for (int j = 0; j < Size.y; ++j)
            {
                GameObject newTile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newTile.transform.localScale = tileSize * 0.95f * (Vector3.one - Vector3.up * 0.75f);
                newTile.transform.position = new Vector3(i * tileSize, -newTile.transform.localScale.y/2f, j * tileSize);
                newTile.GetComponent<Renderer>().material = (i + j) % 2 == 0 ? tileEvenMat : tileOddMat;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayGrid = new();
        for (int i = 0; i < Size.x; ++i)
        {
            PlayGrid.Add(new());
            for (int j = 0; j < Size.y; ++j)
            {
                PlayGrid[i].Add(new(new(i,j)));
            }
        }

        for (int i = 0; i < TESTBUILDINGS.Count; ++i)
        {
            foreach (var tile in TESTBUILDINGS[i].LocalTiles)
            {
                Vector2Int locPos = TESTBUILDINGS[i].ToWorldSpace(tile);
                //Debug.Log(TESTBUILDINGS[i].name + " : " + TESTBUILDINGS[i].ToWorldSpace(tile));
                PlayGrid[locPos.x][locPos.y].ContentObject = TESTBUILDINGS[i];
            }
            //TESTBUILDINGS[i].transform.position = new Vector3(TESTBUILDINGS[i].Position.x, 1f, TESTBUILDINGS[i].Position.y);
        }

        StartCoroutine(TickBuildings());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Tile GetTile(Vector2Int position)
    {
        if (position.x < 0 || position.x >= Size.x)
            return null;
        if (position.y < 0 || position.y >= Size.x)
            return null;
        return PlayGrid[position.x][position.y];
    }

    public bool TryGetObjectAt(Vector2Int position, out AbstractBuilding outBuilding)
    {
        outBuilding = null;
        Tile tile = GetTile(position);
        if(tile == null || tile.ContentObject == null)
            return false;

        outBuilding = tile.ContentObject;
        return true;

    }

    public GameObject AddObject(AbstractBuilding building, Vector2Int pos, int rot, bool force = false)
    {
        //Si la tile est occup�e et qu'on ne force pas le placement
        if (GetTile(pos).ContentObject && !force) 
        {
            return null;
        }
        //Si elle est occupée et qu'on force le placement, on supprime ce qu'il y avait dessus
        if(force && GetTile(pos).ContentObject)
        {
            RemoveObject(pos);
        }

        //on cr�� une instance du type s�l�ction�
        GameObject added = Instantiate(building.gameObject, new Vector3((pos.x) * tileSize, 0f, pos.y * tileSize), Quaternion.AngleAxis(- rot * 90f, Vector3.up));

        AbstractBuilding Addedbuilding = added.GetComponent<AbstractBuilding>();
        //Rotation et position dans la grille du batiment
        Addedbuilding.Position = pos;
        Addedbuilding.Rotation = rot;
        added.transform.localScale = (tileSize/2) * 0.95f * Vector3.one;

        foreach (var item in building.LocalTiles)
        {
            Vector2Int tilePos = Addedbuilding.ToWorldSpace(item);
            PlayGrid[tilePos.x][tilePos.y].ContentObject = Addedbuilding;
            Addedbuilding.TilesList.Add(PlayGrid[tilePos.x][tilePos.y]);
        }

        return added;
    }

    public void RemoveObject(Vector2Int pos)
    {
        AbstractBuilding Building = PlayGrid[pos.x][pos.y].ContentObject;
        if (Building)
        {
            List<Tile> BuildingTiles = Building.TilesList;

            foreach (var item in BuildingTiles)
            {
                item.ContentObject = null;
            }

            Destroy(Building.gameObject);
        }

    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < Size.x; ++i)
        {
            for (int j = 0; j < Size.y; ++j)
            {
                //GameObject newPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                //newPlane.transform.position = new Vector3(i * tileSize, 0f, j * tileSize);
                //newPlane.transform.localScale = (tileSize / 10f) * 0.95f * Vector3.one;
                Gizmos.DrawCube(new Vector3(i * tileSize, 0f, j * tileSize), new Vector3(tileSize, (tileSize / 10f) * 0.95f, tileSize));
            }
        }
    }
}
