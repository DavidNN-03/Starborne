using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.GameResources;

namespace Starborne.Combat
{
    public class DummyTarget : MonoBehaviour
    {
        [SerializeField] GameObject deathFX;

        void Start()
        {
            GetComponent<DummyHealth>().onDeath += Die;
        }

        private void Die()
        {
            if (deathFX != null) Instantiate(deathFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}