using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenShooter : MonoBehaviour
{
    [SerializeField] float maxRaycastDistance = 10f;

    [SerializeField] StartScreenProjectile[] prefabs;
    [SerializeField] Transform[] shooterOriginPoints;

    void Update()
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

    private Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);

    }
}
