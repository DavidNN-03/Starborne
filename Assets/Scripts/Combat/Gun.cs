using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Combat
{
    public class Gun : MonoBehaviour /*This class functions as the gun for the player and enemies.*/
    {
        [SerializeField] private float maxSpreadDegrees; /*Sets the max value of the angle between the direction the gun is facing and the direction the projectiles are fired.*/
        private float projectileDamage; /*The damage dealt when the projectile hits.*/
        [SerializeField] private float secondsBetweenShots = 1f; /*The delay between shots.*/
        [SerializeField] public Projectile prefab; /*The GameObject to be instantiated when the gun is fired.*/
        private GameObject projectileParentObject; /*This GameObject will be set as the owner of all instantiated projectiles. This GameObject should be the GameObject with the collider.*/

        private bool canFire = true; /*Defines whether or not the gun is ready to fire.*/

        public void SetDamage(float newDamage) /*Sets projectileDamage to a given value.*/
        {
            projectileDamage = newDamage;
        }

        public void SetRateOfFire(float shotsPerSecond) /*This function assigns a value to secondsBetweenShots. The value is equal to 1 second divided by the amount of shots per second passed as a parameter.*/
        {
            secondsBetweenShots = 1f / shotsPerSecond;
        }

        public void SetMaxSpreadDegrees(float newMaxSpreadDegrees) /*Sets maxSpreadDegrees to a given value.*/
        {
            maxSpreadDegrees = newMaxSpreadDegrees;
        }

        public void SetProjectileParentObject(GameObject newProjectileParent) /*Sets the projectileParentObject to a given GameObject.*/
        {
            projectileParentObject = newProjectileParent;
        }

        public void AttemptFire() /*This function will attempt to fire the gun. If canFire is true, the gun will fire, canFire will be switched to false, and the IEnumerator LoadNextShot will be begun.*/
        {
            if (canFire)
            {
                Fire();
                canFire = false;
                StartCoroutine(LoadNextShot());
            }
        }

        private void Fire() /*This function fires the gun by instantiating an instance of the Projectile prefab. The projectile will be fired in the direction that the gun is facing plus a bit of randomness.*/
        {
            Vector2 spreadDirection = Random.insideUnitCircle.normalized; //Get a random direction for the spread
            Vector3 offsetDirection = new Vector3(transform.right.x * spreadDirection.x, transform.up.y * spreadDirection.y, 0); //Align direction with the gun's direction

            float offsetMagnitude = Random.Range(0f, maxSpreadDegrees);

            Vector3 bulletTrajectory = transform.eulerAngles + (offsetDirection * offsetMagnitude);
            Projectile projectile = Instantiate(prefab, transform.position, Quaternion.Euler(bulletTrajectory), GameObject.Find("Shots Parent").transform);

            projectile.SetDamage(projectileDamage);
            projectile.SetOwner(projectileParentObject);
        }

        private IEnumerator LoadNextShot() /*Sets canFire to true after an amount of time equal to secondsBetweenShots.*/
        {
            yield return new WaitForSeconds(secondsBetweenShots);
            canFire = true;
        }
    }
}