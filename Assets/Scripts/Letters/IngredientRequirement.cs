using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IngredientRequirement: MonoBehaviour
{
    [SerializeField] Image image;

    private RequirementData requirmentData;

    public bool isSatisfied = false;

    public void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetUpRequirement(RequirementData data)
    {
        requirmentData = data;
        image.sprite = requirmentData.Image;
    }

    public bool IngredientValid(Ingredient ingredient)
    {

        IngredientData ingredientData = ingredient.ingredientData;

        return requirmentData.isIngredientValid(ingredientData);
    }

    public bool UseIngredient(Ingredient ingredient)
    {
        if (IngredientValid(ingredient) && !isSatisfied)
        {
            isSatisfied = true;
            return true;
        }
        return false;
    }


}