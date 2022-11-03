using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Starborne.Control
{
    public class StartScreenButton : MonoBehaviour /*Class that manages buttons in the Main Menu scene. An event will be invoked when the buton is hit by a StartScreenProjectile.*/
    {
        [SerializeField] private float invokeDelay = 2f; /*Amount of seconds between hitting the button and invoking the onHit event.*/
        [SerializeField] private UnityEvent onHit; /*This UnityEvent will be invoked after a given delay when the button is hit.*/

        public void Hit() /*Start the process of invoking the event.*/
        {
            StartCoroutine(InvokeEvent());
        }

        private IEnumerator InvokeEvent() /*Invoke the event after the delay.*/
        {
            yield return new WaitForSeconds(invokeDelay);
            onHit.Invoke();
        }
    }
}