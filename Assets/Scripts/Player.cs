using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private Weapon[] weapons;
    private Health health;

    public Vector2 velocity = new Vector2(5, 5f);
    private Vector2 movementDirection;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        weapons = GetComponentsInChildren<Weapon>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBorder, rightBorder), Mathf.Clamp(transform.position.y, topBorder, bottomBorder), transform.position.z);

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movementDirection = new Vector2(inputX, inputY);
        movementDirection.Normalize();

        bool isShotFired = Input.GetButtonDown("Fire1") | Input.GetButtonDown("Fire2");
        if (!isShotFired) { return; }

        foreach (Weapon weapon in weapons)
        {
            if (weapon == null) { continue; }
            if (!weapon.CanAttack()) { continue; }
            weapon.Attack();
        }
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = movementDirection * velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy == null) { return; }

        Health enemyHealth = collision.gameObject.GetComponent<Health>();
        if (enemyHealth == null) { return; };
        int damage = Mathf.Min(health.health, enemyHealth.health);
        enemyHealth.TakeDamage(damage);
        health.TakeDamage(damage);
    }
}
