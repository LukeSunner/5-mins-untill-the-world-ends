using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerUnityEvent : MonoBehaviour
{
    public UnityEvent OnTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger.Invoke();
    }
}
