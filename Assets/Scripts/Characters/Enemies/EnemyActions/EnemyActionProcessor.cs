using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

public static class EnemyActionProcessor
{
    private static readonly Dictionary<EnemyActionType, EnemyAction> EnemyActionDict =
        new Dictionary<EnemyActionType, EnemyAction>();

    public static bool IsInitialized { get; private set; }

    public static void Initialize()
    {
        EnemyActionDict.Clear();

        var allEnemyActions = Assembly.GetAssembly(typeof(EnemyAction)).GetTypes()
            .Where(t => typeof(EnemyAction).IsAssignableFrom(t) && t.IsAbstract == false);

        foreach (var enemyAction in allEnemyActions)
        {
            EnemyAction action = Activator.CreateInstance(enemyAction) as EnemyAction;
            if (action != null) EnemyActionDict.Add(action.ActionType, action);
        }
        IsInitialized = true;
    }

    public static EnemyAction GetAction(EnemyActionType targetAction) =>
        EnemyActionDict[targetAction];
}