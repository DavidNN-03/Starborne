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
        GameObject owner;

        void Awake()
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed; //also add parent speed?
        }

        public void SetDamage(float newDamage)
        {
            damage = newDamage;
        }

        public void SetOwner(GameObject newOwner)
        {
            owner = newOwner;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == owner)
            {
                Destroy(gameObject);
                return;
            }

            IHealth otherHealth = other.GetComponent<IHealth>();

            if (otherHealth != null)
            {
                otherHealth.TakeDamage(damage);
            }

            if (other.tag != "DDP")
            {
                Destroy(gameObject);
                if (deathFXPrefab != null)
                {
                    Instantiate(deathFXPrefab, transform.position, Quaternion.identity, GameObject.Find("Shots Parent").transform);
                }
            }
        }
    }
}