using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Core
{
    public class DestroyAfterSeconds : MonoBehaviour
    {
        [SerializeField] float delay = 5f;

        void Start()
        {
            StartCoroutine(DestroyGameObject());
        }

        private IEnumerator DestroyGameObject()
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}