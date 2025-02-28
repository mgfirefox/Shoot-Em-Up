using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private new Collider2D collider2D;
    private new Renderer renderer;
    private Move move;

    private Weapon[] weapons;

    private bool isEnabled = false;

    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        renderer = GetComponent<Renderer>();
        move = GetComponent<Move>();
        weapons = GetComponentsInChildren<Weapon>();
    }

    void Update()
    {
        if (!isEnabled)
        {
            if (renderer.IsVisibleFrom(Camera.main)) {
                collider2D.enabled = true;
                move.enabled = true;

                foreach (Weapon weapon in weapons)
                {
                    weapon.enabled = true;
                }
                isEnabled = true;
            }
            return;
        }

        foreach (Weapon weapon in weapons) {
            if (weapon == null) { continue; }
            if (!weapon.CanAttack()) { continue; }
            weapon.Attack();
        }

        if (!renderer.IsVisibleFrom(Camera.main))
        {
            Destroy(gameObject);
        }
    }
}
