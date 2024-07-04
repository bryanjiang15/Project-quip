using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// A basic health system with iFrames and a max health implemented
/// Written by Nikhil Ghosh '24, Max Perraut '20
/// </summary>
public abstract class Character : MonoBehaviour
{

    public CharacterStats CharacterStats { get; protected set; }
    //public FighterHealthBar fighterHealthBar;

    /*[Header("Buffs")]
    public Buff vulnerable;
    public Buff weak;
    public Buff strength;
    public Buff ritual;
    public Buff enrage;
    public GameObject buffPrefab;
    public Transform buffParent;*/

    protected BattleManager battleManager;
    protected GameManager gameManager;
    public GameObject damageIndicator;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        gameManager = FindObjectOfType<GameManager>();

        /*fighterHealthBar.healthSlider.maxValue = maxHealth;
        fighterHealthBar.DisplayHealth(currentHealth);
        if (isPlayer)
            gameManager.DisplayHealth(currentHealth, currentHealth);*/

    }

    public virtual void BuildCharacter()
    {

    }

    protected virtual void OnDeath()
    {

    }
}

public enum CharacterType{
    ally,
    enemy
}



