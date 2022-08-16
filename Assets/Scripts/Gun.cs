using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Combat
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] Projectile prefab;

        private float secondsBetweenShots;
        bool canFire = true;

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
            //projectile.SetDamage();
        }

        private IEnumerator LoadNextShot()
        {
            yield return new WaitForSeconds(secondsBetweenShots);
            canFire = true;
        }
    }
}