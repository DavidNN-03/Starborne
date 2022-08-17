using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Combat
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] float projectileDamage;
        [SerializeField] Projectile prefab;

        private float secondsBetweenShots;
        bool canFire = true;

        public void SetProjectileDamage(float newDamage)
        {
            projectileDamage = newDamage;
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
            Projectile projectile = Instantiate(prefab, transform.position, Quaternion.identity);
            projectile.SetDamage(projectileDamage);
        }

        private IEnumerator LoadNextShot()
        {
            yield return new WaitForSeconds(secondsBetweenShots);
            canFire = true;
        }
    }
}