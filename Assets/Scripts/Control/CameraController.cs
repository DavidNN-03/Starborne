using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Control
{
    public class CameraController : MonoBehaviour /*Controls the camera the player sees through.*/
    {
        public float maxSpeed; /*The max speed the player can move with.*/
        public float maxPitch; /*The max value the player can rotate around its X-axis per frame.*/
        public float maxRoll; /*The max value the player can rotate around its Z-axis per frame.*/
        private float speed; /*The current speed of the player.*/
        private float pitch; /*The current pitch of the player.*/
        private float roll; /*The current roll of the player.*/
        private Vector3 camOffset; /*The amount of offset from the player to the camera.*/
        private Vector3 camRotation; /*The new rotation for the camera.*/

        [Header("Y offset")]
        [SerializeField] private float maxCamYOffset; /*The max amount of offset on its Y-axis. When the camera moves upwards relative to the player.*/
        [SerializeField] private float minCamYOffset; /*The min ammount of offset on its Y-axis. When the camera moves downward relative to the player.*/
        [SerializeField] private float yOffsetPushback; /*The amount of units the camera moves back towards its starting value on its Y-axis.*/
        [SerializeField] private float yOffsetSensitivity; /*The amount of units the camera moves along its Y-axis per frame is equal to this value multiplied by the current value of pitch.*/
        [Header("Z offset")]
        [SerializeField] private float topSpeedCamZOffset; /*The offset that will be applied to the camera's Z-axis at top speed. The faster the player moves, the closer the offset is set to this value.*/
        [Header("Field of view")]
        [SerializeField] private float maxFOV; /*The max value of the camera's field of view. The higher the speed, the larger the field of view.*/
        [SerializeField] private float minFOV; /*The min value of the camera's field of view. The lower the speed, the smaller the field of view.*/
        [SerializeField] private float zoomFOV; /*The value of the camera's field of view when zooming.*/
        [Header("Z roll")]
        [SerializeField] private float maxCamRoll; /*The max amount of rotation that can be applied to the camera's local Z-axis both positively and negatively.*/
        [SerializeField] private float rollPushback; /*The amount that the camera rotates back to its original rotation on its local Z-axis.*/
        [SerializeField] private float rollSensitivity; /*The amount that the camera rotates is equal to this value multiplied by roll.*/

        [SerializeField] private Transform targetTransform; /*The player's Transform.*/
        private PlayerController playerController; /*The PlayerController.*/
        private Camera cam; /*The camera.*/
        private Vector3 startPos; /*The position of the camera relative to the player at start.*/

        private void Awake() /*Find a reference for playerController and cam.*/
        {
            playerController = targetTransform.GetComponent<PlayerController>();
            cam = GetComponent<Camera>();
        }

        private void Start() /*Assign startPos to the position of the camera at start.*/
        {
            startPos = transform.localPosition;
        }

        private void Update() /*First, gets the current values of speed, pitch, and roll from playerController. Secondly, determine the field of view based on whetner or not the right mouse button is down and how fast the player is moving. Lastly, calculate and apply new offset and rotation.*/
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
                cam.fieldOfView = zoomFOV;
            }

            camOffset = new Vector3();
            ApplySpeedCamOffset();
            ApplyPitchCamOffset();
            ApplyCamRotation();

            transform.localPosition = startPos + camOffset;
            transform.localEulerAngles = camRotation;
        }

        private void ApplySpeedCamOffset() /*Calculate the offset on the local Z-axis based on the player's speed.*/
        {
            if (speed > 0)
            {
                camOffset.z = Mathf.Lerp(0f, topSpeedCamZOffset, Mathf.Abs(speed) / maxSpeed);
            }
            else if (speed < 0)
            {
                camOffset.z = Mathf.Lerp(0f, -topSpeedCamZOffset, Mathf.Abs(speed) / maxSpeed);
            }
        }

        private void ApplyPitchCamOffset() /*Calculate the offset on the local Y-axis based on the player's pitch.*/
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

        private void ApplyCamRotation() /*Calculate the rotation of the camera.*/
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