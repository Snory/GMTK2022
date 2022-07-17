using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private RequestNodesEvent _requestNodesEvent;
    
    [SerializeField]
    private float _spawnTime;

    [SerializeField]
    private GameObject _spawnObject;

    private int _randomValue;

    [SerializeField]
    private bool _spawnAtStart;

    void Start()
    {
        if (_spawnAtStart)
        {
            _randomValue = Random.Range(1, 6);
            _requestNodesEvent.Raise(new NodesRequest(Filter, Spawn));
        } else
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    public IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_spawnTime);
        _randomValue = Random.Range(1, 6);
        _requestNodesEvent.Raise(new NodesRequest(Filter,Spawn));
    }

    public bool Filter(GridBaseNode node)
    {
        if(node.NodeValue == _randomValue && node.Occupied == false)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void Spawn(List<GridBaseNode> nodes)
    {
        foreach (var node in nodes)
        {
            SpawnBase bs = Instantiate(_spawnObject, node.WorldPosition, Quaternion.identity, node.transform).GetComponent<SpawnBase>();
            bs.DestroyCallback = node.RemoveOccupation;
            node.Occupied = true;
        }

        StartCoroutine(SpawnRoutine());
    }

}
