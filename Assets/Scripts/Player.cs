using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Deck deck;
    List<Card> hand;
    BasicHealth health;


    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<BasicHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void Damage(int dmg)
    {
        health.Damage(dmg);
    }

    public void Die()
    {
        Debug.Log("Game Over");
    }
}
