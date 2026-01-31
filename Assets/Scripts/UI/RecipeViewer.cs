using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using DG;
using DG.Tweening;
using Unity.VisualScripting;

public class RecipeViewer : MonoBehaviour
{

    public List<TransformativeBuilding> TransformativeBuildingList;

    [SerializeField] private GameObject UIParent;
    [SerializeField] private GameObject RecipePrefab;
    [SerializeField] private GameObject ScrollView;

    [DoNotSerialize] public bool IsVisible = false;

    Tween tween;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(TransformativeBuilding building in TransformativeBuildingList)
        {
            foreach (Recipe recipe in building.recipes)
            {
                GameObject added = Instantiate(RecipePrefab, UIParent.transform);
                added.TryGetComponent<RecipeUI>(out RecipeUI recipeUIComponent);

                int i = 0;
                foreach(BaseIngredient food in recipe.ingredients)
                {
                    recipeUIComponent.iconList[i].sprite = food.icon;
                    ++i;
                    if (i > 2) break;
                }

                recipeUIComponent.iconList[3].sprite = building.Icon;
                recipeUIComponent.iconList[4].sprite = recipe.outputs[0].icon;

            }
        }
    }


    float getPivot()
    {
        return (ScrollView.transform as RectTransform).pivot.x;
    }

    void setPivot(float x)
    {
        (ScrollView.transform as RectTransform).pivot = new Vector2(x, 1);
    }

    public void Hide()
    {
        RectTransform trans = ScrollView.transform as RectTransform;

        DOTween.To(getPivot, setPivot, 0f, 1f).SetEase(Ease.OutExpo);
    }

    public void Show()
    {
        RectTransform trans = ScrollView.transform as RectTransform;

        DOTween.To(getPivot, setPivot, 1f, 1f).SetEase(Ease.OutExpo);
    }
}
