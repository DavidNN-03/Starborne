using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Control
{
    public class PatrolPath : MonoBehaviour /*Class that defines the path for behemoths to follow. The waypoints themselves should be empty child GameObjects.*/
    {
        private const float waypointGizmoRadius = 0.3f; /*Radius of the Gizmos drawn in the Unity Editor.*/

        private void OnDrawGizmos() /*Draw the Gizmos.*/
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextIndex(int i) /*Get index of the next waypoint.*/
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypoint(int i) /*Get position of the waypoint of a given index.*/
        {
            return transform.GetChild(i).position;
        }
    }
}