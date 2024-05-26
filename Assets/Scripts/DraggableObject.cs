using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector2 originalPosition;
    private Rigidbody2D _rigidbody;

    private bool stopping;
    Vector3 targetPosition;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;

        if (stopping)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, (targetPosition - transform.position * 10).magnitude);
            if(transform.position == targetPosition)
            {
                stopping = false;
                Debug.Log("stop");
            }
        }
        
    }

    private void OnMouseDrag()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newWorldPosition = new Vector3(position.x, position.y, transform.position.z);

        var difference = newWorldPosition - transform.position;

        var speed = 10 * difference;
        _rigidbody.velocity = speed;
    }

    private void OnMouseUp()
    {
        stopping = true;
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition = new Vector3(position.x, position.y, transform.position.z);

    }
}