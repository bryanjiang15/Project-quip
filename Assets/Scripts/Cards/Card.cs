using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    [Header("Card profile")]
    public string cardTitle;
    //public bool isUpgraded;
    public string cardDescription;
    public List<RequirementData> IngredientCosts;
    public Sprite cardIcon;

    public CardType cardType;
    public enum CardType { Attack, Skill, Power }
/*    public CardClass cardClass;
    public enum CardClass { ironChad, silent, colorless, curse, status }*/
    public CardTargetType cardTargetType;

    [Header("Action Settings")]
    public bool usableWithoutTarget;
    public bool exhaustAfterPlay;
    public List<CardActionData> cardActionDataList;

}

[Serializable]
public struct CardAmount
{
    public int baseAmount;
    public int upgradedAmount;
}
[Serializable]
public struct CardDescription
{
    public string baseAmount;
    public string upgradedAmount;
}
[Serializable]
public struct CardBuffs
{
    //public Buff.Type buffType;
    public CardAmount buffAmount;
}

public enum CardTargetType { self, enemy, allEnemy };

[Serializable]
public class CardActionData
{
    [SerializeField] private CardActionType cardActionType;
    [SerializeField] private CardTargetType actionTargetType;
    [SerializeField] private float actionValue;

    public CardTargetType ActionTargetType => actionTargetType;
    public CardActionType CardActionType => cardActionType;
    public float ActionValue => actionValue;

    #region Editor

#if UNITY_EDITOR
    public void EditActionType(CardActionType newType) => cardActionType = newType;
    public void EditActionTarget(CardTargetType newTargetType) => actionTargetType = newTargetType;
    public void EditActionValue(float newValue) => actionValue = newValue;

#endif


    #endregion
}