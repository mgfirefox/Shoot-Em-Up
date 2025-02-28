using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 1;

    public bool isEnemy = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Shot shot = collision.gameObject.GetComponent<Shot>();
        if (shot == null) { return; }

        if (shot.isEnemyShot == isEnemy) { return; }

        int damage = Mathf.Min(health, shot.damage);
        TakeDamage(damage);
        Destroy(shot.gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health > 0) { return; }

        Destroy(gameObject);
    }
}
