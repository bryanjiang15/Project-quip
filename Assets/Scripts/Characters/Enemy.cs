using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    [Header("Ally Base Settings")]
    [SerializeField] private CharacterCanvas characterCanvas;
    [SerializeField] private EnemyData characterData;
    protected EnemyAbilityData NextAbility;
    public CharacterCanvas CharacterCanvas => characterCanvas;

    private int _usedAbilityCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void BuildCharacter()
    {
        base.BuildCharacter();
        characterCanvas.InitCanvas();
        CharacterStats = new CharacterStats(characterData.MaxHealth, characterCanvas);
        CharacterStats.OnDeath += OnDeath;
        battleManager.OnAllyTurnStarted += ShowNextAbility;
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        if (battleManager != null)
        {
            /*battleManager.OnAllyTurnStarted -= CharacterStats.battleManager;
            battleManager.OnAllyDeath(this);*/
        }

        Destroy(gameObject);
    }

    public virtual IEnumerator ActionRoutine()
    {
        if (CharacterStats.IsStunned)
            yield break;

        //EnemyCanvas.IntentImage.gameObject.SetActive(false);
        if (NextAbility.Intention.EnemyIntentionType == EnemyIntentionType.Attack || NextAbility.Intention.EnemyIntentionType == EnemyIntentionType.Debuff)
        {
            yield return StartCoroutine(AttackRoutine(NextAbility));
        }
        else
        {
            yield return StartCoroutine(BuffRoutine(NextAbility));
        }
    }

    private void ShowNextAbility()
    {
        NextAbility = characterData.GetAbility(_usedAbilityCount);
        //EnemyCanvas.IntentImage.sprite = NextAbility.Intention.IntentionSprite;

        /*if (NextAbility.HideActionValue)
        {
            EnemyCanvas.NextActionValueText.gameObject.SetActive(false);
        }
        else
        {
            EnemyCanvas.NextActionValueText.gameObject.SetActive(true);
            EnemyCanvas.NextActionValueText.text = NextAbility.ActionList[0].ActionValue.ToString();
        }*/

        _usedAbilityCount++;
        //EnemyCanvas.IntentImage.gameObject.SetActive(true);
    }

    protected virtual IEnumerator AttackRoutine(EnemyAbilityData targetAbility)
    {
        var waitFrame = new WaitForEndOfFrame();

        if (battleManager == null) yield break;

        var target = DetermineTargets();

        targetAbility.ActionList.ForEach(x => target.ForEach(y => EnemyActionProcessor.GetAction(x.ActionType).DoAction(new EnemyActionParameters(x.ActionValue, y, this))));

    }

    protected virtual IEnumerator BuffRoutine(EnemyAbilityData targetAbility)
    {
        var waitFrame = new WaitForEndOfFrame();

        if (battleManager == null) yield break;

        var target = DetermineTargets();

        targetAbility.ActionList.ForEach(x => target.ForEach(y => EnemyActionProcessor.GetAction(x.ActionType).DoAction(new EnemyActionParameters(x.ActionValue, y, this))));

    }

    protected List<Character> DetermineTargets()
    {
        List<Character> targetList = new List<Character>();
        targetList.Add(battleManager.player);
        return targetList;
    }
}