using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEventRaiser : MonoBehaviour
{
    public GeneralEvent EventToRaise;


    public void RaiseEvent()
    {
        EventToRaise.Raise();
    }
}
