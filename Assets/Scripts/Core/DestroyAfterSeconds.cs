using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Core
{
    public class DestroyAfterSeconds : MonoBehaviour /*Class that destroys the GameObject it is on after a number of seconds.*/
    {
        [SerializeField] private float delay = 5f; /*The amount of seconds before the GameObject should be destroyed.*/

        private void Start() /*Start DestroyGameObject.*/
        {
            StartCoroutine(DestroyGameObject());
        }

        private IEnumerator DestroyGameObject() /*Destroyd the GameObject after a delay.*/
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}