using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.GameResources;

namespace Starborne.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 1f; /*The speed of the projectile.*/
        [SerializeField] private float damage; /*The damage the projectile deals on collision.*/
        [SerializeField] private GameObject deathFXPrefab; /*The GameObject that is instantiated when the projectile is destroyed.*/
        [SerializeField] private bool destructibleByProjectiles = false; /*If this value is set to true, the projectile will be destroyed when colliding with any other projectile.*/
        private GameObject owner; /*GameObject that owns the projectile. The projectile will destroy itself if it collides with its owner.*/

        void Awake() /*On Awake, the projectile will find the GameObject's Rigidbody and set it to move forward with the value of the speed variable.*/
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }

        public void SetDamage(float newDamage) /*Sets the damage of the projectile.*/
        {
            damage = newDamage;
        }

        public void SetOwner(GameObject newOwner) /*Sets the owner of the projectile*/
        {
            owner = newOwner;
        }

        private void OnTriggerEnter(Collider other) /*When the projectile collides, it will first check if it has collided 
                                                    with its owner in which case the projectile will be destroyed. 
                                                    Otherwise, it will search for a component that implements IHealth 
                                                    on the other GameObject. If a component is found, it takes damage. 
                                                    The projectile is then set to be destroyed if the other gameObject's tag is not "DDP"
                                                     and if the other GameObject is not a projectile. 
                                                     The projectile will also be destroyed if it can be destroyed by projectiles 
                                                     and the other GameObject is a projectile*/
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

            Projectile otherProjectile = other.GetComponent<Projectile>();

            if (other.tag != "DDP" && otherProjectile == null || otherProjectile != null && destructibleByProjectiles)
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