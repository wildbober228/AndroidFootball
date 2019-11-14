using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isdown = false;
    public UnityEvent action;
    public void OnPointerDown(PointerEventData eventData)
    {
        isdown = true;
        Debug.Log("Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isdown = false;
        Debug.Log("Up");
    }

    private void Update()
    {
        if (isdown)
            action.Invoke();
    }


}
