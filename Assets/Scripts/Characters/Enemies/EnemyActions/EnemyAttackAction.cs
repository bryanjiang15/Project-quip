using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Characters.Enemies.EnemyActions
{
    public class EnemyAttackAction : EnemyAction
    {
        public override EnemyActionType ActionType => EnemyActionType.Attack;

        public override void DoAction(EnemyActionParameters actionParameters)
        {
            if (!actionParameters.TargetCharacter)
            {
                return;
            }

            var targetCharacter = actionParameters.TargetCharacter;
            var selfCharacter = actionParameters.SelfCharacter;

            var value = actionParameters.Value;
            targetCharacter.CharacterStats.Damage(Mathf.RoundToInt(value));
        }

    }
}