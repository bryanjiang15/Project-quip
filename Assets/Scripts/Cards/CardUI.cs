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
    public Image cardImage;
    public GameObject discardEffect;
    BattleManager battleManager;
    Animator animator;

    public GameObject requirementSlots;
    public LetterRequirement letterRequirementPrefab;

    void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }

    public void SetupRequirements(string letterCosts)
    {
        foreach (char c in letterCosts)
        {
            LetterRequirement l = Instantiate(letterRequirementPrefab);
            l.SetUpRequirement(c);
            l.transform.SetParent(requirementSlots.transform);
        }
    }

    public bool IsPlayable()
    {
        foreach (LetterRequirement l in requirementSlots.GetComponentsInChildren<LetterRequirement>())
        {
            if (!l.isSatisfied)
            {
                return false;
            }
        }
        return true;
    }

    public void LoadCard(Card _card)
    {
        card = _card;
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        cardTitleText.text = card.cardTitle;
        cardDescriptionText.text = card.cardDescription;
        //cardCostText.text = card.letterCosts;
        cardImage.sprite = card.cardIcon;
        SetupRequirements(card.letterCosts);
    }
    public void SelectCard()
    {
        //Debug.Log("card is selected");
        battleManager.selectedCard = this;
    }

    public void DeselectCard()
    {
        //Debug.Log("card is deselected");
        battleManager.selectedCard = null;
        //animator.Play("HoverOffCard");
    }

    public void HandleDrag()
    {

    }
    public void HandleEndDrag()
    {
        if (!IsPlayable())
            return;

        if (card.cardType == Card.CardType.Attack)
        {
            battleManager.PlayCard(this);
            //animator.Play("HoverOffCard");
        }
        else if (card.cardType != Card.CardType.Attack)
        {
            //animator.Play("HoverOffCard");
            battleManager.PlayCard(this);
        }
    }

    public void ResetCard()
    {
        var letters = GetComponentsInChildren<LetterRequirement>();
        for (int i = letters.Length - 1; i >= 0; i--)
        {
            Destroy(letters[i].gameObject);
        }
    }
}
