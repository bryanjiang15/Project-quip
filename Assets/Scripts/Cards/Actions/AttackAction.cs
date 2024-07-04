using System.Collections;
using UnityEngine;

public class AttackAction : CardActionBase
{
    public override CardActionType ActionType => CardActionType.Attack;
    public override void DoAction(CardActionParameters actionParameters)
    {
        if (!actionParameters.TargetCharacter) return;

        var targetCharacter = actionParameters.TargetCharacter;
        var selfCharacter = actionParameters.SelfCharacter;

        var value = actionParameters.Value;
        targetCharacter.CharacterStats.Damage(Mathf.RoundToInt(value));

    }
}