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
        public float maxRoll;
        float speed;
        float pitch;
        float roll;
        Vector3 camOffset;
        Vector3 camRotation;

        [Header("Y offset")]
        [SerializeField] float maxCamYOffset;
        [SerializeField] float minCamYOffset;
        [SerializeField] float yOffsetPushback;
        [SerializeField] float yOffsetSensitivity;
        [Header("Z offset")]
        [SerializeField] float topSpeedCamZOffset;
        [Header("Field of view")]
        [SerializeField] float maxFOV;
        [SerializeField] float minFOV;
        [Header("Z roll")]
        [SerializeField] float maxCamRoll;
        [SerializeField] float rollPushback;
        [SerializeField] float rollSensitivity;

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
            roll = playerController.roll;

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
            ApplyCamRotation();

            transform.localPosition = startPos + camOffset;
            transform.localEulerAngles = camRotation;
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

        private void ApplyCamRotation()
        {
            camRotation = transform.localEulerAngles;

            bool leftSide = transform.localEulerAngles.z > 0 && transform.localEulerAngles.z <= 180;
            bool rightSide = transform.localEulerAngles.z > 180 && transform.localEulerAngles.z < 360;

            if (Mathf.Abs(camRotation.z) < rollPushback)
            {
                camRotation.z = 0;
            }
            else if (leftSide)
            {
                camRotation.z -= rollPushback;
            }
            else if (rightSide)
            {
                camRotation.z += rollPushback;
            }

            camRotation.z += -1 * roll * rollSensitivity;

            if (leftSide)
            {
                camRotation.z = Mathf.Clamp(camRotation.z, 0, maxCamRoll);
            }
            else if (rightSide)
            {
                camRotation.z = Mathf.Clamp(camRotation.z, 360 - maxCamRoll, 360);
            }

            if (camRotation.z == 360) camRotation.z = 0;
        }
    }
}