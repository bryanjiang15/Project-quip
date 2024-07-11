using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ingredient : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public IngredientData ingredientData;
    public BattleManager battleManager;
    public GraphicRaycaster graphicRaycaster;

    IngredientRequirement targetedRequirment;
    Image image;

    
    RectTransform rectTransform;
    RectTransform parentTransform;
    bool ingredientActive = true;

    Canvas canvas;
    CanvasGroup canvasGroup;
    

    bool dragging;
    // Start is called before the first frame update
    void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        rectTransform = GetComponent<RectTransform>();
        parentTransform = GameObject.Find("IngredientSlots").GetComponent<RectTransform>();
        canvas = GameObject.Find("/Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        graphicRaycaster = GameObject.Find("/Canvas").GetComponent<GraphicRaycaster>();
        image = GetComponent<Image>();
    }

    public void setUpIngredient(IngredientData data)
    {
        ingredientData = data;
        image.sprite = ingredientData.Image;
    } 

    public void SelectIngredient()
    {
        //Debug.Log("card is selected");
        battleManager.selectedIngredient = this;
    }

    public void DeselectIngredient()
    {
        //Debug.Log("card is deselected");
        battleManager.selectedIngredient = null;
        //animator.Play("HoverOffCard");
    }

    private Vector2 targetPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ingredientActive)
        {
            dragging = true;
            transform.SetParent(canvas.transform);
            canvasGroup.blocksRaycasts = false;
            return;
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ingredientActive)
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
                IngredientRequirement req = hovered.GetComponent<IngredientRequirement>();

                if(req != null)
                {
                    targetedRequirment = req;
                    hoveringRequirement = true;
                    targetPosition = hovered.transform.position - new Vector3(0, 2*canvas.transform.position.y);
                    break;
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
            
            if (targetedRequirment.UseIngredient(this))
            {
                transform.SetParent(targetedRequirment.transform);
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                transform.localPosition = Vector3.zero;
                ingredientActive = false;
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




