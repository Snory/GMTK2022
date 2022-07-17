using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEventDataEvent", menuName = "Events/EventDataEvent/EventDataEvent", order = 1)]
public class EventDataEvent : ScriptableObject
{
	private List<EventDataEventListener> listeners =
		new List<EventDataEventListener>();

	public void Raise(EventData data)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised(data);
	}

	public void RegisterListener(EventDataEventListener listener)
	{ listeners.Add(listener); }

	public void UnregisterListener(EventDataEventListener listener)
	{ listeners.Remove(listener); }
}
