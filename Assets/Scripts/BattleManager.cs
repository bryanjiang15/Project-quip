using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BattleManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject ingredientPrefab;
    [SerializeField] GameObject cardUIPrefab;

    [Header("Cards")]
    public List<Card> deck;
    public List<Dice> dices;
    public List<Card> drawPile = new List<Card>();
    public List<Card> cardsInHand = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Ingredient> ingredients = new List<Ingredient>();
    public List<CardUI> cardUis = new List<CardUI>();
    public CardUI selectedCard;
    public Ingredient selectedIngredient;

    [Header("Stats")]
    public Character cardTarget;
    public Character player;
    public int maxHandSize = 10;
    public int maxIngredientCount = 10;
    public int handSize = 5;
    public Turn turn;
    public enum Turn { Player, Enemy };
    public int round;

    [Header("Enemies")]
    public List<Transform> enemySlots;
    public List<Enemy> enemies;

    [Header("UI")]
    public Button endTurnButton;
    public Transform ingredientSlot;
    public Transform topParent;
    public Transform enemyParent;
    //public EndScreen endScreen;

    GameManager gameManager;
    public Action OnAllyTurnStarted;
    public Action OnEnemyTurnStarted;

    [SerializeField]
    TextMeshProUGUI turnText;

    public static BattleManager instance;

    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameManager.instance;
        CardActionProcessor.Initialize();
        EnemyActionProcessor.Initialize();
        instance = this;
    }

    void Start()
    {
        BeginBattle();
    }

    public void BeginBattle()
    {
        
        round = 1;
        turn = Turn.Player;
        turnText.SetText("Turn " + round);

       /* for(int i=0; i<encounter.enemyPrefabs.Count; i++)
        {
            GameObject newEnemy = Instantiate(encounter.enemyPrefabs[i], enemySlots[i]);
        }*/

        Enemy[] eArr = FindObjectsOfType<Enemy>();
        enemies = new List<Enemy>();

        foreach (Enemy e in eArr) { enemies.Add(e); }

        BuildCharacters();

        foreach (CardUI card in cardUis)
        {
            DiscardCard(card);
        }
        

        discardPile = new List<Card>();
        drawPile = new List<Card>();
        cardsInHand = new List<Card>();
        drawPile.AddRange(deck);
        ShuffleCards();
        DrawCards(handSize);
        DrawIngredients();

        OnAllyTurnStarted?.Invoke();
    }

    void BuildCharacters()
    {
        player.BuildCharacter();
        foreach(Enemy e in enemies)
        {
            e.BuildCharacter();
        }
    }

    public void changeTurn()
    {
        if (turn == Turn.Player)
        {
            turn = Turn.Enemy;
            OnEnemyTurnStarted?.Invoke();

            endTurnButton.enabled = false;

            #region discard hand
            /*foreach (Card card in cardsInHand)
            {
                DiscardCard(card);
            }
            foreach (CardUI cardUI in cardsInHandGameObjects)
            {
                if (cardUI.gameObject.activeSelf)
                    Instantiate(cardUI.discardEffect, cardUI.transform.position, Quaternion.identity, topParent);

                cardUI.gameObject.SetActive(false);
                cardsInHand.Remove(cardUI.card);
            }*/
            ingredients.Clear();

            //TODO Animation for discard
            for (int i = ingredientSlot.childCount - 1; i >= 0; i--)
            {
                Destroy(ingredientSlot.GetChild(i).gameObject);
            }
            #endregion

            StartCoroutine(EnemyTurnRoutine());
        }
        else
        {
            foreach (Enemy e in enemies)
            {
                //e.DisplayIntent();
            }

            turn = Turn.Player;
            OnAllyTurnStarted?.Invoke();

            //reset block
            /*player.currentBlock = 0;
            player.fighterHealthBar.DisplayBlock(0);
            energy = maxEnergy;
            energyText.text = energy.ToString();*/

            endTurnButton.enabled = true;
            DrawCards(maxHandSize);
            DrawIngredients();

            turnText.text = "Player's Turn";
            //banner.Play("bannerOut");
        }
    }

    public void DrawCards(int amountToDraw)
    {
        int cardsDrawn = 0;
        while (cardsDrawn < amountToDraw && cardsInHand.Count <= maxHandSize)
        {
            if (drawPile.Count < 1)
                ShuffleCards();

            if(drawPile.Count < 1)
            {
                return;
            }

            cardsInHand.Add(drawPile[0]);
            DisplayCardInHand(drawPile[0]);
            drawPile.Remove(drawPile[0]);
            //drawPileCountText.text = drawPile.Count.ToString();
            cardsDrawn++;
        }
    }
    public void DiscardCard(CardUI cardUI)
    {
        cardUI.ResetCard();
        cardUI.gameObject.SetActive(false);
        //cardUI.gameObject.transform.SetAsLastSibling();
        cardsInHand.Remove(cardUI.card);
        cardsInHand.Remove(cardUI.card);
        discardPile.Add(cardUI.card);

    }

    public void ShuffleCards()
    {
        drawPile.AddRange(discardPile);
        drawPile.Shuffle();
        discardPile = new List<Card>();
    }
    public void DrawIngredients()
    {
        ingredients.Clear(); // Clear the list from previous results

        // Loop through each dice in the list
        foreach (Dice dice in dices)
        {
            // Roll the dice and store the result
            IngredientData ingredientData = dice.Roll();
            if (ingredientData != null)
            {
                GameObject ingredientObject = Instantiate(ingredientPrefab);
                Ingredient ingredient = ingredientObject.GetComponent<Ingredient>();
                ingredient.setUpIngredient(ingredientData);
                ingredients.Add(ingredient);

                ingredientObject.transform.SetParent(ingredientSlot);
                
            }
        }
    }

    public void DiscardIngredient(Ingredient ingredient)
    {
        ingredients.Remove(ingredient);
    }

    public void DisplayCardInHand(Card card)
    {
        CardUI cardUI = null;
        foreach(CardUI c in cardUis)
        {
            if (!c.gameObject.activeInHierarchy)
            {
                cardUI = c;
                break;
            }
        }
        if (cardUI)
        {
            cardUI.LoadCard(card);
            cardUI.gameObject.SetActive(true);
        }
        
    }

    public void PlayCard(CardUI cardUI)

    {
        //Debug.Log("played card");
        //GoblinNob is enraged

        StartCoroutine(CardUseRoutine(cardUI, player, cardTarget, enemies));
        //Instantiate(cardUI.discardEffect, cardUI.transform.position, Quaternion.identity, topParent);      
    }

    private IEnumerator CardUseRoutine(CardUI cardUI, Character self, Character targetCharacter, List<Enemy> allEnemies)
    {
        bool played = false;
        foreach (var playerAction in cardUI.card.cardActionDataList)
        {
            
            var targetList = DetermineTargets(targetCharacter, allEnemies, player, playerAction);
            if (targetList == null)
            {
                break;
            }
            else
            {
                played = true;
                foreach (var target in targetList)
                                CardActionProcessor.GetAction(playerAction.CardActionType)
                                    .DoAction(new CardActionParameters(playerAction.ActionValue,
                                        target, self));
            }
        }
        if (played)
        {
            selectedCard = null;
            
            DiscardCard(cardUI);
        }
        
        

        yield return null;
    }

    private static List<Character> DetermineTargets(Character targetCharacter, List<Enemy> allEnemies, Character player, CardActionData playerAction)
    {
        List<Character> targetList = new List<Character>();
        switch (playerAction.ActionTargetType)
        {
            case CardTargetType.enemy:
                if (targetCharacter == null) return null;
                targetList.Add(targetCharacter);
                break;
/*            case CardTargetType.Ally:
                targetList.Add(targetCharacter);
                break;*/
            case CardTargetType.allEnemy:
                foreach (var enemyBase in allEnemies)
                    targetList.Add(enemyBase);
                break;
            case CardTargetType.self:
                targetList.Add(player);
                break;
            /*case ActionTargetType.AllAllies:
                foreach (var allyBase in allAllies)
                    targetList.Add(allyBase);
                break;*/
            /*case CardTargetType.RandomEnemy:
                if (allEnemies.Count > 0)
                    targetList.Add(allEnemies.RandomItem());

                break;
            case ActionTargetType.RandomAlly:
                if (allAllies.Count > 0)
                    targetList.Add(allAllies.RandomItem());
                break;*/
            default:
                break;
        }

        return targetList;
    }

    public void EndFight(bool win)
    {
        //if (!win)
            //gameover.SetActive(true);

        //player.ResetBuffs();
        //HandleEndScreen();

        //gameManager.UpdateFloorNumber();
        //gameManager.UpdateGoldNumber(enemies[0].goldDrop);
;
    }

    private IEnumerator EnemyTurnRoutine()
    {
        var waitDelay = new WaitForSeconds(0.7f);
        yield return waitDelay;

        foreach (var currentEnemy in enemies)
        {
            yield return currentEnemy.StartCoroutine(nameof(currentEnemy.ActionRoutine));
            yield return waitDelay;
        }

        changeTurn();

    }
}
