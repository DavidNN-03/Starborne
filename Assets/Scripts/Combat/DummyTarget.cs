using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.GameResources;
using Starborne.UI;

namespace Starborne.Combat
{
    public class DummyTarget : MonoBehaviour /*This class was developed for testing the setup for the enemy GameObjects.*/
    {
        [SerializeField] private GameObject deathFX; /*Contains the GameObject to be instantiated on the Dummy's death.*/

        private void Start() /*Finds the GameObject's EnemyHealth component and add the Die function to the EnemyHealth's onDeath event.*/
        {
            GetComponent<EnemyHealth>().onDeath += Die;
        }

        private void Die() /*This function is called when the dummy dies. It is called by the GameObject's EnemyHealth component when the Die event is invoked.*/
        {
            if (deathFX != null) Instantiate(deathFX, transform.position, Quaternion.identity);
            FindObjectOfType<EnemyPointer>().EnemyDestroyed(gameObject);
            Destroy(gameObject);
        }
    }
}