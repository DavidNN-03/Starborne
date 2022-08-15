using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenShooter : MonoBehaviour
{
    [SerializeField] float maxRaycastDistance = 10f;

    [SerializeField] StartScreenProjectile prefab;
    [SerializeField] Transform[] shooterOriginPoints;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pointOfOrigin = shooterOriginPoints[Random.Range(0, shooterOriginPoints.Length)].position;


            RaycastHit hitInfo;
            bool rayHit = Physics.Raycast(GetMouseRay(), out hitInfo, maxRaycastDistance);

            if (!rayHit) return;

            StartScreenProjectile instance = Instantiate(prefab, pointOfOrigin, Quaternion.identity);

            instance.transform.LookAt(hitInfo.point);
            instance.GetComponent<Rigidbody>().velocity = instance.transform.forward * instance.speed;
        }
    }

    private Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
