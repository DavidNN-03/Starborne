using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.GameResources;
using Starborne.UI;

namespace Starborne.Combat
{
    public class DummyTarget : MonoBehaviour
    {
        [SerializeField] GameObject deathFX;

        void Start()
        {
            GetComponent<EnemyHealth>().onDeath += Die;
        }

        private void Die()
        {
            if (deathFX != null) Instantiate(deathFX, transform.position, Quaternion.identity);
            FindObjectOfType<EnemyPointer>().EnemyDestroyed(gameObject);
            Destroy(gameObject);
        }
    }
}