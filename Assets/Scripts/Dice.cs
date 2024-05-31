 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dice : MonoBehaviour
{
    public int faceAmount;
    public List<char> faces;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void Roll();

    public abstract void UseDice();
}
