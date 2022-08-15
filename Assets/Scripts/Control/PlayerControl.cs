using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float rollSensitivity = 1f;
    [SerializeField] float pitchSensitivity = 1f;
    [SerializeField] float yawSensitivity = 1f;
    [SerializeField] float speed = 1f;

    [SerializeField] bool invertVertical = true;
    [SerializeField] bool invertHorizontal = false;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }
}
