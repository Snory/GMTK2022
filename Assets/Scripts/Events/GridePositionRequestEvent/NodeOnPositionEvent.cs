using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewNodeOnPostionRequestEvent", menuName = "Events/Grid/NodeOnPostionRequestEvent", order = 1)]
public class NodeOnPositionEvent : ScriptableObject
{
	private List<NodeOnPositionEventListener> listeners =
	 new List<NodeOnPositionEventListener>();

	public void Raise(Vector3 position, Action<GridBaseNode> callback)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised(position, callback);
	}

	public void RegisterListener(NodeOnPositionEventListener listener)
	{ listeners.Add(listener); }

	public void UnregisterListener(NodeOnPositionEventListener listener)
	{ listeners.Remove(listener); }
}
