using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Combat
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] bool overwriteDefaultProjectileDamage = false;
        [SerializeField] float projectileDamage;
        [SerializeField] Projectile prefab;

        public float secondsBetweenShots = 1f;
        bool canFire = true;

        public void SetValues(float newDamage, float shotsPerSecond)
        {
            projectileDamage = newDamage;
            secondsBetweenShots = 1f / shotsPerSecond;
        }

        public void AttemptFire()
        {
            if (canFire)
            {
                Fire();
                canFire = false;
                StartCoroutine(LoadNextShot());
            }
        }

        private void Fire()
        {
            Projectile projectile = Instantiate(prefab, transform.position, transform.rotation, GameObject.Find("Shots Parent").transform);
            if (overwriteDefaultProjectileDamage) projectile.SetDamage(projectileDamage);
            projectile.SetOwner(gameObject);
        }

        private IEnumerator LoadNextShot()
        {
            yield return new WaitForSeconds(secondsBetweenShots);
            canFire = true;
        }
    }
}