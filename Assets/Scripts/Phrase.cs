using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Phrase : MonoBehaviour
{
    // Start is called before the first frame update
    string content;
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = new Vector2(text.preferredWidth, collider.bounds.extents.y);
        collider.offset = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
