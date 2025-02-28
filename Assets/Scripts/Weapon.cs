using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float firerate = 0.5f;
    private float fireCooldown;

    private void Start()
    {
        fireCooldown = 0.0f;
    }

    private void Update()
    {
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (!CanAttack()) { return; }

        fireCooldown = firerate;

        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
    }

    public bool CanAttack()
    {
        return !(fireCooldown > 0);
    }
}
