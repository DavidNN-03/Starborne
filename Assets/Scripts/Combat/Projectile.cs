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
                Instantiate(deathFXPrefab, transform.position, Quaternion.identity);
            }

            DummyHealth otherHealth = other.GetComponent<DummyHealth>();
            
            if(otherHealth != null)
            {
                other.GetComponent<DummyHealth>().TakeDamage(damage);
            }

            if(other.tag != "DDP")
            {
                Destroy(gameObject);
            }
        }
    }
}