using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    bool hasPlayed;
    bool selected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    protected abstract void OnMouseDown();

    protected abstract void OnMouseUp();

    protected abstract void playCard();

    /*private void OnMouseDown()
    {
        selected = true;
        transform.position += Vector3.up;
    }

    private void OnMouseUp()
    {
        hasPlayed = true;
        playCard();
        gameObject.SetActive(false);
        selected = false;
    }*/
}
