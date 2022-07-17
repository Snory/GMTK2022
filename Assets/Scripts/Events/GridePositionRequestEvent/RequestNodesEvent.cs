using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNodeRequestEvent", menuName = "Events/Grid/NodesRequestEvent", order = 1)]
public class RequestNodesEvent : ScriptableObject
{
	private List<RequestNodesEventListener> listeners =
		new List<RequestNodesEventListener>();

	public void Raise(NodesRequest nodesRequest)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised(nodesRequest);
	}

	public void RegisterListener(RequestNodesEventListener listener)
	{ listeners.Add(listener); }

	public void UnregisterListener(RequestNodesEventListener listener)
	{ listeners.Remove(listener); }
}
