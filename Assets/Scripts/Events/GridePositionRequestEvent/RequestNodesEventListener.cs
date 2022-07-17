using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RequestNodesEventListener : MonoBehaviour
{
    public RequestNodesEvent Event;
    public UnityEvent<NodesRequest> Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(NodesRequest nodesRequest)
    { Response?.Invoke(nodesRequest); }
}
