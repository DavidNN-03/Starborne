using System.Collections;
using System.Collections.Generic;
using Starborne.UI;
using UnityEngine;

namespace Starborne.Control
{
    public class BehemothController : MonoBehaviour, ILateInit /*This class controls the behemoths. Behemoths simply along a repeating path.*/
    {
        [SerializeField] private float speed; /*The speed at which the behemoth moves.*/
        [SerializeField] private float waypointTolerance; /*How close the behemoth has to be to a waypoint before it starts moving to the next.*/
        [SerializeField] private PatrolPath patrolPath; /*The path that the behemots will follow.*/
        [SerializeField] private GameObject deathFX; /*The GameObject that is instantiated when the behemoth is destroyed.*/
        private Rigidbody rb; /*The behemoth's Rigidbody*/
        private int patrolPathPointIndex; /*The index of the waypoint that the behemoth is moving towards.*/
        private bool hasStarted = false; /*Defines whether or not the behemoth has started moving along its route.*/

        public void LateAwake() /*Gets a reference for the behemoth's Rigidbody.*/
        {
            rb = GetComponent<Rigidbody>();
        }

        public void LateStart() /*Gets the Behemoth's component that implements IHealth and adds the Die function to the onDeath event. Also, sets hasStarted to true.*/
        {
            GetComponent<IHealth>().onDeath += Die;
            hasStarted = true;
        }

        private void Update() /*If the behemoth has started patroling, call PatrolBehaviour*/
        {
            if (!hasStarted) return;

            PatrolBehaviour();
        }

        private void PatrolBehaviour() /*If patrolPath is null, return. Otherwise, call AtWaypoint to see if the behemoth is at a waypoint, call CycleWaypoint to find the next waypoint. Finally, move to the current waypoint.*/
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

        private void CycleWaypoint() /*Increment patrolPathPointIndex. If the value is equal to or higher than the amount of waypoints, reset it to 0.*/
        {
            patrolPathPointIndex++;
            if (patrolPathPointIndex >= patrolPath.transform.childCount)
            {
                patrolPathPointIndex = 0;
            }
        }

        private bool AtWaypoint() /*Returns whether or not the behemoth is within a given distance of the current waypoint. This distance is equal to waypointTolerance.*/
        {
            return Vector3.Distance(transform.position, patrolPath.transform.GetChild(patrolPathPointIndex).position) < waypointTolerance;
        }

        private void Die() /*Instantiate deathFX if it is not null. Find the EnemyPointer class and call EnemyDestroyed. Lastly, destroy the GameObject.*/
        {
            if (deathFX != null) Instantiate(deathFX, transform.position, Quaternion.identity);
            FindObjectOfType<EnemyPointer>().EnemyDestroyed(gameObject);
            Destroy(gameObject);
        }
    }
}