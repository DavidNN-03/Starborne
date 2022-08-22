using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.GameResources;

namespace Starborne.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] float damage;
        [SerializeField] GameObject deathFXPrefab;

        void Awake()
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed; //also add parent speed?
        }

        public void SetDamage(float newDamage)
        {
            damage = newDamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (deathFXPrefab != null)
            {
                Instantiate(deathFXPrefab, transform.position, Quaternion.identity, GameObject.Find("Shots Parent").transform);
            }

            EnemyHealth otherHealth = other.GetComponent<EnemyHealth>();

            if (otherHealth != null)
            {
                other.GetComponent<EnemyHealth>().TakeDamage(damage);
            }

            if (other.tag != "DDP")
            {
                Destroy(gameObject);
            }
        }
    }
}