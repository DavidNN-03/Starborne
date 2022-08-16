using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float enginePower = 1;
    [SerializeField] float rollSensitivity = 1f;
    [SerializeField] float pitchSensitivity = 1f;
    [SerializeField] float yawSensitivity = 1f;
    [SerializeField] float maxSpeed = 1f;
    float rollInput;
    float pitchInput;
    float yawInput;
    float throttleInput;


    [SerializeField] bool invertVertical = true;
    [SerializeField] bool invertHorizontal = false;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Character characterStats = FindObjectOfType<CharacterHandler>().GetCharacterStats();

        maxSpeed = characterStats.maxSpeed;
        rollSensitivity = characterStats.turnSensitivity;
    }

    private void FixedUpdate()
    {
        float roll = Input.GetAxis("Mouse X");
        float pitch = Input.GetAxis("Mouse Y");
        float yaw = Input.GetAxis("Horizontal");
        float throttle = Input.GetAxis("Vertical");
        Move(roll, pitch, yaw, throttle);
    }

    private void Move(float roll, float pitch, float yaw, float throttle)
    {
        rollInput = roll;
        pitchInput = pitch;
        yawInput = yaw;
        throttleInput = throttle;

        //ClampInputs(); values from Input.GetAxis() should alreadu be between -1 and 1?

        CalculateForces();

        ClampSpeed();
    }

    private void ClampInputs()
    {
        rollInput = Mathf.Clamp(rollInput, -1, 1);
        pitchInput = Mathf.Clamp(pitchInput, -1, 1);
        yawInput = Mathf.Clamp(yawInput, -1, 1);
        throttleInput = Mathf.Clamp(throttleInput, -1, 1);
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
