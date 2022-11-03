using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.Saving;
using Starborne.Combat;
using Starborne.Core;
using Starborne.GameResources;
using Starborne.SceneHandling;
using Starborne.UI;
using Starborne.Mission;

namespace Starborne.Control
{
    public class PlayerController : MonoBehaviour /*Controls the player character.*/
    {
        [SerializeField] private InputConfig inputConfig; /*How the player character should take input.*/
        [SerializeField] private float maxEnginePower = 5f; /*How much power can be applied to the player character.*/
        [SerializeField] private float enginePower; /*The amount of power currrently being applied to the player character.*/
        [SerializeField] private float rollSensitivity = 1f; /*Sensitivity for rotating the player character on its Z-axis.*/
        [SerializeField] private float pitchSensitivity = 1f; /*Sensitivity for rotating the player character on its X-axis.*/
        [SerializeField] private float yawSensitivity = 1f; /*Sensitivity for rotating the player character on its Y-axis.*/
        [SerializeField] private float rollEffectivenessAtMaxSpeed; /*How effectively the player can rotate on its Z-axis when moving at max speed. The faster the player moves, the slower the player rotates - assuming that this value is between higher than 0 and lower than 1.*/
        [SerializeField] private float pitchEffectivenessAtMaxSpeed; /*How effectively the player can rotate on its X-axis when moving at max speed. The faster the player moves, the slower the player rotates - assuming that this value is between higher than 0 and lower than 1.*/
        [SerializeField] private float yawEffectivenessAtMaxSpeed; /*How effectively the player can rotate on its Y-axis when moving at max speed. The faster the player moves, the slower the player rotates - assuming that this value is between higher than 0 and lower than 1.*/
        public float maxSpeed = 1f; /*The max speed at which the player can move.*/
        [SerializeField] private float baseSpeed = 1f; /*The speed the player will accelerate towards if movementType is set to MovementType.dampenedBaseSpeed.*/
        [SerializeField] private float dampeningSpeedChange = 1f; /*How quickly the character's speed changes due to dampening.*/
        [SerializeField] private float gunMaxSpreadDegrees; /*Max amount of spread in degrees when firing the guns.*/
        [SerializeField] private float changeSceneOnDeathDelay = 1f; /*Amount of seconds between the player dying and loading the game over scene.*/
        public float speed; /*Player's current speed.*/
        public float roll; /*Rotation to be applied on the player's Z-axis.*/
        public float pitch; /*Rotation to be applied on the player's X-axis.*/
        public float yaw; /*Rotation to be applied on the player's Y-axis.*/
        public float throttle; /*Input from the player on if and how they want to accelerate.*/

        [Tooltip("-No dampening: The player's speed will stay constant when throttle is 0 \n-Dampening base speed: Player will accelerate to base speed when throttle is 0 \n-Dampening static speed: Player will accelerate to speed=0 when throttle is 0")]
        [SerializeField] private MovementType movementType = MovementType.noDampening; /*How the player's speed should change when no buttons for acceleration are being pushed.*/
        [SerializeField] private Transform gunsParent; /*The GameObject that will be the parent of the guns.*/
        [SerializeField] private Gun gunPrefab; /*The gun prefab that will be instantiated.*/
        [SerializeField] private Gun[] guns; /*Array of all the instantiated guns.*/
        [SerializeField] private Character characterStats; /*The chosen character's stats.*/
        [SerializeField] private GameObject deathFX; /*The GameObject to be instantiated when the player dies.*/

        private GameUI gameUI; /*The component that controls the player's UI.*/
        private Rigidbody rb; /*The player GameObject's Rigidbody.*/
        private PlayerHealth health; /*The player's health component.*/
        private MeshFilter meshFilter; /*The player GameObject's MeshFilter.*/
        private MeshRenderer meshRenderer; /*The player GameObject's MeshRenderer.*/

        private void Awake() /*Find values for gameUI, rb, health, meshFilter, and meshRenderer.*/
        {
            gameUI = FindObjectOfType<GameUI>();
            rb = GetComponent<Rigidbody>();
            health = GetComponentInChildren<PlayerHealth>();
            meshFilter = GetComponentInChildren<MeshFilter>();
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        private void Start() /*Lock the mouse, get the character stats, add the Die funtion to health's onDeath event, set values in the CameraController, MeshFilter, MeshFilter, and MeshCollider, update the UI, and the guns are instantiated and their values are set.*/
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

            CameraController cc = GetComponentInChildren<CameraController>();
            cc.maxSpeed = characterStats.maxSpeed;
            cc.maxPitch = characterStats.pitchSensitivity;
            cc.maxRoll = characterStats.rollSensitivity;

            Mesh playerMesh = Resources.Load<Mesh>("Meshes/" + characterStats.meshFileName);
            meshFilter.mesh = playerMesh;
            meshRenderer.material = Resources.Load<Material>("Materials/" + characterStats.materialFileName);
            meshFilter.transform.localScale = characterStats.meshScale;
            MeshCollider playerCollider = meshRenderer.gameObject.AddComponent<MeshCollider>();
            playerCollider.convex = true;
            playerCollider.sharedMesh = playerMesh;

            gameUI.UpdateDampeningText(movementType);

            guns = new Gun[characterStats.gunPositions.Length];

            for (int i = 0; i < characterStats.gunPositions.Length; i++)
            {
                Gun gun = Instantiate(gunPrefab, characterStats.gunPositions[i] + transform.position, Quaternion.identity, gunsParent);
                guns[i] = gun;
                gun.SetDamage(characterStats.damagePerShot);
                gun.SetRateOfFire(characterStats.shotsPerSecond);
                gun.SetMaxSpreadDegrees(gunMaxSpreadDegrees);
                gun.SetProjectileParentObject(playerCollider.gameObject);
            }
        }

