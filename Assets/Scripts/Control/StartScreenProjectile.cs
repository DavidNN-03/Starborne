using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Control
{
    public class StartScreenProjectile : MonoBehaviour /*Class that handles the projectiles in the Main Menu scene.*/
    {
        [SerializeField] private Transform forceOrigin; /*This Transform's position will be used as the position of the explosion force applied to the StartScreenButton.*/
        [SerializeField] private GameObject deathFXPrefab; /*This GameObject will be instantiated when the StartScreenProjectile GameObject is destroyed.*/
        [SerializeField] private float explosionForce = 2f; /*The force of the explosion force.*/
        [SerializeField] private float explosionRange = 1f; /*The range of the explosion force.*/
        [SerializeField] private float speed = 1f; /*The speed of the projectile.*/

        public void SetTarget(Vector3 targetPos) /*Set the projectile to aim at a given position.*/
        {
            transform.LookAt(targetPos);
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other) /*When the projectile hits, it will destroy itself if it hit another StartScreenProjectile. Otherwise, it will instantiate the deathFXPrefab, apply force to the other GameObjects Rigidbody if it has one, and it will call the Hit function in the other GameObjects StartScreenButton if it has one. Lastly, this GameObject will be destroyed.*/
        {
            if (other.tag == "DDP") //could also modify the colliders to not detect collisions between the background collider and the projectile
            {
                return;
            }

            if (other.GetComponent<StartScreenProjectile>() != null)
            {
                Destroy(gameObject);
                return;
            }

            if (deathFXPrefab != null)
            {
                Instantiate(deathFXPrefab, transform.position, Quaternion.identity);
            }

            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.AddExplosionForce(explosionForce, forceOrigin.position, explosionRange, 0f, ForceMode.Force);

            StartScreenButton button = other.GetComponentInParent<StartScreenButton>();

            if (button != null)
            {
                button.Hit();
            }

            Destroy(gameObject);
        }
    }
}