using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deck : MonoBehaviour
{

    public static Deck instance;

    public List<Card> deckPiles;
    public List<Card> discardPile;

    public HandSlot hand;
    public int handIndex;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ShuffleDeck()
    {
        deckPiles.AddRange(discardPile);
        discardPile.Clear();

        Shuffle.ShuffleList<Card>(deckPiles);
    }

    public void DrawCards(int handSize)
    {
        for(int i=0; i<handSize; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if(deckPiles.Count > 0)
        {
            Card drawnCard = deckPiles[0];
            deckPiles.RemoveAt(0);
            hand.addCard(drawnCard);
        }
    }


    void ResetDiscard()
    {
        return;
    }
}
