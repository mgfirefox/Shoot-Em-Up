using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0, 0);
    public Vector2 movementDirection = new Vector2(0, 0);

    public bool isLinkedToCamera = false;
    public bool isLooping = false;

    private List<Transform> backgroundObjects;

    void Start()
    {
        if (!isLooping) { return; }

        backgroundObjects = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.GetComponent<Renderer>() != null)
            {
                backgroundObjects.Add(child);
            }
        }

        backgroundObjects = backgroundObjects.OrderBy(t => t.position.x).ToList();
    }

    void Update()
    {
        Vector2 movement = velocity * movementDirection * Time.deltaTime;
        transform.Translate(movement);

        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        if (isLooping) {
            Transform firstChild = backgroundObjects.FirstOrDefault();
            if (firstChild != null)
            {
                if (firstChild.position.x < Camera.main.transform.position.x)
                {
                    if (!firstChild.GetComponent<Renderer>().IsVisibleFrom(Camera.main))
                    {
                        Transform lastChild = backgroundObjects.LastOrDefault();
                        Renderer renderer = lastChild.GetComponent<Renderer>();
                        Vector3 lastSize = renderer.bounds.max - renderer.bounds.min;

                        firstChild.position = new Vector3(lastChild.position.x + lastSize.x, firstChild.position.y, firstChild.position.z);

                        backgroundObjects.RemoveAt(0);
                        backgroundObjects.Add(firstChild);
                    }
                }
            }
        }
    }
}
