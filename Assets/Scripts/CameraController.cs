using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Control
{
    public class CameraController : MonoBehaviour
    {
        float maxSpeed;
        float speed;
        float camOffset;
        [SerializeField] float topSpeedCamOffset;
        [SerializeField] float maxFOV;
        [SerializeField] float minFOV;
        [SerializeField] Transform targetTransform;
        PlayerController playerController;
        Camera cam;
        Vector3 startPos;

        void Awake()
        {
            playerController = targetTransform.GetComponent<PlayerController>();
            cam = GetComponent<Camera>();
        }

        void Start()
        {
            startPos = transform.localPosition;
            maxSpeed = playerController.maxSpeed;
        }

        void Update()
        {
            speed = playerController.speed;

            float f = Mathf.Lerp(minFOV, maxFOV, Mathf.Max(speed, 0f) / maxSpeed);

            Debug.Log(f);

            cam.fieldOfView = f;

            Debug.Log(cam.fieldOfView);

            if (speed > 0)
            {
                camOffset = -Mathf.Lerp(0, topSpeedCamOffset, speed / maxSpeed);
            }
            else if (speed < 0)
            {
                camOffset = -Mathf.Lerp(-topSpeedCamOffset, 0, Mathf.Abs(speed) / maxSpeed);
            }

            transform.localPosition = startPos + new Vector3(0, 0, camOffset);
        }
    }
}