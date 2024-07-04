using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Player Base Settings")]
    [SerializeField] private CharacterCanvas characterCanvas;
    [SerializeField] private PlayerData playerData;
    public CharacterCanvas CharacterCanvas => characterCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void BuildCharacter()
    {
        base.BuildCharacter();
        characterCanvas.InitCanvas();
        CharacterStats = new CharacterStats(playerData.MaxHealth, characterCanvas);
        CharacterStats.OnDeath += OnDeath;
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

}