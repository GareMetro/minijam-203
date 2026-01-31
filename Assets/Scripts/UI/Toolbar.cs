using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Toolbar : MonoBehaviour
{
    [SerializeField] private GameObject SelectionPrefab;

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
            SelectionTransform.position = new Vector2(650, SelectionTransform.position.y); //nice hardcoded position
        }
        else
        {
            SelectionTransform.position = new Vector2(-350 + 100 * toolIndex, SelectionTransform.position.y); //nice hardcoded position
        }
    }
}
