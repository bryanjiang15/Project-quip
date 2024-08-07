﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dice", menuName = "ScriptableObjects/Dice", order = 2)]
public class Dice : ScriptableObject
{
    public int faceAmount;
    public List<IngredientData> faces;

    public IngredientData Roll()
    {
        if (faces.Count == 0)
        {
            Debug.LogError("No face assigned to dice!");
            return null;
        }

        // Get a random face letter
        int randomIndex = Random.Range(0, faceAmount);
        return faces[randomIndex];
    }

}
