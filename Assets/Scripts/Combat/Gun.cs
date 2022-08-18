using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Combat
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] float projectileDamage;
        [SerializeField] Projectile prefab;
        Transform shotsParent;

        public float secondsBetweenShots = 1f;
        bool canFire = true;

        private void Awake()
        {
            shotsParent = GameObject.Find("Shots Parent").transform;
        }

        public void SetValues(float newDamage, float shotsPerSecond)
        {
            projectileDamage = newDamage;
            secondsBetweenShots = 1f/shotsPerSecond;
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
            Projectile projectile = Instantiate(prefab, transform.position, transform.rotation, shotsParent);
            projectile.SetDamage(projectileDamage);
        }

        private IEnumerator LoadNextShot()
        {
            yield return new WaitForSeconds(secondsBetweenShots);
            canFire = true;
        }
    }
}