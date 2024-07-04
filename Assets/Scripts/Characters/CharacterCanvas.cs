using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentHealthText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCanvas()
    {
        /*highlightRoot.gameObject.SetActive(false);

        for (int i = 0; i < Enum.GetNames(typeof(StatusType)).Length; i++)
            StatusDict.Add((StatusType)i, null);

        TargetCanvas = GetComponent<Canvas>();

        if (TargetCanvas)
            TargetCanvas.worldCamera = Camera.main;*/
    }

    public void UpdateHealthText(int currentHealth, int maxHealth) => currentHealthText.text = $"{currentHealth}/{maxHealth}";
}
