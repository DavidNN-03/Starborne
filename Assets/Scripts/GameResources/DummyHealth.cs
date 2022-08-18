using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHealth : MonoBehaviour
{
    private float health;
    [SerializeField] private float maxHealth;

    public event Action onDeath;

    public void TakeDamage(float damage)
        {
            health -= damage;
            if(health <= 0)
            {
                onDeath();
            }
        }
}
