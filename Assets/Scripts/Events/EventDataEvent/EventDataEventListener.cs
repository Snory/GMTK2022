using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDataEventListener : MonoBehaviour
{
    public EventDataEvent Event;
    public UnityEvent<EventData> Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(EventData data)
    { Response?.Invoke(data); }
}
