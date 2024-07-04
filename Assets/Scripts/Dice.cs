 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dice", menuName = "ScriptableObjects/Dice", order = 2)]
public class Dice : ScriptableObject
{
    public int faceAmount;
    public List<LetterData> faces;

    public LetterData Roll()
    {
        if (faces.Count == 0)
        {
            Debug.LogError("No face letters assigned to dice!");
            return null;
        }

        // Get a random face letter
        int randomIndex = Random.Range(0, faceAmount);
        return faces[randomIndex];
    }

}
