﻿using System.Collections;
using UnityEngine;

namespace Cards.Actions
{
    public class BlockAction : CardActionBase
    {
        public override CardActionType ActionType => CardActionType.Block;
        public override void DoAction(CardActionParameters actionParameters)
        {
            var newTarget = actionParameters.TargetCharacter
                    ? actionParameters.TargetCharacter
                    : actionParameters.SelfCharacter;
            if (!newTarget) return;

            newTarget.CharacterStats.ApplyStatus(StatusType.Block,
                    Mathf.RoundToInt(actionParameters.Value + actionParameters.SelfCharacter.CharacterStats
                        .StatusDict[StatusType.Dexterity].StatusValue));

        }
    }
}