using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _minDistanceSwipe;
    [SerializeField] private float _speedSwipe;
    [SerializeField] private Vector3 _offsetBetweenStripes;
    private Animator _animator;
    private Vector3 _currentPosition;
    private Vector3 _previousPosition;
    private Vector3 _startPosition;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentPosition = transform.position;
        _previousPosition = transform.position;
        _startPosition = transform.position;
    }

    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).deltaPosition.x >= _minDistanceSwipe && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _previousPosition = _currentPosition;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + _offsetBetweenStripes, _speedSwipe);
                _currentPosition = transform.position;
            }
            else if(Input.GetTouch(0).deltaPosition.x <= -_minDistanceSwipe && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _previousPosition = _currentPosition;
                transform.position = Vector3.MoveTowards(transform.position, transform.position - _offsetBetweenStripes, _speedSwipe);
                _currentPosition = transform.position;
            }
            else if (Input.GetTouch(0).deltaPosition.x == 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _animator.SetTrigger("Jump");
            }

            CheckPosition();
        }
    }

    private void CheckPosition()
    {
        if(_startPosition.x + _offsetBetweenStripes.x + 1 < _currentPosition.x || _startPosition.x - _offsetBetweenStripes.x - 1 > _currentPosition.x)
        {
            transform.position = _previousPosition;
            _currentPosition = transform.position;
        }
    }
}
