using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBase: MonoBehaviour
{
    public Action DestroyCallback;
    protected bool _playerPresent;

    private void OnDestroy()
    {
        DestroyCallback.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _playerPresent=true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _playerPresent = false;
        }
    }

}
