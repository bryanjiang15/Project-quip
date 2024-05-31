using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Card card;
    public TMP_Text cardTitleText;
    public TMP_Text cardDescriptionText;
    public TMP_Text cardCostText;
    public Image cardImage;
    public GameObject discardEffect;
    BattleManager battleManager;
    Animator animator;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
