using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class LetterData
{
    [SerializeField] public LetterType type;
    [SerializeField] public string letters;
    private string representationLetter;

    public string RepresentationLetter => representationLetter;
    
    public void initialize()
    {
        if (type == LetterType.Vowel)
        {
            letters = "AEIOU";
            representationLetter = "@";
        }
        else if (type == LetterType.Consonant)
        {
            letters = "BCDFGHJKLMNPQRSTVWXYZ";
            representationLetter = "*";
        }
        else
        {
            representationLetter = letters;
        }
    }
}
