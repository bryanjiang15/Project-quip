using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Deck deck;
    public HandSlot hand;
    public Player player;

    [SerializeField]
    TextMeshProUGUI turntext;

    public static GameManager instance;

    private bool isPlayerTurn;
    private int turn;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        turn = 1;
        turntext.SetText("Turn " + turn);
        isPlayerTurn = true;
        beginTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void beginTurn()
    {
        deck.DrawCards(hand.maxHandSize);
    }

    public void endTurn()
    {

        if (!isPlayerTurn)
        {
            turn++;
            turntext.SetText("Turn " + turn);
        }
        isPlayerTurn = !isPlayerTurn;
    }
}
