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

    private List<List<Tile>> PlayGrid;

    [SerializeField]
    public Vector2Int SelectedTile;

    private Dictionary<Vector2Int, Coroutine> coroutines = new();
    [SerializeField]
    public float TickDuration = 1;

    [SerializeField]
    public BatiInfos batiInfos;

    public List<AbstractBuilding> TESTBUILDINGS;
    public List<Vector2Int> TESTBUILDINGSPOS;

    public UnityAction OnTick;
    public UnityAction NextTick;

    private IEnumerator TickBuildings()
    {
        while(true)
        {
            NextTick?.Invoke();
            OnTick?.Invoke();
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
                GameObject newPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                newPlane.transform.position = new Vector3(i * tileSize, 0f, j * tileSize);
                newPlane.transform.localScale = (tileSize/10f) * 0.95f * Vector3.one;
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
            TESTBUILDINGS[i].transform.position = new Vector3(TESTBUILDINGSPOS[i].x, 1f, TESTBUILDINGSPOS[i].y);
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

    private void AddObject(AbstractBuilding building, Vector2Int pos, int rot)
    {
        building.Position = pos;
        building.Rotation = rot;
        GameObject added = Instantiate(building.gameObject, new Vector3((pos.x + 0.5f) * tileSize, 0f, pos.y), Quaternion.AngleAxis(rot * 90f, Vector3.up));
        
        foreach (var item in building.LocalTiles)
        {
            Vector2Int tilePos = building.ToWorldSpace(item);
            AbstractBuilding addedAbstractBuilding = added.GetComponent<AbstractBuilding>();
            PlayGrid[tilePos.x][tilePos.y].ContentObject = addedAbstractBuilding;
            addedAbstractBuilding.TilesList.Add(PlayGrid[tilePos.x][tilePos.y]);
        }
    }
}
