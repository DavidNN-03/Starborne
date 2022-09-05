using System.Collections;
using System.Collections.Generic;
using Starborne.UI;
using UnityEngine;

namespace Starborne.Control
{
    public class BehemothController : MonoBehaviour, ILateInit
    {
        [SerializeField] float speed;
        [SerializeField] float waypointTolerance;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] GameObject deathFX;
        Rigidbody rb;
        int patrolPathPointIndex;
        bool hasStarted = false;

        public void LateAwake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void LateStart()
        {
            GetComponent<IHealth>().onDeath += Die;
            hasStarted = true;
        }

        void Update()
        {
            if (!hasStarted) return;

            PatrolBehaviour();
        }

        private void PatrolBehaviour()
        {
            if (patrolPath == null) return;

            if (AtWaypoint())
            {
                CycleWaypoint();
            }

            Vector3 direction = patrolPath.transform.GetChild(patrolPathPointIndex).position - transform.position;
            direction.Normalize();

            rb.velocity = direction * speed;
        }

        private void CycleWaypoint()
        {
            patrolPathPointIndex++;
            if (patrolPathPointIndex >= patrolPath.transform.childCount)
            {
                patrolPathPointIndex = 0;
            }
        }

        private bool AtWaypoint()
        {
            return Vector3.Distance(transform.position, patrolPath.transform.GetChild(patrolPathPointIndex).position) < waypointTolerance;
        }

        private void Die()
        {
            if (deathFX != null) Instantiate(deathFX, transform.position, Quaternion.identity);
            FindObjectOfType<EnemyPointer>().EnemyDestroyed(gameObject);
            Destroy(gameObject);
        }
    }
}