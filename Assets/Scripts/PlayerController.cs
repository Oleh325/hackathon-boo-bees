using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float Tolerance = 0.1f;
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
   
    [SerializeField] private Player _player;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Vector2 _minMapCordinatesPoint;
    [SerializeField] private Vector2 _maxMapCordinatesPoint;
    [SerializeField] private GameObject _stoneParent;
    [SerializeField] private GameObject _waterIslandParent;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private Animator _animator;
    [SerializeField] private UnityEngine.Camera _camera;
    private bool _isReloading = false;
    private float _delayForReload = 1;
    private Water[] _waterInstances;
    
    public Action<DirectionWrapper> OnMoveChange = delegate {};
    public Action<DirectionWrapper, DirectionWrapper> OnAnimationChange = delegate {};
    public Action<Vector2> OnShoot = delegate {};

    private void Awake()
    { 
        _waterInstances = _waterIslandParent.GetComponentsInChildren<Water>();
    }
    
    private bool IsCurrentlyMoving(DirectionWrapper horizontalDirectionWrapper, DirectionWrapper verticalDirectionWrapper)
    {
        return Mathf.Abs(horizontalDirectionWrapper.AxisValue) != 0f && Mathf.Abs(verticalDirectionWrapper.AxisValue) != 0f;
    }

    private bool IsLegalMove(DirectionWrapper horizontalDirectionWrapper, DirectionWrapper verticalDirectionWrapper)
    {
        float afterMoveHorizontal = _player.CurrentPosition.x + horizontalDirectionWrapper.AxisValue; 
        float afterMoveVertical = _player.CurrentPosition.y + verticalDirectionWrapper.AxisValue;
        Vector3 afterMovePosition = new Vector3(afterMoveHorizontal, afterMoveVertical);
        bool isWithinBounds = _minMapCordinatesPoint.x <= afterMoveHorizontal &&
                              afterMoveHorizontal <= _maxMapCordinatesPoint.x &&
                              _minMapCordinatesPoint.y <= afterMoveVertical &&
                              afterMoveVertical <= _maxMapCordinatesPoint.y;
        bool isNotStuck = true;
        foreach (var stone in _stoneParent.GetComponentsInChildren<Stone>())
        {
            if (stone.CurrentPosition == afterMovePosition)
            {
                isNotStuck = false;
            }
        }
        foreach (var water in _waterInstances)
        {
            if (water.CurrentPosition == afterMovePosition)
            {
                isNotStuck = false;
            }
        }
        return isWithinBounds && isNotStuck;
    }

    private void Update()
    {
        ControlCommonMovement();
        ControlShooting();
    }

    private void ControlShooting()  
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_isReloading)
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            OnShoot(mousePos);
            _isReloading = true;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_delayForReload);
        _isReloading = false;
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
