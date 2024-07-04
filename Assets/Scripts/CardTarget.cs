using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardTarget : MonoBehaviour
{
    BattleManager battleManager;
    Character enemyFighter;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        enemyFighter = GetComponent<Character>();
    }

    private void OnMouseEnter()
    {
        if (enemyFighter == null)
        {
            Debug.Log("fighter is null");
            battleManager = FindObjectOfType<BattleManager>();
            enemyFighter = GetComponent<Character>();
        }

        if (battleManager.selectedCard != null && battleManager.selectedCard.card.cardTargetType == CardTargetType.enemy)
        {
            //target == enemy
            battleManager.cardTarget = enemyFighter;
            //Debug.Log("set target");
        }
    }

    private void OnMouseExit()
    {
        battleManager.cardTarget = null;
        //Debug.Log("drop target");
    }
}