using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _minDistanceSwipe;
    [SerializeField] private float _speedSwipe;
    [SerializeField] private float _offsetBetweenStripes;
    private Animator _animator;
    private Vector3 _leftPosition;
    private Vector3 _rightPosition;
    private Vector3 _startPosition;
    private Vector3 _nextPosition;
    private Vector3 _currentPosition;
    private bool _move = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _startPosition = transform.position;
        _currentPosition = _startPosition;
        _leftPosition = new Vector3(_startPosition.x - _offsetBetweenStripes, 0, 0);
        _rightPosition = new Vector3(_startPosition.x + _offsetBetweenStripes, 0, 0);
    }

    void FixedUpdate()
    {
        PlayerInput();
        PlayerMove();
    }

    private void PlayerInput()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).deltaPosition.x >= _minDistanceSwipe && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                ChooseNextPosition(_rightPosition);
            }
            else if(Input.GetTouch(0).deltaPosition.x <= -_minDistanceSwipe && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                ChooseNextPosition(_leftPosition);
            }
            else if (Input.GetTouch(0).deltaPosition.x == 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _animator.SetTrigger("Jump");
            }
        }
    }

    private void ChooseNextPosition(Vector3 nextPosition)
    {
        if (_currentPosition == _startPosition)
        {
            Debug.Log("position == startPosition");
            _nextPosition = nextPosition;
            _move = true;
        }
        else if(_currentPosition != _startPosition && nextPosition != _currentPosition)
        {
            Debug.Log("else");

            _nextPosition = _startPosition;
            _move = true;
        }
        else
        {
            Debug.Log("position == nextPosition");

            return;
        }
    }

    private void PlayerMove()
    {
        if(_move == true)
        {
            Debug.Log("MOVE");
            transform.position = Vector3.MoveTowards(transform.position, _nextPosition, _speedSwipe * Time.deltaTime);
        }
        if(transform.position == _nextPosition)
        {
            _move = false;
            _currentPosition = _nextPosition;
        }
    }
}
