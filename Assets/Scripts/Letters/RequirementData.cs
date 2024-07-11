using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "RequirementData", menuName = "ScriptableObjects/RequirementData", order = 0)]

public class RequirementData: ScriptableObject
{
    [SerializeField] IngredientType type;
    [SerializeField] bool isSpecific;
    [SerializeField] IngredientData specificIngredient;
    [SerializeField] Sprite image;

    public IngredientType Type => type;
    public bool IsSpecific => isSpecific;
    public IngredientData SpecificIngredient => specificIngredient;
    public Sprite Image => image;
    
    public bool isIngredientValid(IngredientData ingredientData)
    {
        if (type != ingredientData.Type)
        {
            return false;
        }

        if (IsSpecific)
        {
            if (ingredientData.IsSpecific)
            {
                return specificIngredient.isEqual(ingredientData);
            }
            return true;
        }

        return true;
    }
}

