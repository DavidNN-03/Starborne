using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.Saving;
using Starborne.Combat;
using Starborne.Core;

namespace Starborne.Control
{
    public class PlayerController : MonoBehaviour
    {
        //roll = z-axis
        //pitch = x-axis
        //yaw = y-axis
        [SerializeField] float maxEnginePower = 5f;
        [SerializeField] float enginePower = 1;
        [SerializeField] float rollSensitivity = 1f;
        [SerializeField] float pitchSensitivity = 1f;
        [SerializeField] float yawSensitivity = 1f;
        [SerializeField] float maxSpeed = 1f;
        float rollInput;
        float pitchInput;
        float yawInput;
        float throttleInput;
        float throttle;

        [SerializeField] Gun[] guns;
        [SerializeField] Character characterStats;

        [SerializeField] bool invertVertical = true;
        [SerializeField] bool invertHorizontal = false;

        Rigidbody rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            characterStats = EssentialObjects.instance.GetComponentInChildren<CharacterHandler>().GetCharacterStats();

            maxSpeed = characterStats.maxSpeed;
            rollSensitivity = characterStats.turnSensitivity;

            foreach (Gun gun in guns)
            {
                gun.SetValues(characterStats.damagePerShot, characterStats.shotsPerSecond);
            }
        }

        private void FixedUpdate()
        {
            float roll = Input.GetAxis("Mouse X");
            float pitch = Input.GetAxis("Mouse Y");
            float yaw = Input.GetAxis("Horizontal");
            float throttle = Input.GetAxis("Vertical");
            Move(roll, pitch, yaw, throttle);

            if (Input.GetMouseButton(0))
            {
                foreach (Gun gun in guns)
                {
                    gun.AttemptFire();
                }
            }
        }

        private void Move(float roll, float pitch, float yaw, float throttle)
        {
            rollInput = roll;
            pitchInput = pitch;
            yawInput = yaw;
            throttleInput = throttle;

            //ClampInputs(); values from Input.GetAxis() should alreadu be between -1 and 1?

            ControlThrottle();

            CalculateForces();

            ClampSpeed();
        }

        private void ClampInputs()
        {
            Debug.Log("rollInput:" + rollInput + "     " + "pitchInput:" + pitchInput + "     " + "yawInput:" + yawInput + "     " + "throttleInput:" + throttleInput);
            rollInput = Mathf.Clamp(rollInput, -1, 1);
            pitchInput = Mathf.Clamp(pitchInput, -1, 1);
            yawInput = Mathf.Clamp(yawInput, -1, 1);
            throttleInput = Mathf.Clamp(throttleInput, -1, 1);
            
        }

        private void ControlThrottle()
        {
            throttle = Mathf.Clamp(throttle + throttleInput*Time.deltaTime, -1, 1);

            enginePower = throttle * maxEnginePower;
        }

        private void CalculateForces()
        {
            Vector3 forces = Vector3.zero;
            forces += enginePower * transform.forward;

            rb.AddForce(forces);
        }

        private void ClampSpeed()
        {
            Vector3 velocity = rb.velocity;

            if (velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
            }
        }
    }
}