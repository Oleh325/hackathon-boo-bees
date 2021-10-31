using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScaredHumanController : MonoBehaviour
{
    private const float Tolerance = 0.1f;
    
    [SerializeField] private ScaredHuman _scaredHuman;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Vector2 _minMapCordinatesPoint;
    [SerializeField] private Vector2 _maxMapCordinatesPoint;
    [SerializeField] private GameObject _stoneParent;
    [SerializeField] private Player _player;
    
    public Action<DirectionWrapper> OnMoveChange = delegate {};
    public Action<DirectionWrapper> OnAnimationChange = delegate {};
    public Action<Vector3> OnTeleport = delegate {};
    
    private void Update()
    {
        ControlCommonMovement();
    }

    private bool IsLegalMove(DirectionWrapper directionWrapper) 
    {
        Vector3 afterMovePosition = _scaredHuman.CurrentPosition + directionWrapper.Vector3Value; 
        bool isWithinBounds = _minMapCordinatesPoint.x <= afterMovePosition.x &&
                              afterMovePosition.x <= _maxMapCordinatesPoint.x &&
                              _minMapCordinatesPoint.y <= afterMovePosition.y &&
                              afterMovePosition.y <= _maxMapCordinatesPoint.y;
        bool isNotStuckIntoStone = true;

        bool isNotStuckIntoPlayer = !IsPointNearPlayer(afterMovePosition);

        foreach (var stone in _stoneParent.GetComponentsInChildren<Stone>())
        {
            Debug.Log("in foreach");
            if (stone.CurrentPosition == afterMovePosition)
            {
                isNotStuckIntoStone = false;
            }
        }
        
        return isWithinBounds && isNotStuckIntoStone && isNotStuckIntoPlayer;
    }

    private void ControlCommonMovement()
    {
        if (Vector3.Distance(transform.position, _movePoint.position) <= Tolerance)
        {
            if (Random.Range(0, 15) == 1)
            {
                Vector3 teleportToPoint = GenerateRandomMapPoint();
                while (IsPointNearPlayer(teleportToPoint))
                {
                    teleportToPoint = GenerateRandomMapPoint();
                }
                OnTeleport(teleportToPoint);
                return;
            }
            
            DirectionWrapper directionWrapper = new DirectionWrapper((Direction)Random.Range(0, 4));
            if (IsLegalMove(directionWrapper))
            {
                OnMoveChange(directionWrapper);
            }
            OnAnimationChange(directionWrapper);
        }
    }

    private Vector3 GenerateRandomMapPoint()
    {
        return new Vector3(Random.Range(_minMapCordinatesPoint.x, _maxMapCordinatesPoint.x),
            Random.Range(_minMapCordinatesPoint.y, _maxMapCordinatesPoint.y), 0);
    }

    private bool IsPointNearPlayer(Vector3 point)
    {
        return Vector3.Distance(point, _player.CurrentPosition) <= 3;
    }
}
