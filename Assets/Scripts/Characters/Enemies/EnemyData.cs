using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TJ;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemydata", order = 0)]

public class EnemyData : CharacterData
{
    [Header("Enemy Defaults")]
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private bool followAbilityPattern;
    [SerializeField] private List<EnemyAbilityData> enemyAbilityList;

    public Enemy EnemyPrefab => enemyPrefab;
    public List<EnemyAbilityData> EnemyAbilityList => enemyAbilityList;

    public EnemyAbilityData GetAbility()
    {
        return EnemyAbilityList.RandomItem();
    }

    public EnemyAbilityData GetAbility(int usedAbilityCount)
    {
        if (followAbilityPattern)
        {
            var index = usedAbilityCount % EnemyAbilityList.Count;
            return EnemyAbilityList[index];
        }

        return GetAbility();
    }

}

[Serializable]
public class EnemyAbilityData
{
    [Header("Settings")]
    [SerializeField] private string name;
    [SerializeField] private EnemyIntentionData intention;
    [SerializeField] private bool hideActionValue;
    [SerializeField] private List<EnemyActionData> actionList;
    public string Name => name;
    public EnemyIntentionData Intention => intention;
    public List<EnemyActionData> ActionList => actionList;
    public bool HideActionValue => hideActionValue;
}

[Serializable]
public class EnemyActionData
{
    [SerializeField] private EnemyActionType actionType;
    [SerializeField] private int minActionValue;
    [SerializeField] private int maxActionValue;
    public EnemyActionType ActionType => actionType;
    public int ActionValue => Random.Range(minActionValue, maxActionValue);

}


