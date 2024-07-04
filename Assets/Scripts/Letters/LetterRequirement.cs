using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class LetterRequirement: MonoBehaviour
{
    public LetterType type;
    public string letters;
    public TextMeshProUGUI letterText;

    public bool isSatisfied = false;


    public void SetUpRequirement(char letter)
    {
        if (letter == '0')
        {
            type = LetterType.Vowel;
            letters = "AEIOU";
            letterText.text = "@";
        }
        else if (letter == '1')
        {
            type = LetterType.Consonant;
            letters = "BCDFGHJKLMNPQRSTVWXYZ";
            letterText.text = "*";
        }       
        else
        {
            type = LetterType.Specific;
            letters = letter.ToString();
            letterText.text = letters;
        }
    }

    public bool LetterValid(Letter letter)
    {
        HashSet<char> set1 = new HashSet<char>(letter.letterData.letters);
        if (letters.Any(c => set1.Contains(c)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UseLetter(Letter letter)
    {
        if (LetterValid(letter) && !isSatisfied)
        {
            isSatisfied = true;
            return true;
        }
        return false;
    }


}