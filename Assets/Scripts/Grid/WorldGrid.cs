using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{

    [SerializeField]
    private Collider2D _gridCollider;

    [SerializeField]
    private GameObject GridNodePrefab;

    private Vector3 _griCenterdWorldPosition;

    private float _width;   

    private float _depth;

    public float Width { get => _width; }
    public float Depth { get => _depth; }

    [SerializeField]
    protected float _nodeSize;
    protected int _gridSizeX, _gridSizeY;

    private GridBaseNode[,] _nodesGrid;

    private void GenerateNodeValues(Dice dice)
    {
        for(int x = 0; x < _gridSizeX; x++)
        {
            for(int y = 0; y < _gridSizeY; y++)
            {
                int value = UnityEngine.Random.Range(0, 6);
                _nodesGrid[x, y].SetNode(dice.FacesSprites[value], value + 1);
            }
        }
    }

    private void RefreshGrid()
    {
        this.transform.position = _gridCollider.bounds.center; 
        _griCenterdWorldPosition = this.transform.position;

        _width = _gridCollider.bounds.size.x;
        _depth = _gridCollider.bounds.size.y;

        _gridSizeX = Mathf.RoundToInt(_width / _nodeSize);
        _gridSizeY = Mathf.RoundToInt(_depth / _nodeSize);

        Vector3 leftBottomNodeCenterWorldPosition = new Vector3(_gridCollider.bounds.min.x + _nodeSize / 2, _gridCollider.bounds.min.y + _nodeSize / 2);


        _nodesGrid = new GridBaseNode[_gridSizeX, _gridSizeY];

        //create nodes
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector3 nodeCenterWorldPosition = leftBottomNodeCenterWorldPosition + (Vector3.right * x * _nodeSize) + (Vector3.up * y * _nodeSize);

                GridBaseNode n = Instantiate(GridNodePrefab, nodeCenterWorldPosition, Quaternion.identity, this.transform).GetComponent<GridBaseNode>();
                n.GridPosition = new Vector2(x, y);
                _nodesGrid[x, y] = n;
            }
        }
    }

    public GridBaseNode GetNodeFromWorldPosition(Vector3 worldPosition)
    {

        //correction to center grid center with world center
        float xCenterCorrection = (_width / 2) - _griCenterdWorldPosition.x;
        float yCenterCorrection = (_depth / 2) - _griCenterdWorldPosition.y;

        float percentageX = (worldPosition.x + xCenterCorrection) / _width;
        float percentageY = (worldPosition.y + yCenterCorrection) / _depth;

        float percentageXClamped = Mathf.Clamp01(percentageX);
        float percentageYClamped = Mathf.Clamp01(percentageY);

        //flor is here in case WorldPosition(0.5,0.5) has to return GridPosition(0,0)

        int xPosition = Mathf.Min((Mathf.FloorToInt(_gridSizeX * percentageXClamped)), _gridSizeX - 1);
        int yPosition = Mathf.Min((Mathf.FloorToInt(_gridSizeY * percentageYClamped)), _gridSizeY - 1);

        GridBaseNode n = _nodesGrid[xPosition, yPosition];

        return n;
    }

    public GridBaseNode GetNodeFromGridPosition(Vector2 gridPosition)
    {
        int checkX = (int)Mathf.Min(gridPosition.x, _gridSizeX - 1);
        int checkY = (int)Mathf.Min(gridPosition.y, _gridSizeY - 1);
        checkX = (int)Mathf.Max(checkX, 0);
        checkY = (int)Mathf.Max(checkY, 0);

        return _nodesGrid[checkX, checkY]; 
    }

    public void OnRequestNodeFromWorldPosition(Vector3 worldPosition, Action<GridBaseNode> callback)
    {
        GridBaseNode n = GetNodeFromWorldPosition(worldPosition);
        callback.Invoke(n);
    }

    public void OnRequestNodeFromGridPosition(Vector3 gridPosition, Action<GridBaseNode> callback)
    {
        GridBaseNode n = GetNodeFromGridPosition(gridPosition);
        callback.Invoke(n);
    }

    public void OnDiceSpawned(Dice dice)
    {
        GenerateNodeValues(dice);
    }

    public void OnRequestNodes(NodesRequest nodesRequest)
    {
        List<GridBaseNode> nodes = new List<GridBaseNode>();
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                GridBaseNode n = _nodesGrid[x, y];

                if (nodesRequest.Filter(n))
                {
                    nodes.Add(n);
                }
            }
        }

        nodesRequest.Callback.Invoke(nodes);

    }

    private void Awake()
    {
        RefreshGrid();
    }


}
