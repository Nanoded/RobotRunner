using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAndBonusGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _obstaclesAndBonusPrefabs;
    [SerializeField] private float _obstacleOffsetX;
    private System.Random _random = new System.Random();
    private GameObject _obstacle;

    void Start()
    {
    }

    private void OnEnable()
    {
        int randomSide = _random.Next(-1, 2);
        Vector3 position = new Vector3(_obstacleOffsetX * randomSide, 0, 0);
        CreateObstacle(position);
    }

    private void CreateObstacle(Vector3 position)
    {
        int numberObstacle = _random.Next(0, _obstaclesAndBonusPrefabs.Count + 1);
        if (numberObstacle < _obstaclesAndBonusPrefabs.Count)
        {
            _obstacle = Instantiate(_obstaclesAndBonusPrefabs[numberObstacle], transform);
            _obstacle.transform.position += position;
        }
        else
        {
            return;
        }
    }

    private void OnDisable()
    {
        Destroy(_obstacle);
    }
}
