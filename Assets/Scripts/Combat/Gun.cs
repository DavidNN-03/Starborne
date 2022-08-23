using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Combat
{
    public class Gun : MonoBehaviour
    {
        float projectileDamage;
        public float secondsBetweenShots = 1f;

        [SerializeField] Projectile prefab;

        bool canFire = true;

        public void SetDamage(float newDamage)
        {
            projectileDamage = newDamage;
        }

        public void SetRateOfFire(float shotsPerSecond)
        {
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
            projectile.SetDamage(projectileDamage);
            projectile.SetOwner(gameObject);
        }

        private IEnumerator LoadNextShot()
        {
            yield return new WaitForSeconds(secondsBetweenShots);
            canFire = true;
        }
    }
}