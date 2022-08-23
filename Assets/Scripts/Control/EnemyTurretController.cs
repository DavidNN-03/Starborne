using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.Combat;
using Starborne.GameResources;

namespace Starborne.Control
{
    public class EnemyTurretController : MonoBehaviour
    {
        [SerializeField] float aimRange = 20f;
        [SerializeField] Transform gunsParent;
        [SerializeField] GameObject deathFX;
        GameObject target;
        Gun[] guns;

        void Awake()
        {
            target = GameObject.FindWithTag("Player");
            guns = GetComponentsInChildren<Gun>();
        }

        void Start()
        {
            GetComponent<EnemyHealth>().onDeath += Die;
        }

        void Update()
        {
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

        bool ClearShotToTarget()
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

        void Die()
        {
            if (deathFX != null) Instantiate(deathFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}