using System.Collections;
using UnityEngine;

public abstract class EnemyAction
{
    protected EnemyAction() { }
    public abstract EnemyActionType ActionType { get; }
    public abstract void DoAction(EnemyActionParameters actionParameters);

    protected GameManager GameManager => GameManager.instance;
    protected BattleManager BattleManager => BattleManager.instance;

}

public class EnemyActionParameters
{
    public readonly float Value;
    public readonly Character TargetCharacter;
    public readonly Character SelfCharacter;

    public EnemyActionParameters(float value, Character target, Character self)
    {
        Value = value;
        TargetCharacter = target;
        SelfCharacter = self;
    }
}