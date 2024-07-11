using System.Collections;
using UnityEngine;

namespace Cards.Actions
{
    public class StrengthUpAction : CardActionBase
    {

        public override CardActionType ActionType => CardActionType.IncreaseStrength;
        public override void DoAction(CardActionParameters actionParameters)
        {
            var self = actionParameters.SelfCharacter;
            if (!self) return;

            self.CharacterStats.ApplyStatus(StatusType.Strength,
                    Mathf.RoundToInt(actionParameters.Value));

        }
    }
}