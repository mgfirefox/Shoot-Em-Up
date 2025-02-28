using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    public Vector2 velocity = new Vector2(2.5f, 2.5f);
    public Vector2 movementDirection = new Vector2(0, 0);

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = movementDirection * velocity;
    }
}
