using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartScreenButton : MonoBehaviour
{
    [SerializeField] float invokeDelay = 2f;
    [SerializeField] UnityEvent onHit;

    public void Hit()
    {
        StartCoroutine(InvokeEvent());
    }

    private IEnumerator InvokeEvent()
    {
        yield return new WaitForSeconds(invokeDelay);
        onHit.Invoke();
    }
}
