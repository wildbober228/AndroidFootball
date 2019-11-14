using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AddEvents : MonoBehaviour
{
    [SerializeField]
    BasicTrigger[] triggers;

   
     [SerializeField]
     UnityEvent[] Actions;

    Predicate<Collider> condition = (p) => p.tag == "Ball";
    void Start()
    {
        SetActionsToTriggers();
    }

    void SetActionsToTriggers()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].AddListener(Actions[i], condition);
        }
    }
}
