using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Combat
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] float maxSpread;
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
            Vector2 spreadDirection = Random.insideUnitCircle.normalized; //Get a random direction for the spread
            Vector3 offsetDirection = new Vector3(transform.right.x * spreadDirection.x, transform.up.y * spreadDirection.y, 0); //Align direction with fps cam direction

            float offsetMagnitude = Random.Range(0f, maxSpread); //Get a random offset amount
            offsetMagnitude = Mathf.Tan(offsetMagnitude); //Convert to segment length so we get desired degrees value
            Vector3 bulletTrajectory = transform.eulerAngles + (offsetDirection * offsetMagnitude); //Add our offset to our forward vector

            Projectile projectile = Instantiate(prefab, transform.position, Quaternion.Euler(bulletTrajectory), GameObject.Find("Shots Parent").transform);
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