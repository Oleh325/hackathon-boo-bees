using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Vector2 _minMapCordinatesPoint;
    [SerializeField] private Vector2 _maxMapCordinatesPoint;
    [SerializeField] private GameObject _stoneParent;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private Animator animator;

    private const float Tolerance = 0.1f;
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
   
    public Action<DirectionWrapper> OnMoveChange = delegate {};
    public Action<DirectionWrapper, DirectionWrapper> OnAnimationChange = delegate {};


    private bool IsCurrentlyMoving(DirectionWrapper horizontalDirectionWrapper, DirectionWrapper verticalDirectionWrapper)
    {
        return Mathf.Abs(horizontalDirectionWrapper.AxisValue) != 0f && Mathf.Abs(verticalDirectionWrapper.AxisValue) != 0f;
    }

    private bool IsLegalMove(DirectionWrapper horizontalDirectionWrapper, DirectionWrapper verticalDirectionWrapper) {
        float afterMoveHorizontal = _player.CurrentPosition.x + horizontalDirectionWrapper.AxisValue; 
        float afterMoveVertical = _player.CurrentPosition.y + verticalDirectionWrapper.AxisValue;
        Vector3 afterMovePosition = new Vector3(afterMoveHorizontal, afterMoveVertical);
        bool isWithinBounds = _minMapCordinatesPoint.x <= afterMoveHorizontal &&
                              afterMoveHorizontal <= _maxMapCordinatesPoint.x &&
                              _minMapCordinatesPoint.y <= afterMoveVertical &&
                              afterMoveVertical <= _maxMapCordinatesPoint.y;
        bool isNotStuckIntoStone = true;
        foreach (var stone in _stoneParent.GetComponentsInChildren<Stone>())
        {
            if (stone.CurrentPosition == afterMovePosition)
            {
                isNotStuckIntoStone = false;
            }
        }
        if (animator.GetBool("isPlayerBat") == true)
        {
            isNotStuckIntoStone = true;
        }
        return isWithinBounds && isNotStuckIntoStone;
    }

    private void Update()
    {
        ControlCommonMovement();
    }


    private void ControlCommonMovement()
    {
        DirectionWrapper horizontalDirectionWrapper = new DirectionWrapper(HorizontalAxisName);
        DirectionWrapper verticalDirectionWrapper = new DirectionWrapper(VerticalAxisName);
        if (!IsCurrentlyMoving(horizontalDirectionWrapper, verticalDirectionWrapper) &&
            Vector3.Distance(transform.position, _movePoint.position) <= Tolerance)
        {
            if (IsLegalMove(horizontalDirectionWrapper, verticalDirectionWrapper))
            {
                if (Mathf.Abs(horizontalDirectionWrapper.AxisValue) == 1f)
                {
                    OnMoveChange(horizontalDirectionWrapper);
                }
                if (Mathf.Abs(verticalDirectionWrapper.AxisValue) == 1f)
                {
                    OnMoveChange(verticalDirectionWrapper);
                }
            }
            OnAnimationChange(horizontalDirectionWrapper, verticalDirectionWrapper);
        }
    }
}
