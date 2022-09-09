using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.Saving;
using Starborne.Combat;
using Starborne.Core;
using Starborne.GameResources;
using Starborne.SceneHandling;
using Starborne.UI;

namespace Starborne.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] InputConfig inputConfig;

        //roll = z-axis
        //pitch = x-axis
        //yaw = y-axis
        [SerializeField] float maxEnginePower = 5f;
        [SerializeField] float enginePower = 1;
        [SerializeField] float rollSensitivity = 1f;
        [SerializeField] float pitchSensitivity = 1f;
        [SerializeField] float yawSensitivity = 1f;
        [SerializeField] float rollEffectivenessAtMaxSpeed;
        [SerializeField] float pitchEffectivenessAtMaxSpeed;
        [SerializeField] float yawEffectivenessAtMaxSpeed;
        public float maxSpeed = 1f;
        [SerializeField] float baseSpeed = 1f;
        [SerializeField] float dampeningSpeedChange = 1f;
        [SerializeField] float changeSceneOnDeathDelay = 1f;
        public float speed;
        public float roll;
        public float pitch;
        public float yaw;
        public float throttle;

        [Tooltip("-No dampening: The player's speed will stay constant when throttle is 0 \n-Dampening base speed: Player will accelerate to base speed when throttle is 0 \n-Dampening static speed: Player will accelerate to speed=0 when throttle is 0")]
        [SerializeField] MovementType movementType = MovementType.noDampening;
        [SerializeField] Transform gunsParent;
        [SerializeField] Gun gunPrefab;
        [SerializeField] Gun[] guns;
        [SerializeField] Character characterStats;
        [SerializeField] GameObject deathFX;

        GameUI gameUI;
        Rigidbody rb;
        PlayerHealth health;
        MeshFilter meshFilter;
        MeshRenderer meshRenderer;

        void Awake()
        {
            gameUI = FindObjectOfType<GameUI>();
            rb = GetComponent<Rigidbody>();
            health = GetComponent<PlayerHealth>();
            meshFilter = GetComponentInChildren<MeshFilter>();
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            characterStats = EssentialObjects.instance.GetComponentInChildren<CharacterHandler>().GetCharacterStats();

            health.onDeath += Die;

            maxSpeed = characterStats.maxSpeed;
            baseSpeed = characterStats.baseSpeed;
            dampeningSpeedChange = characterStats.dampeningSpeedChange;
            maxEnginePower = characterStats.enginePower;
            rollSensitivity = characterStats.rollSensitivity;
            pitchSensitivity = characterStats.pitchSensitivity;
            yawSensitivity = characterStats.yawSensitivity;
            rollEffectivenessAtMaxSpeed = characterStats.rollEffectivenessAtMaxSpeed;
            pitchEffectivenessAtMaxSpeed = characterStats.pitchEffectivenessAtMaxSpeed;
            yawEffectivenessAtMaxSpeed = characterStats.yawEffectivenessAtMaxSpeed;

            guns = new Gun[characterStats.gunPositions.Length];

            for (int i = 0; i < characterStats.gunPositions.Length; i++)
            {
                Gun gun = Instantiate(gunPrefab, characterStats.gunPositions[i] + transform.position, Quaternion.identity, gunsParent);
                guns[i] = gun;
                gun.SetDamage(characterStats.damagePerShot);
                gun.SetRateOfFire(characterStats.shotsPerSecond);
            }

            meshFilter.mesh = Resources.Load<Mesh>("Meshes/" + characterStats.meshFileName);
            meshRenderer.material = Resources.Load<Material>("Materials/" + characterStats.materialFileName);
            meshFilter.transform.localScale = characterStats.meshScale;

            gameUI.UpdateDampeningText(movementType);
        }

        private void FixedUpdate() //consider using Update() for better responsiveness or check the set framerate for FixedUpdate()
        {
            if (Input.GetKeyDown("z")) CycleMovementType();
            /*if (Input.GetKeyDown("1"))
            {
                movementType = MovementType.noDampening;
                gameUI.UpdateDampeningText(movementType);
            }
            else if (Input.GetKeyDown("2"))
            {
                movementType = MovementType.dampenedBaseSpeed;
                gameUI.UpdateDampeningText(movementType);
            }
            else if (Input.GetKeyDown("3"))
            {
                movementType = MovementType.dampenedStatic;
                gameUI.UpdateDampeningText(movementType);
            }*/

            Move();

            if (Input.GetMouseButton(0))
            {
                foreach (Gun gun in guns)
                {
                    gun.AttemptFire();
                }
            }

            gameUI.UpdateSpeedText(speed, maxSpeed);
        }

        private void CycleMovementType()
        {
            if (movementType == MovementType.noDampening)
            {
                movementType = MovementType.dampenedBaseSpeed;
            }
            else if (movementType == MovementType.dampenedBaseSpeed)
            {
                movementType = MovementType.dampenedStatic;
            }
            else if (movementType == MovementType.dampenedStatic)
            {
                movementType = MovementType.noDampening;
            }
            gameUI.UpdateDampeningText(movementType);
        }

        private void Move()
        {
            GetInputs();

            InvertInputs();

            ClampInputs();

            //ApplySensitivity();

            ControlVelocityDirection();

            ControlThrottle();

            CalculateTorque();

            SetSpeed();

            ModifySpeed();
        }

        private void GetInputs()
        {
            roll = GetInputFromInputSet(inputConfig.rollInputSet);
            pitch = GetInputFromInputSet(inputConfig.pitchInputSet);
            yaw = GetInputFromInputSet(inputConfig.yawInputSet);
            throttle = GetInputFromInputSet(inputConfig.throttleInputSet);
        }

        float GetInputFromInputSet(InputSet inputSet)
        {
            if (inputSet == InputSet.mouseX)
            {
                return Input.GetAxis("Mouse X");
            }
            else if (inputSet == InputSet.mouseY)
            {
                return Input.GetAxis("Mouse Y");
            }
            else if (inputSet == InputSet.wAndS)
            {
                return Input.GetAxis("Vertical");

            }
            else if (inputSet == InputSet.aAndD)
            {
                return Input.GetAxis("Horizontal");
            }
            else if (inputSet == InputSet.eAndQ)
            {
                return Input.GetAxis("Roll");
            }
            return 0f;
        }

        void InvertInputs()
        {
            if (inputConfig.inverThrottle)
            {
                throttle *= -1f;
            }
            if (inputConfig.invertRoll)
            {
                roll *= -1f;
            }
            if (inputConfig.invertPitch)
            {
                pitch *= -1f;
            }
            if (inputConfig.invertYaw)
            {
                yaw *= -1f;
            }
        }

        private void ClampInputs()
        {
            roll = Mathf.Clamp(roll, -1, 1);
            pitch = Mathf.Clamp(pitch, -1, 1);
            yaw = Mathf.Clamp(yaw, -1, 1);
            throttle = Mathf.Clamp(throttle, -1, 1);
        }

        void ApplySensitivity()
        {
            roll *= rollSensitivity;
            pitch *= pitchSensitivity;
            yaw *= yawSensitivity;
        }

        private void ControlVelocityDirection()
        {
            float speed = rb.velocity.magnitude;
            rb.velocity = transform.forward * speed;
        }

        private void ControlThrottle()
        {
            enginePower = throttle * maxEnginePower * Time.deltaTime;
        }

        private void CalculateTorque()
        {
            //float rollEffectiveness = 1 - rollEffectivenessAtMaxSpeed * (Mathf.Abs(speed) / maxSpeed);
            //float pitchEffectiveness = 1 - pitchEffectivenessAtMaxSpeed * (Mathf.Abs(speed) / maxSpeed);
            //float yawEffectiveness = 1 - yawEffectivenessAtMaxSpeed * (Mathf.Abs(speed) / maxSpeed);

            float rollEffectiveness = Mathf.Lerp(1, rollEffectivenessAtMaxSpeed, Mathf.Abs(speed) / maxSpeed);
            float pitchEffectiveness = Mathf.Lerp(1, pitchEffectivenessAtMaxSpeed, Mathf.Abs(speed) / maxSpeed);
            float yawEffectiveness = Mathf.Lerp(1, yawEffectivenessAtMaxSpeed, Mathf.Abs(speed) / maxSpeed);


            pitch *= pitchSensitivity * pitchEffectiveness;
            yaw *= yawSensitivity * yawEffectiveness;
            roll *= rollSensitivity * rollEffectiveness;

            transform.Rotate(pitch, yaw, roll, Space.Self);
        }

        private void SetSpeed()
        {
            speed += enginePower;

            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

            rb.velocity = speed * transform.forward;
        }

        private void ModifySpeed() //Consider refactoring
        {
            if (movementType == MovementType.noDampening || throttle != 0)
            {
                return;
            }
            else if (movementType == MovementType.dampenedBaseSpeed)
            {
                float speedChange = dampeningSpeedChange * Time.deltaTime;
                float speedDifference = baseSpeed - speed;

                if (Mathf.Abs(speedDifference) < speedChange)
                {
                    speed = baseSpeed;
                    return;
                }

                if (speedDifference < 0)
                {
                    speed -= dampeningSpeedChange * Time.deltaTime;
                }
                else
                {
                    speed += dampeningSpeedChange * Time.deltaTime;
                }
            }
            else if (movementType == MovementType.dampenedStatic)
            {
                float speedChange = dampeningSpeedChange * Time.deltaTime;
                if (Mathf.Abs(speed) < speedChange)
                {
                    speed = 0;
                    return;
                }

                if (speed > 0)
                {
                    speed -= dampeningSpeedChange * Time.deltaTime;
                }
                else
                {
                    speed += dampeningSpeedChange * Time.deltaTime;
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            Die();
            enabled = false;
        }

        private void Die()
        {
            if (deathFX != null) Instantiate(deathFX, transform.position, Quaternion.identity);
            Cursor.lockState = CursorLockMode.None;
            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();
            sceneHandler.LoadScene(sceneHandler.charSelectSceneIndex, changeSceneOnDeathDelay);
        }
    }
}