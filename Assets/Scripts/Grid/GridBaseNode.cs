using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBaseNode : MonoBehaviour 
{

    public Vector3 WorldPosition { get => this.transform.position; }
    public Vector2 GridPosition { get; set; }

    public float NodeValue { get => _nodeValue; }

    private SpriteRenderer _spriteRenderer;

    private int _nodeValue;

    public bool Occupied;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Occupied = false;
    }

    public void SetNode(Sprite sprite, int value)
    {
        _spriteRenderer.sprite = sprite;
        _nodeValue = value;
    }

    public override string ToString()
    {
        return "World position is: " + WorldPosition.ToString() + " Grid position is: " + GridPosition.ToString();
    }

    public void OnPlayerEnterNode(int bottomDiceFaceValue)
    {

        if(bottomDiceFaceValue == _nodeValue)
        {

        } else
        {
        }
    }

    public void RemoveOccupation()
    {
        Occupied = false;
    }

}