        private void FixedUpdate() /*Check if the Z key is pressed, if so, call CycleMovementType. If the left mouse button is down, call AttemptFire on all Guns in guns. Update speedometer by calling UpdateSpeedText on gameUI.*/
        {
            if (Input.GetKeyDown("z")) CycleMovementType();

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

        private void CycleMovementType() /*Change the MovementType and update the UI accordingly.*/
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

        private void Move() /*Get input from the player, invert it if necessary, clamp it, set the movement to be forwards, and calculaate and add acceleration.*/
        {
            GetInputs();

            InvertInputs();

            ClampInputs();

            ControlVelocityDirection();

            ControlThrottle();

            CalculateTorque();

            SetSpeed();

            ModifySpeed();
        }

        private void GetInputs() /*Get input with GetInputFromInputSet based on the inputConfig.*/
        {
            roll = GetInputFromInputSet(inputConfig.rollInputSet);
            pitch = GetInputFromInputSet(inputConfig.pitchInputSet);
            yaw = GetInputFromInputSet(inputConfig.yawInputSet);
            throttle = GetInputFromInputSet(inputConfig.throttleInputSet);
        }

        private float GetInputFromInputSet(InputSet inputSet) /*Return input of a given InputSet.*/
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

        private void InvertInputs() /*Invert the inputs if defined by the inputConfig.*/
        {
            if (inputConfig.invertThrottle)
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

        private void ClampInputs() /*Clamp the inputs between -1 and 1.*/
        {
            roll = Mathf.Clamp(roll, -1, 1);
            pitch = Mathf.Clamp(pitch, -1, 1);
            yaw = Mathf.Clamp(yaw, -1, 1);
            throttle = Mathf.Clamp(throttle, -1, 1);
        }

        private void ControlVelocityDirection() /*Set the velocity to be completely along its local Z-axis.*/
        {
            float speed = rb.velocity.magnitude;
            rb.velocity = transform.forward * speed;
        }

        private void ControlThrottle() /*Calcluate enginePower.*/
        {
            enginePower = throttle * maxEnginePower * Time.deltaTime;
        }

        private void CalculateTorque() /*Calculate and apply rotation.*/
        {
            float rollEffectiveness = Mathf.Lerp(1, rollEffectivenessAtMaxSpeed, Mathf.Abs(speed) / maxSpeed);
            float pitchEffectiveness = Mathf.Lerp(1, pitchEffectivenessAtMaxSpeed, Mathf.Abs(speed) / maxSpeed);
            float yawEffectiveness = Mathf.Lerp(1, yawEffectivenessAtMaxSpeed, Mathf.Abs(speed) / maxSpeed);


            pitch *= pitchSensitivity * pitchEffectiveness;
            yaw *= yawSensitivity * yawEffectiveness;
            roll *= rollSensitivity * rollEffectiveness;

            transform.Rotate(pitch, yaw, roll, Space.Self);
        }

        private void SetSpeed() /*Add engine power to speed, clamp the speed and set the velocity.*/
        {
            speed += enginePower;

            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

            rb.velocity = speed * transform.forward;
        }

        private void ModifySpeed() /*Accelerate depending on the throttle and movementType.*/
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

        private void OnCollisionEnter(Collision collision) /*When the player collides with anything, the player dies immediatly. The player finds PlayerHealth and calls Collision.*/
        {
            GetComponentInChildren<PlayerHealth>().Collision();
            enabled = false;
        }

        private void Die() /*Instantiate the deathFX if it isnt null, find the OptionalAssignmentHandler and call CaptureData and SetLevelWon(false). Also, unlock the mouse and load the game-over scene.*/
        {
            if (deathFX != null) Instantiate(deathFX, transform.position, Quaternion.identity);
            OptionalAssignmentHandler optionalAssignmentHandler = FindObjectOfType<OptionalAssignmentHandler>();
            optionalAssignmentHandler.CaptureData();
            optionalAssignmentHandler.SetLevelWon(false);
            Cursor.lockState = CursorLockMode.None;
            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();
            sceneHandler.LoadScene(sceneHandler.gameOverSceneIndex, changeSceneOnDeathDelay);
        }
    }
}