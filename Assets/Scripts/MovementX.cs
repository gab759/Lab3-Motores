using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementX : MonoBehaviour
{
    private Transform _comTransform;
    public float speed;
    private int xDirection = 1;
    private float minX = -6f;
    private float maxX = 6f;

    void Awake()
    {
        _comTransform = GetComponent<Transform>();
    }

    void Update()
    {
        _comTransform.position = new Vector2(_comTransform.position.x + speed * xDirection * Time.deltaTime, _comTransform.position.y);

        if (_comTransform.position.x >= maxX || _comTransform.position.x <= minX)
        {
            xDirection *= -1; 
        }
    }
}