using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Core
{
    public class CreditsTextMover : MonoBehaviour /*Class that moves the credits along the screen.*/
    {
        [SerializeField] private Vector2 velocity; /*The velocity the GameObject should move with.*/

        private Rigidbody2D rb = null; /*The Rigidbody of the GameObject that should move.*/

        private void Awake() /*Get the Rigidbody.*/
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start() /*If the Rigidbody is not null, set its velocity to the variable velocity.*/
        {
            if (velocity == null)
            {
                velocity = new Vector2(0, 0);
            }

            rb.velocity = velocity;
        }
    }
}