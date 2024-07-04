using System.Collections;
using UnityEngine;

public class CardActionParameters
{
    public readonly float Value;
    public readonly Character TargetCharacter;
    public readonly Character SelfCharacter;
    public CardActionParameters(float value, Character target, Character self)
    {
        Value = value;
        TargetCharacter = target;
        SelfCharacter = self;
    }
}
public abstract class CardActionBase
{
    protected CardActionBase() { }
    public abstract CardActionType ActionType { get; }
    public abstract void DoAction(CardActionParameters actionParameters);

/*    protected FxManager FxManager => FxManager.Instance;
    protected AudioManager AudioManager => AudioManager.Instance;*/
    protected GameManager GameManager => GameManager.instance;
    protected BattleManager battleManager => BattleManager.instance;
/*    protected CollectionManager CollectionManager => CollectionManager.Instance;*/

}