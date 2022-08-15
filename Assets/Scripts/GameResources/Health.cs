using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float health;
    private float maxHealth;

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
