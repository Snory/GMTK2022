using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDiceEvent", menuName = "Events/Dice/DiceEvent", order = 1)]
public class DiceEvent : ScriptableObject
{
	private List<DiceEventListener> listeners =
	  new List<DiceEventListener>();

	public void Raise(Dice dice)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised(dice);
	}

	public void RegisterListener(DiceEventListener listener)
	{ listeners.Add(listener); }

	public void UnregisterListener(DiceEventListener listener)
	{ listeners.Remove(listener); }
}
