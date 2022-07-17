using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Dice : MonoBehaviour
{

    [SerializeField]
    private DiceEvent _diceFacesChanged, _diceSpawned;
  

    public Sprite[] FacesSprites;
    private SpriteRenderer _spriteRendered;

    private int[] _horizontal;
    private int[] _vertical;

    private void Awake()
    {
        _spriteRendered = this.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _horizontal = new int[] { 6, 5, 1, 2 };
        _vertical = new int[] { 6, 3, 1, 4 };
        _diceSpawned.Raise(this);
    }

    public void Move(Vector2 movement)
    {
        movement = movement * new Vector2(-1, 1);

        _horizontal = MovementShift(_horizontal, (int) movement.x);
        ReplaceSharedNumber(movement);
        _vertical = MovementShift(_vertical, (int)movement.y);
        ReplaceSharedNumber(movement);
        _diceFacesChanged.Raise(this);
        _spriteRendered.sprite = FacesSprites[_horizontal[2]-1];
    }

    public void Rotate(Vector2 rotate)
    {
        RotateShift((int) rotate.x);
        //RotateShift((int) rotate.y);
        _diceFacesChanged.Raise(this);
    }

    private void RotateShift(int increment)
    {

        if (increment == 0)
        {
            return;
        }

        int first = _vertical[1];
        //just for the X
        if(increment < 0)
        {
            _vertical[1] = _horizontal[3];
            _horizontal[3] = _vertical[3];
            _vertical[3] = _horizontal[1];
            _horizontal[1] = first;
        } else
        {
            _vertical[1] = _horizontal[1];
            _horizontal[1] = _vertical[3];
            _vertical[3] = _horizontal[3];
            _horizontal[3] = first;
        }
    }


    private int[] MovementShift(int[] array, int increment)
    {
        if(increment == 0)
        {
            return array;
        }

        int from = increment > 0 ? 0 : array.Length -1;
        int to = increment > 0 ? array.Length - 1 : 0;

        int first = array[from];

        while(from != to)
        {
            array[from] = array[from + increment];
            from += increment;
        }

        array[to] = first;

        return array;

    }

    private void ReplaceSharedNumber(Vector2 movement)
    {
        if(movement.x != 0)
        {
            _vertical[2] = _horizontal[2];
            _vertical[0] = _horizontal[0];
          
        }

        if(movement.y != 0)
        {
            _horizontal[2] = _vertical[2];
            _horizontal[0] = _vertical[0];
            
        }
        
    }

    public int[] GetDiceCompassFaceSides()
    {
        return new int[]
        {
            _vertical[1], //north
            _horizontal[2], //middle
            _vertical[3], //south
            _horizontal[1], //west
            _horizontal[3] //east

        };
    }

    public int GetDiceBottomFaceValue()
    {
        return _horizontal[0];
    }

}
