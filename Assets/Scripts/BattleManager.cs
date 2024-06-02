using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    [Header("Cards")]
    public List<Card> deck;
    public List<Card> drawPile = new List<Card>();
    public List<Card> cardsInHand = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Dice> dices = new List<Dice>();
    public List<CardUI> cardUis = new List<CardUI>();
    public CardUI selectedCard;

    [Header("Stats")]
    public Character cardTarget;
    public Character player;
    public int maxHandSize = 10;
    public int handSize = 5;
    public Turn turn;
    public enum Turn { Player, Enemy };
    public int round;

    [Header("Enemies")]
    public List<Transform> enemySlots;
    public List<Enemy> enemies;

    [Header("UI")]
    public Button endTurnButton;
    public TMP_Text energyText;
    public Transform topParent;
    public Transform enemyParent;
    //public EndScreen endScreen;

    GameManager gameManager;

    [SerializeField]
    TextMeshProUGUI turnText;

    public static BattleManager instance;

    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameManager.instance;
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

        foreach (Card card in cardsInHand)
        {
            DiscardCard(card);
        }

        

        discardPile = new List<Card>();
        drawPile = new List<Card>();
        cardsInHand = new List<Card>();
        drawPile.AddRange(deck);
        ShuffleCards();
        DrawCards(handSize);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeTurn()
    {
        if (turn == Turn.Player)
        {
            turn = Turn.Enemy;
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
            #endregion

            /*foreach (Enemy e in enemies)
            {
                if (e.thisEnemy == null)
                    e.thisEnemy = e.GetComponent<Fighter>();

                //reset block
                e.thisEnemy.currentBlock = 0;
                e.thisEnemy.fighterHealthBar.DisplayBlock(0);
            }

            player.EvaluateBuffsAtTurnEnd();
            StartCoroutine(HandleEnemyTurn());*/
        }
        else
        {
            foreach (Enemy e in enemies)
            {
                //e.DisplayIntent();
            }
            turn = Turn.Player;

            //reset block
            /*player.currentBlock = 0;
            player.fighterHealthBar.DisplayBlock(0);
            energy = maxEnergy;
            energyText.text = energy.ToString();*/

            endTurnButton.enabled = true;
            DrawCards(maxHandSize);

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

            cardsInHand.Add(drawPile[0]);
            DisplayCardInHand(drawPile[0]);
            drawPile.Remove(drawPile[0]);
            //drawPileCountText.text = drawPile.Count.ToString();
            cardsDrawn++;
        }
    }
    public void DiscardCard(Card card)
    {
        cardsInHand.Remove(card);
        discardPile.Add(card);
    }

    public void ShuffleCards()
    {
        drawPile.AddRange(discardPile);
        drawPile.Shuffle();
        discardPile = new List<Card>();
    }

    public void DisplayCardInHand(Card card)
    {
        CardUI cardUI = cardUis[cardsInHand.Count - 1];
        cardUI.LoadCard(card);
        cardUI.gameObject.SetActive(true);
    }

    public void PlayCard(CardUI cardUI)
    {
        //Debug.Log("played card");
        //GoblinNob is enraged
       

        //cardActions.PerformAction(cardUI.card, cardTarget);

        //Instantiate(cardUI.discardEffect, cardUI.transform.position, Quaternion.identity, topParent);
        selectedCard = null;
        cardUI.gameObject.SetActive(false);
        cardsInHand.Remove(cardUI.card);
        DiscardCard(cardUI.card);
    }
}
