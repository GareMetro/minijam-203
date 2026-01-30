using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Grid : MonoBehaviour
{
    private PlayerInput Controls;
    [SerializeField]
    public Vector2Int Size;

    private List<List<Tile>> PlayGrid;

    [SerializeField]
    public Vector2Int SelectedTile;

    private Dictionary<Vector2Int, Coroutine> coroutines = new();

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
            yield return new WaitForSeconds(0.1f);
        }

    }

    private void Awake()
    {
        Controls = new();


        Controls.PlayerActions.MoveRight.performed += (context) => MoveInteraction(context, Vector2Int.right);
        Controls.PlayerActions.MoveLeft.performed += (context) => MoveInteraction(context, Vector2Int.left);
        Controls.PlayerActions.MoveUp.performed += (context) => MoveInteraction(context, Vector2Int.up);
        Controls.PlayerActions.MoveDown.performed += (context) => MoveInteraction(context, Vector2Int.down);

        Controls.PlayerActions.MoveRight.canceled += (context) =>
        {
            if (coroutines[Vector2Int.right] != null)
            {
                StopCoroutine(coroutines[Vector2Int.right]);
                coroutines.Remove(Vector2Int.right);
            }
        };

        Controls.PlayerActions.MoveLeft.canceled += (context) =>
        {
            if (coroutines[Vector2Int.left] != null)
            {
                StopCoroutine(coroutines[Vector2Int.left]);
                coroutines.Remove(Vector2Int.left);
            }
        };

        Controls.PlayerActions.MoveUp.canceled += (context) =>
        {
            if (coroutines[Vector2Int.up] != null)
            {
                StopCoroutine(coroutines[Vector2Int.up]);
                coroutines.Remove(Vector2Int.up);
            }
        };

        Controls.PlayerActions.MoveDown.canceled += (context) =>
        {
            if (coroutines[Vector2Int.down] != null)
            {
                StopCoroutine(coroutines[Vector2Int.down]);
                coroutines.Remove(Vector2Int.down);
            }
        };
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
