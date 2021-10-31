using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skeleton : MonoBehaviour
{
    private enum SkeletonState
    {
        IsMovingDown,
        IsMovingUp,
        IsMovingLeft,
        IsMovingRight,
        IsRuined,
        None
    }

    private const string IslandTagName = "Island";
    private const string WaterTagName = "Water";
    private const string GameOverSceneName = "GameOver";
    private const string PlayerTagName = "Player";
    private const string SkeletonTagName = "Skeleton";
    private const string BulletTagName = "Bullet";

    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _movement;
    private bool _isDead = false;
    private bool _isOnWater = false;
    private bool _canWalkOnIsland = false;

    public bool IsDead => _isDead;
    public bool IsOnWater => _isOnWater;
    private void Awake()
    {
        _targetTransform = FindObjectOfType<Player>().transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }   
    private void Update()
    {
        Vector3 direction = _targetTransform.position - transform.position;
        direction.Normalize();
        _movement = direction;
        SetDirectionAnimation(direction);
    }

    private void SetDirectionAnimation(Vector3 direction)
    {
        if (_isDead)
        {
            return;
        }
        MoveCharacter(_movement);
        if (Mathf.Abs(direction.y / direction.x) <= 1f)
        {
            _animator.SetTrigger(direction.x >= 0
                ? Animator.StringToHash(SkeletonState.IsMovingRight.ToString())
                : Animator.StringToHash(SkeletonState.IsMovingLeft.ToString()));
        }
        else
        {
            _animator.SetTrigger(direction.y >= 0
                ? Animator.StringToHash(SkeletonState.IsMovingUp.ToString())
                : Animator.StringToHash(SkeletonState.IsMovingDown.ToString()));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead)
        {
            return;
        }
        if (other.gameObject.CompareTag(WaterTagName))
        {
            Debug.Log("touched water");
            _isOnWater = true;
        }

        if (other.gameObject.CompareTag(SkeletonTagName) && _isOnWater)
        {
            Debug.Log("toucher other sceleton");
            if (other.gameObject.GetComponent<Skeleton>().IsDead)
            {
                Debug.Log("CAN WLK ON ISLAND");
                _canWalkOnIsland = true;
            }
        }

        if (other.gameObject.CompareTag(BulletTagName))
        {
            _isDead = true;
            ResetAllTriggers();
            _animator.SetTrigger(Animator.StringToHash(SkeletonState.IsRuined.ToString()));
        }

        if (other.gameObject.CompareTag(IslandTagName) && !_canWalkOnIsland)
        {
            //StartCoroutine(DieWithDelay());
            _isDead = true;
            ResetAllTriggers();
            _animator.SetTrigger(Animator.StringToHash(SkeletonState.IsRuined.ToString()));
        }

    }

    private void ResetAllTriggers()
    {
        foreach (var param in _animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                _animator.ResetTrigger(param.name);
            }
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        _rigidbody2D.MovePosition((Vector2) transform.position + (direction * _moveSpeed * Time.deltaTime));
    }
}