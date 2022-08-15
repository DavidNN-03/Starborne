using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenProjectile : MonoBehaviour
{
    [SerializeField] Transform forceOrigin;
    [SerializeField] GameObject deathFXPrefab;
    [SerializeField] float explosionForce = 2f;
    [SerializeField] float explosionRange = 1f;
    public float speed = 1f;

    private void OnTriggerEnter(Collider other)
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

        Rigidbody rb = other    .GetComponent<Rigidbody>();
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
