using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Dice _playerDice;

    [SerializeField]
    private NodeOnPositionEvent _requestGridPosition;
    [SerializeField]
    private NodeOnPositionEvent _requestWorldPosition;

    private bool _moving, _rotating;
    private bool _requested;
    private Vector2 _gridPosition;

    [SerializeField]
    private float _rotateDelayAction;
    [SerializeField]
    private float _moveDelayAction;

    // Start is called before the first frame update
    void Start()
    {
        _requestWorldPosition.Raise(this.transform.position, SetPosition);
        _moving = false;
        _requested = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        RotateInput();
    }

    private void MovementInput()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement.x != 0 && movement.y != 0)
        {
            return;
        }

        if (_requested == false && _moving == false)
        {
            _requested = true;
            Vector2 requestedGridPosition = _gridPosition + (movement);
            _requestGridPosition.Raise(requestedGridPosition, Move);
        }
    }

    public void RotateInput()
    {
        if (_rotating)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(new Vector2(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(new Vector2(1, 0));
        }
    }

    private void Move(GridBaseNode node)
    {
        //pøihlásit se k nodu
        StartCoroutine(MoveROutine(node));
        _requested = false;
    }

    private void Rotate(Vector2 rotate)
    {
        _rotating = true;
        StartCoroutine(RotateRoutine(rotate));
    }

    private void SetPosition(GridBaseNode node)
    {
        this.transform.position = node.WorldPosition;
        _gridPosition = node.GridPosition;
        _requested = false;
    }

    private IEnumerator MoveROutine(GridBaseNode node)
    {
        _moving = true;

        //start anim

        while (_moving)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, node.WorldPosition, 5f * Time.deltaTime);

            if(this.transform.position == node.WorldPosition)
            {
                yield return new WaitForSeconds(_moveDelayAction);
                //evaluate dice
                FinishMove(node);
                break;
            }

            yield return null;
        }
    }

    private IEnumerator RotateRoutine(Vector2 rotate)
    {
        _playerDice.Rotate(rotate);
        yield return new WaitForSeconds(_rotateDelayAction);
        _rotating = false;
    }

    private void FinishMove(GridBaseNode node)
    {
        Vector2 movementMade = node.GridPosition - _gridPosition;
        _playerDice.Move(movementMade);
        node.OnPlayerEnterNode(_playerDice.GetDiceBottomFaceValue());
        //vyvolat událost, odhlásit se
        _gridPosition = node.GridPosition;
        _moving = false;
    }

   




}
