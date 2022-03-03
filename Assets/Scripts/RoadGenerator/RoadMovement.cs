using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _startSpeed;
    private float _currentSpeed;
    private Rigidbody _rigidbody;

    void Start()
    {
        _startPosition.y = transform.position.y;
        _currentSpeed = _startSpeed;
        _rigidbody = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(0, 0, -_currentSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndRoad"))
        {
            gameObject.SetActive(false);
            transform.position = _startPosition;
            gameObject.SetActive(true);
        }
    }
}
