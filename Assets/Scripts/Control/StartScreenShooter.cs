using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Control
{
    public class StartScreenShooter : MonoBehaviour /*This class handles the raycasting and instantiation of StartScreenProjectiles in the Main Menu scene.*/
    {
        [SerializeField] private float maxRaycastDistance = 10f; /*The max range of the raycast.*/

        [SerializeField] private StartScreenProjectile[] prefabs; /*These prefabs will be instantiated at random whenever the left mouse button is pressed.*/
        [SerializeField] private Transform[] shooterOriginPoints; /*The projectiles will be instantiated at these positions at random.*/

        private void Update() /*When the left mouse button is pressed, raycast in the direction of the cursor. If the raycast hit nothing, return. Otherwise, instantiate a random prefab from prefabs at a random position from shooterOriginPoints and set it to aim at the point where the raycast hit.*/
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pointOfOrigin = shooterOriginPoints[Random.Range(0, shooterOriginPoints.Length)].position;

                RaycastHit hitInfo;
                bool rayHit = Physics.Raycast(GetMouseRay(), out hitInfo, maxRaycastDistance);

                if (!rayHit) return;

                StartScreenProjectile prefab = prefabs[Random.Range(0, prefabs.Length)];
                StartScreenProjectile instance = Instantiate(prefab, pointOfOrigin, Quaternion.identity);

                instance.SetTarget(hitInfo.point);
            }
        }

        private Ray GetMouseRay() /*Returns a Ray based on the cursor's position on the screen.*/
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);

        }
    }
}