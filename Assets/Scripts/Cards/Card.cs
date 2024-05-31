using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : ScriptableObject
{
    public string cardTitle;
    //public bool isUpgraded;
    public CardDescription cardDescription;
    public CardAmount cardCost;
    public CardAmount cardEffect;
    public CardAmount buffAmount;
    public Sprite cardIcon;
    public CardType cardType;
    public enum CardType { Attack, Skill, Power }
/*    public CardClass cardClass;
    public enum CardClass { ironChad, silent, colorless, curse, status }*/
    public CardTargetType cardTargetType;
    public enum CardTargetType { self, enemy, allEnemy };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    protected abstract void OnMouseDown();

    protected abstract void OnMouseUp();

    protected abstract void playCard();

    /*private void OnMouseDown()
    {
        selected = true;
        transform.position += Vector3.up;
    }

    private void OnMouseUp()
    {
        hasPlayed = true;
        playCard();
        gameObject.SetActive(false);
        selected = false;
    }*/
}

[System.Serializable]
public struct CardAmount
{
    public int baseAmount;
    public int upgradedAmount;
}
[System.Serializable]
public struct CardDescription
{
    public string baseAmount;
    public string upgradedAmount;
}
[System.Serializable]
public struct CardBuffs
{
    //public Buff.Type buffType;
    public CardAmount buffAmount;
}
