using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Character character;
    public List<Card> playerDeck = new List<Card>();
    public List<Card> cardLibrary = new List<Card>();

    public int floorNumber = 1;
    public int goldAmount;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
