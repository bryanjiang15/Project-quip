using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "ScriptableObjects/IngredientData", order = 0)]
public class IngredientData: ScriptableObject
{
/*    //old
    [SerializeField] public LetterType type;
    [SerializeField] public string letters;
    private string representationLetter;
    //*/

    [SerializeField] IngredientType ingredientType;
    [SerializeField] bool isSpecific;

    [SerializeField] string ingredientName;
    [SerializeField] Sprite image;
    [SerializeField] int ingredientID;

    public bool IsSpecific => isSpecific;
    public IngredientType Type => ingredientType;
    public string Name => ingredientName;
    public Sprite Image => image;
    public int IngredientID => ingredientID;
   

    public bool isEqual(IngredientData other)
    {
        return ingredientID == other.IngredientID;
    }
}

public enum IngredientType { Biological, Elemental, Corrupted, Standard }
