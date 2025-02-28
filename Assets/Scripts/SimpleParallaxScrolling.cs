using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleParallaxScrolling : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0, 0);
    public Vector2 movementDirection = new Vector2(0, 0);

    public bool isLinkedToCamera = false;

    void Update()
    {
        Vector2 movement = velocity * movementDirection * Time.deltaTime;
        transform.Translate(movement);

        if (!isLinkedToCamera) { return; }
        Camera.main.transform.Translate(movement);
    }
}
