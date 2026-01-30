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

    private List<List<Tile>> PlayGrid;

    [SerializeField]
    public Vector2Int SelectedTile;

    [SerializeField] float keyRepeatDelay = 0.1f;

    private Dictionary<Vector2Int, Coroutine> coroutines = new();
    [SerializeField]
    public float TickDuration = 1;

    public UnityAction OnTick;

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
                PlayGrid[i].Add(new());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveCursor(Vector2Int move)
    {
        SelectedTile += move;
        SelectedTile.Clamp(new Vector2Int(0, 0), Size - new Vector2Int(1, 1));
    }

    Tile GetTile(int x, int y)
    {
        if (x < 0 || x >= Size.x)
            return null;
        if (y < 0 || y >= Size.x)
            return null;
        return PlayGrid[x][y];
    }

    private void OnDrawGizmos()
    {

    }
}
