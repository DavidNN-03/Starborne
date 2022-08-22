using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewRotater : MonoBehaviour
{
    [SerializeField] float degreesPerFrame;

    void Update()
    {
        transform.Rotate(0, degreesPerFrame, 0, Space.World);
    }
}
