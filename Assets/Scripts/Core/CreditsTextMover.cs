using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Core
{
    public class CreditsTextMover : MonoBehaviour
    {
        [SerializeField] Vector2 velocity;

        Rigidbody2D rb = null;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            if (velocity == null)
            {
                velocity = new Vector2(0, 0);
            }

            rb.velocity = velocity;
        }
    }
}