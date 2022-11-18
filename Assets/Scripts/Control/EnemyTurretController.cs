using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.Combat;
using Starborne.UI;

namespace Starborne.Control
{
    public class EnemyTurretController : MonoBehaviour, ILateInit /*Class that controls the enemy turrets. The turrets will aim and shoot at the player when it is within range.*/
    {
        [SerializeField] float aimRange = 20f; /*The distance within the player is aimed and shot at.*/
        [SerializeField] float projectileDamage = 5f; /*The amount of damage each projectile deals.*/
        [SerializeField] float shotsPerSecond; /*The amout of shots each gun fires per second.*/
        [SerializeField] float gunMaxSpreadDegrees; /*The max amount of spread on the gun in degrees.*/
        [SerializeField] Transform gunsParent; /*The parent object of the guns. This GameObject will be rotated to look at the player-*/
        [SerializeField] GameObject deathFX; /*This gameObject will be instantiated when the turret is destroyed.*/
        GameObject target; /*The turret's target. This will be set to the player.*/
        Gun[] guns; /*Array of all the guns that will be fired when the player is within range.*/

        bool hasStarted = false; /*Defines whether or not the turret has started going. This value will be set to true in LateStart.*/

        public void LateAwake() /*Sets target to the player and guns to all the Guns in the GameObject and its descendants.*/
        {
            target = GameObject.FindWithTag("Player");
            guns = GetComponentsInChildren<Gun>();
        }

        public void LateStart() /*Gets the component that implements IHealth and adds the Die function to the onDeath event. Set the damage, rate of fire, and max spread of all the guns. Set hasStarted to true.*/
        {
            GetComponent<IHealth>().onDeath += Die;

            foreach (Gun gun in guns)
            {
                if (gun.prefab == null)
                {
                    Destroy(gun.transform.parent.parent.parent);
                }
                gun.SetDamage(projectileDamage);
                gun.SetRateOfFire(shotsPerSecond);
                gun.SetMaxSpreadDegrees(gunMaxSpreadDegrees);
            }

            hasStarted = true;
        }

        void Update() /*Returns immediatly if hasStarted is false or if the player is out of range. Otherwise, ClearShotToTarget is called. If it returns true, AttemptFire will be called on all the guns.*/
        {
            if (!hasStarted) return;

            if (Vector3.Distance(transform.position, target.transform.position) > aimRange) return;
            gunsParent.LookAt(target.transform.position);
            if (ClearShotToTarget())
            {
                foreach (Gun gun in guns)
                {
                    gun.AttemptFire();
                }
            }
        }

        bool ClearShotToTarget() /*This function returns whether or not the turret has a clear shot at the player. It does so by raycasting in the direction of the player, sorting the hits by distance, and checking the closest hit. If this hit is the player, then it has a clear shot and returns true.*/
        {
            Vector3 directionToTarget = target.transform.position - transform.position;
            RaycastHit[] hits = Physics.RaycastAll(transform.position, directionToTarget, aimRange);
            float[] distances = new float[hits.Length];

            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            Array.Sort(distances, hits);

            return hits[0].transform.gameObject == target;
        }

        void Die() /*Instantiate deathFX if it isn't null, find the EnemyPointer and call EnemyDestroyed, and destroy the GameObject.*/
        {
            if (deathFX != null) Instantiate(deathFX, transform.position, Quaternion.identity);
            if (gameObject.tag == "Enemy") FindObjectOfType<EnemyPointer>().EnemyDestroyed(gameObject);
            Destroy(gameObject);
        }

        void OnDrawGizmosSelected() /*Draw a wire sphere of the range of the turret.*/
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, aimRange);
        }
    }
}