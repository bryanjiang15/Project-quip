using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    public static HandSlot instance;

    public SpriteRenderer handArea; // Set this to your area's RectTransform
    public int maxHandSize;
    void Start()
    {
        instance = this;
        DistributeCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DistributeCards()
    {
        
        CardUI[] hand = GetComponentsInChildren<CardUI>();
        if (hand.Length == 0)
            return;

        float areaWidth = handArea.size.x * handArea.transform.localScale.x * 2;
        float areaHeight = handArea.size.y * handArea.transform.localScale.y * 2;
        Debug.Log(areaWidth + " " + areaHeight);

        Vector3 cardSize = hand[0].GetComponent<Collider2D>().bounds.size;
        
        // Calculate the number of rows and columns to fit the objects
        // For this example, we'll just place them in a single row
        int columns = hand.Length;

        // Calculate the spacing between objects
        float horizontalSpacing = (areaWidth - (columns * cardSize.x)) / (columns + 1);
        for (int i = 0; i < hand.Length; i++)
        {
            // Calculate the position for each object
            float xPosition = horizontalSpacing + (cardSize.x / 2) + (i * (cardSize.x + horizontalSpacing)) - areaWidth / 2;
            float yPosition = transform.position.x;

            // Position is set relative to the bottom-left corner of the area
            Vector3 newPosition = new Vector3(xPosition, yPosition, 0);

            // Assuming the anchor point for the GameObjects is their center,
            // and your area's anchor is at the bottom left.
            hand[i].transform.localPosition = newPosition;
        }
    }

    public void addCard(CardUI card)
    {
        card.transform.parent = transform;
        card.gameObject.SetActive(true);
        DistributeCards();
    }

    public void removeCardAt(int index)
    {
        Card[] hand = GetComponentsInChildren<Card>();
    }

    public void addCards(CardUI[] cards)
    {
        foreach(CardUI card in cards){
            card.transform.parent = transform;
        }
        DistributeCards();
    }

    public void discardAll()
    {
        return;
    }
}
