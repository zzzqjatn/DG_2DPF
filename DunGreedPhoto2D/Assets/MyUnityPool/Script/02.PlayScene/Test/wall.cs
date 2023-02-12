using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    private BoxCollider2D objBoxCollider; 

    void Start()
    {
        objBoxCollider = GetComponent<BoxCollider2D>();
        objBoxCollider.size = new Vector2(gameObject.RectSize().x, gameObject.RectSize().y);
    }

    void Update()
    {
        
    }
}
