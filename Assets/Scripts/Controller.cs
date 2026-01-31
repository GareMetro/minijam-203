using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{

    [SerializeField] float keyRepeatDelay = 0.1f;

    [SerializeField] public Vector2Int SelectedTile;
    [HideInInspector] public int SelectedBati = 0;
    [HideInInspector] public int RotationBati = 0;

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

        Controls.PlayerActions.Place.started += (context) => PlaceBuilding();
        Controls.PlayerActions.Delete.started += (context) => DeleteBuilding();
        Controls.PlayerActions.Rotate.started += (context) => RotateSelection();

        for (int i = 0; i < batiInfos.batiInfos.Count ; i++)
        {
            BatiInfo batiInfo  = batiInfos.batiInfos[i];
            var index = i;
            batiInfo.inputAction.action.started += (context) => ChangedSelected(index);

            batiInfo.inputAction.action.Enable();
        }

        cursor = new();
        ChangedSelected(0);
    }

    public delegate void SelectionEvent(int i);
    SelectionEvent OnSelectedChange;

    private void ChangedSelected(int i)
    {
        OnSelectedChange?.Invoke(i);
        Toolbar.SelectTool(i);
        SelectedBati = i;
        UpdateCursorPreview(i);
    }

    private void RotateSelection()
    {
        RotationBati = (RotationBati - 1) % 4;
        UpdateCursorPreview(SelectedBati);
    }

    private void PlaceBuilding()
    {
        if (batiInfos.batiInfos[SelectedBati].batiPrefab)
        {
            Grid.GridInstance.AddObject(batiInfos.batiInfos[SelectedBati].batiPrefab, SelectedTile, RotationBati);
        }
    }
    private void DeleteBuilding()
    {
        Grid.GridInstance.RemoveObject(SelectedTile);
    }

    private void UpdateCursorPreview(int i)
    {
        if (cursorHolo)
        {
            Destroy(cursorHolo);
        }
        
        cursorHolo = Instantiate(batiInfos.batiInfos[i].holoPrefab, cursor.transform.position, Quaternion.identity, cursor.transform);
        cursorHolo.transform.localScale = (Grid.GridInstance.tileSize / 2) * 0.95f * Vector3.one;
        cursorHolo.transform.rotation *= Quaternion.AngleAxis(-90f * RotationBati, Vector3.up);
    }

    void MoveCursor(Vector2Int move)
    {
        SelectedTile += move;
        SelectedTile.Clamp(new Vector2Int(0, 0), Grid.GridInstance.Size - new Vector2Int(1, 1));
        
        cursor.transform.position = new Vector3(SelectedTile.x * Grid.GridInstance.tileSize, 25f, SelectedTile.y * Grid.GridInstance.tileSize);
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
