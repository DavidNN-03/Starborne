using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Control
{
    public class CameraController : MonoBehaviour
    {
        public float maxSpeed;
        public float maxPitch;
        float speed;
        float pitch;
        Vector3 camOffset;

        [SerializeField] float maxCamYOffset;
        [SerializeField] float minCamYOffset;
        [SerializeField] float yOffsetPushback;
        [SerializeField] float yOffsetSensitivity;
        [SerializeField] float topSpeedCamZOffset;
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
        }

        void Update()
        {
            speed = playerController.speed;
            pitch = playerController.pitch;

            if (!Input.GetMouseButton(1))
            {
                float f = Mathf.Lerp(minFOV, maxFOV, Mathf.Max(speed, 0f) / maxSpeed);

                cam.fieldOfView = f;
            }
            else
            {
                cam.fieldOfView = 25;
            }

            ApplySpeedCamOffset();
            ApplyPitchCamOffset();

            transform.localPosition = startPos + camOffset;
        }

        private void ApplySpeedCamOffset()
        {
            camOffset = new Vector3();
            if (speed > 0)
            {
                camOffset.z = Mathf.Lerp(0f, topSpeedCamZOffset, Mathf.Abs(speed) / maxSpeed);
            }
            else if (speed < 0)
            {
                camOffset.z = Mathf.Lerp(0f, -topSpeedCamZOffset, Mathf.Abs(speed) / maxSpeed);
            }
        }

        private void ApplyPitchCamOffset()
        {
            float currentYOffset = transform.localPosition.y - startPos.y;
            float newOffYset = currentYOffset;

            if (Mathf.Abs(currentYOffset) < yOffsetPushback)
            {
                currentYOffset = 0;
            }
            else if (currentYOffset > 0)
            {
                currentYOffset -= yOffsetPushback;
            }
            else if (currentYOffset < 0)
            {
                currentYOffset += yOffsetPushback;
            }

            currentYOffset += pitch * yOffsetSensitivity;

            currentYOffset = Mathf.Clamp(currentYOffset, minCamYOffset, maxCamYOffset);

            camOffset.y = currentYOffset;
        }
    }
}