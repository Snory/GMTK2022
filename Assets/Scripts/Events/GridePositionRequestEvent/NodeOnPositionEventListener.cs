using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NodeOnPositionEventListener : MonoBehaviour
{
    public NodeOnPositionEvent Event;
    public UnityEvent<Vector3, Action<GridBaseNode>> Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(Vector3 position, Action<GridBaseNode> callback)
    { Response?.Invoke(position, callback); }
}
