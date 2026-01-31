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

    private PlayerInput Controls;
    [SerializeField]
    public Vector2Int Size;

    [SerializeField] private float tileSize = 10f;

    private List<List<Tile>> PlayGrid;

    [SerializeField]
    public Vector2Int SelectedTile;

    [SerializeField] float keyRepeatDelay = 0.1f;

    private Dictionary<Vector2Int, Coroutine> coroutines = new();
    [SerializeField]
    public float TickDuration = 1;

    public ConveyorBelt TESTCONVEYOR1;
    public ConveyorBelt TESTCONVEYOR2;
    public ConveyorBelt TESTCONVEYOR3;
    public ConveyorBelt TESTCONVEYOR4;

    public UnityAction OnTick;
    public UnityAction NextTick;

    private GameObject cursor;

    private void MoveInteraction(InputAction.CallbackContext context, Vector2Int movement)
    {
        if (context.interaction is PressInteraction)
            MoveCursor(movement);
        if (context.interaction is HoldInteraction)
            coroutines.Add(movement, StartCoroutine(MovementRepeat(movement)));
    }

    private IEnumerator MovementRepeat(Vector2Int movement)
    {
        while(true)
        {
            MoveCursor(movement);
            yield return new WaitForSeconds(keyRepeatDelay);
        }

    }
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

        Controls = new();

        Controls.PlayerActions.MoveRight.performed += (context) => StartHold(Vector2Int.right);
        Controls.PlayerActions.MoveLeft.performed += (context) => StartHold(Vector2Int.left);
        Controls.PlayerActions.MoveUp.performed += (context) => StartHold(Vector2Int.up);
        Controls.PlayerActions.MoveDown.performed += (context) => StartHold(Vector2Int.down);

        Controls.PlayerActions.MoveRight.started += (context) => MoveCursor(Vector2Int.right);
        Controls.PlayerActions.MoveLeft.started += (context) => MoveCursor(Vector2Int.left);
        Controls.PlayerActions.MoveUp.started += (context) => MoveCursor(Vector2Int.up);
        Controls.PlayerActions.MoveDown.started += (context) => MoveCursor(Vector2Int.down);

        Controls.PlayerActions.MoveRight.canceled += (context) => StopHold(Vector2Int.right);
        Controls.PlayerActions.MoveUp.canceled += (context) => StopHold(Vector2Int.up);
        Controls.PlayerActions.MoveLeft.canceled += (context) => StopHold(Vector2Int.left);
        Controls.PlayerActions.MoveDown.canceled += (context) => StopHold(Vector2Int.down);
        
        cursor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        MeshRenderer meshRenderer = cursor.GetComponent<MeshRenderer>();
        meshRenderer.material.color = Color.white;
        
        StartCoroutine(TickBuildings());
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

    private void StartHold(Vector2Int dir)
    {
        coroutines.Add(dir, StartCoroutine(MovementRepeat(dir)));
    }

    private void StopHold(Vector2Int dir)
    {
        if (coroutines.TryGetValue(dir, out Coroutine c))
        {
            StopCoroutine(c);
            coroutines.Remove(dir);
        }
    }

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }
    private void OnDestroy()
    {
        Controls.Dispose();
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

        PlayGrid[0][0].ContentObject = TESTCONVEYOR1;
        if (TESTCONVEYOR1)
            TESTCONVEYOR1.transform.position = new Vector3(0, 1f, 0);
        
        PlayGrid[0][1].ContentObject = TESTCONVEYOR2;
        if  (TESTCONVEYOR2) 
            TESTCONVEYOR2.transform.position = new Vector3(0, 1f, 1f * tileSize);
        
        PlayGrid[1][1].ContentObject = TESTCONVEYOR3;
        if   (TESTCONVEYOR3)
            TESTCONVEYOR3.transform.position = new Vector3(1f * tileSize, 1f, 1f * tileSize);
        
        PlayGrid[1][0].ContentObject = TESTCONVEYOR4;
        if (TESTCONVEYOR4)
            TESTCONVEYOR4.transform.position = new Vector3(1f * tileSize, 1f, 0f);

        StartCoroutine(TickBuildings());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void MoveCursor(Vector2Int move)
    {
        SelectedTile += move;
        SelectedTile.Clamp(new Vector2Int(0, 0), Size - new Vector2Int(1, 1));
        
        cursor.transform.position = new Vector3(SelectedTile.x * tileSize, 3f, SelectedTile.y * tileSize);
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
