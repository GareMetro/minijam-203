using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Toolbar : MonoBehaviour
{
    [SerializeField] private GameObject SelectionPrefab;

    [SerializeField] List<RectTransform> ToolCells;
    
    private RectTransform SelectionTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectionTransform = SelectionPrefab.GetComponent<RectTransform>();
    }
    public void SelectTool(int i)
    {
        int toolIndex = Math.Clamp(i, 0, 8); //Nice hardcoded tool number

        if (toolIndex == 0)
        {
            //On a 8 outils donc ça arrive pas (c'est en dehors de l'écran btw)
            //SelectionTransform.localPosition = new Vector2(FirstToolPos + 10 * ToolSpacing, SelectionTransform.localPosition.y); //nice hardcoded position
        }
        else
        {
            SelectionTransform.localPosition = ToolCells[toolIndex - 1].localPosition; //nice hardcoded position
        }
    }
}
