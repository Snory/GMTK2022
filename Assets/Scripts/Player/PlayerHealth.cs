using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
 
    [SerializeField]
    private int _health;

    [SerializeField]
    private GeneralEvent _gameOver;

    [SerializeField]
    private EventDataEvent _healthChanged;

    private void Start()
    {
        _healthChanged.Raise(new PlayerHealthEventData(_health));
    }

    public void OnPlayerHurt()
    {
        _health--;

        if(_health < 0)
        {
            return;
        }


        _healthChanged.Raise(new PlayerHealthEventData(_health));

        if(_health <= 0)
        {
            _gameOver.Raise();
        }
    }
}
