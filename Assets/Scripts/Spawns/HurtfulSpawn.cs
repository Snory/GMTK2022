using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtfulSpawn : SpawnBase
{
    [SerializeField]
    private float _lifespan;

    [SerializeField]
    private GeneralEvent _playerHurt;
        
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyRoutine());   
    }

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(_lifespan);
        if (_playerPresent)
        {
            _playerHurt.Raise();
        }
        Destroy(this.gameObject);
    }



}
