﻿using UnityEngine;
using UnityEngine.EventSystems;

public class rightTouch : EventTrigger
{
    

    public override void OnPointerDown(PointerEventData data)
    {
        Debug.Log("OnPointerDown called.");
    }

   
    public override void OnPointerUp(PointerEventData data)
    {
        Debug.Log("OnPointerUp called.");
    }

   
}

