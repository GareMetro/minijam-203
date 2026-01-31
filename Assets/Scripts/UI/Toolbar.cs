using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Toolbar : MonoBehaviour
{
    [SerializeField] private GameObject SelectionPrefab;
    [SerializeField] private float FirstToolPos; //Coordonnée X de l'outil 1 sur l'UI
    [SerializeField] private float ToolSpacing; //Espace entre 2 outils sur l'UI
    
    private RectTransform SelectionTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectionTransform = SelectionPrefab.GetComponent<RectTransform>();
    }
    public void SelectTool(int i)
    {
        int toolIndex = Math.Clamp(i, 0, 7); //Nice hardcoded tool number

        if (toolIndex == 0)
        {
            //On a 8 outils donc ça arrive pas (c'est en dehors de l'écran btw)
            SelectionTransform.localPosition = new Vector2(FirstToolPos + 10 * ToolSpacing, SelectionTransform.localPosition.y); //nice hardcoded position
        }
        else
        {
            SelectionTransform.localPosition = new Vector2(FirstToolPos + ToolSpacing * (1 -toolIndex), SelectionTransform.localPosition.y); //nice hardcoded position
        }
    }
}
