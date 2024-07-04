using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Letter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public LetterData letterData;
    LetterRequirement targetedRequirment;
    TextMeshProUGUI letterText;

    public BattleManager battleManager;
    RectTransform rectTransform;
    RectTransform parentTransform;
    bool letterActive = true;

    Canvas canvas;
    CanvasGroup canvasGroup;
    public GraphicRaycaster graphicRaycaster;

    bool dragging;
    // Start is called before the first frame update
    void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        rectTransform = GetComponent<RectTransform>();
        parentTransform = GameObject.Find("LetterSlots").GetComponent<RectTransform>();
        canvas = GameObject.Find("/Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        graphicRaycaster = GameObject.Find("/Canvas").GetComponent<GraphicRaycaster>();
        letterText = GetComponentInChildren<TextMeshProUGUI>();

        if (letterData != null)
        {
            letterData.initialize();
            letterText.text = letterData.RepresentationLetter;
        }
    }

    public void setUpLetter(LetterData data)
    {
        letterData = data;
        letterData.initialize();
        letterText.text = letterData.RepresentationLetter;
    }

    public void SelectLetter()
    {
        //Debug.Log("card is selected");
        battleManager.selectedLetter = this;
    }

    public void DeselectLetter()
    {
        //Debug.Log("card is deselected");
        battleManager.selectedLetter = null;
        //animator.Play("HoverOffCard");
    }

    private Vector2 targetPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (letterActive)
        {
            dragging = true;
            transform.SetParent(canvas.transform);
            canvasGroup.blocksRaycasts = false;
            return;
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (letterActive)
        {
            // Convert the screen point where the cursor is to a point in the rectTransform's space
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out targetPosition);
            targetPosition += new Vector2(canvas.transform.position.x, -canvas.transform.position.y);

            List<RaycastResult> results = new List<RaycastResult>();

            graphicRaycaster.Raycast(eventData, results);

            bool hoveringRequirement = false;
            foreach (RaycastResult result in results)
            {
                
                GameObject hovered = result.gameObject;
                LetterRequirement req = hovered.GetComponent<LetterRequirement>();

                if(req != null)
                {
                    targetedRequirment = req;
                    hoveringRequirement = true;
                    targetPosition = hovered.transform.position - new Vector3(0, 2*canvas.transform.position.y);
                }
            
            }

            if (!hoveringRequirement)
            {
                targetedRequirment = null;
            }
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        if (targetedRequirment != null)
        {
            
            if (targetedRequirment.UseLetter(this))
            {
                transform.SetParent(targetedRequirment.transform);
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                transform.localPosition = Vector3.zero;
                letterActive = false;
                return;
            }
        }
        transform.SetParent(parentTransform);
        canvasGroup.blocksRaycasts = true;
        return;
    }

    private void Update()
    {
        // Lerp the position of the object towards the target position based on the speed
        if(dragging) rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, 12 * Time.deltaTime);
    }
}
public enum LetterType { Consonant, Vowel, Specific}




