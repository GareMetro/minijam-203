using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    [SerializeField] float keyRepeatDelay = 0.1f;

    [SerializeField] public Vector2Int SelectedTile;
    [HideInInspector] public int SelectedBati = 0;

    [SerializeField] private Toolbar Toolbar;
    private GameObject cursor;
    private GameObject cursorHolo;

    BatiInfos batiInfos;

    private PlayerInput Controls;

    private Dictionary<Vector2Int, Coroutine> coroutines = new();
    
    void Start()
    {
        batiInfos = Grid.GridInstance.batiInfos;

        Controls = new();
        Controls.Enable();

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

        for (int i = 0; i < batiInfos.batiInfos.Count ; i++)
        {
            BatiInfo batiInfo  = batiInfos.batiInfos[i];
            var index = i;
            batiInfo.inputAction.action.started += (context) => ChangedSelected(index);

            batiInfo.inputAction.action.Enable();
        }

        cursor = new();
    }

    public delegate void SelectionEvent(int i);
    SelectionEvent OnSelectedChange;

    private void ChangedSelected(int i)
    {
        OnSelectedChange?.Invoke(i);
        Toolbar.SelectTool(i);
        UpdateCursorPreview(i);
    }

    private void UpdateCursorPreview(int i)
    {
        if (cursorHolo)
        {
            Destroy(cursorHolo);
        }
        
        cursorHolo = Instantiate(batiInfos.batiInfos[i].holoPrefab, cursor.transform.position, Quaternion.identity, cursor.transform);
    }

    void MoveCursor(Vector2Int move)
    {
        SelectedTile += move;
        SelectedTile.Clamp(new Vector2Int(0, 0), Grid.GridInstance.Size - new Vector2Int(1, 1));
        
        cursor.transform.position = new Vector3(SelectedTile.x * Grid.GridInstance.tileSize, 3f, SelectedTile.y * Grid.GridInstance.tileSize);
    }

    private IEnumerator MovementRepeat(Vector2Int movement)
    {
        while(true)
        {
            MoveCursor(movement);
            yield return new WaitForSeconds(keyRepeatDelay);
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

    private void OnDestroy()
    {
        Controls.Dispose();
    }
}
