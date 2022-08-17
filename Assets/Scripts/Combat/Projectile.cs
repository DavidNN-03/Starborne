using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.GameResources;

namespace Starborne.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float damage;
        [SerializeField] GameObject deathFXPrefab;

        public void SetDamage(float newDamage)
        {
            damage = newDamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (deathFXPrefab != null)
            {
                Instantiate(deathFXPrefab, transform.position, Quaternion.identity);
            }

            other.GetComponent<Health>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}