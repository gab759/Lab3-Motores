using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementY : MonoBehaviour
{
    private Transform _comTransform;
    public float speed;
    private int xDirection = 1;

    void Awake()
    {
        _comTransform = GetComponent<Transform>();
    }

    void Update()
    {
        _comTransform.position = new Vector2(_comTransform.position.x, _comTransform.position.y + speed * xDirection * Time.deltaTime);

        if (_comTransform.position.y > 1.5f || _comTransform.position.y < -3.4f)
        {
            xDirection = xDirection * -1;
        }

    }
}
