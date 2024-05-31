using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    [Header("Cards")]
    public List<Card> deck;
    public List<Card> drawPile = new List<Card>();
    public List<Card> cardsInHand = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Dice> dices = new List<Dice>();

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

    GameManager gameManager;

    [SerializeField]
    TextMeshProUGUI turntext;

    public static BattleManager instance;

    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameManager.instance;
        deck = GameManager.instance.playerDeck;
        instance = this;
    }

    void Start()
    {

    }

    public void BeginBattle(Encounter encounter)
    {
        round = 1;
        turn = Turn.Player;
        turntext.SetText("Turn " + round);

        for(int i=0; i<encounter.enemyPrefabs.Count; i++)
        {
            GameObject newEnemy = Instantiate(encounter.enemyPrefabs[i], enemySlots[i]);
        }

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

        ShuffleCards();
        DrawCards(handSize);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endTurn()
    {

        if (turn == Turn.Enemy)
        {
            round++;
            turntext.SetText("Turn " + turn);
            turn = Turn.Player;
        }
        else
        {
            turn = Turn.Enemy;
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
            //DisplayCardInHand(drawPile[0]);
            drawPile.Remove(drawPile[0]);
            //drawPileCountText.text = drawPile.Count.ToString();
            cardsDrawn++;
        }
    }

    

    public void ShuffleCards()
    {
        drawPile.AddRange(discardPile);
        drawPile.Shuffle();
        discardPile = new List<Card>();
    }

    public void DiscardCard(Card card)
    {
        cardsInHand.Remove(card);
        discardPile.Add(card);
    }
}
