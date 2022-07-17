using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiceEventListener : MonoBehaviour
{
    public DiceEvent Event;
    public UnityEvent<Dice> Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(Dice dice)
    { Response?.Invoke(dice); }
}
