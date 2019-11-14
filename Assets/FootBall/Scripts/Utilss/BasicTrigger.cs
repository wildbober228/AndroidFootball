using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class BasicTrigger : MonoBehaviour
{
    [SerializeField]
    UnityEvent _callback;

    private System.Predicate<Collider> _predicate;
    

    public void AddListener(UnityEvent callback, Predicate<Collider> predicate)
    {
        _callback = callback;
        _predicate = predicate;
    }

    public void RemoveListener()
    {
        _callback = null;
        _predicate = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger was entered by " + other.name);
        if (_predicate.Invoke(other))
            _callback.Invoke();
    }
    
}

