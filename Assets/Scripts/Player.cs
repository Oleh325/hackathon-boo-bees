using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IUnit
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;
    public Vector3 CurrentPosition { get; private set; }

    private const string FinishSceneName = "Finish";
    private const string ScaredHumanTag = "ScaredHuman";

    private void Awake()
    {
        _playerController.OnMoveChange += Move;
        _playerController.OnAnimationChange += SetAnimator;
    }

    private void OnDestroy()
    {
        _playerController.OnMoveChange -= Move;
        _playerController.OnAnimationChange -= SetAnimator;
    }

    private void Start()
    {
        _movePoint.parent = null;
        CurrentPosition = _movePoint.position;
        _animator.SetBool("isPlayerBat", false);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(ScaredHumanTag))
        {
            SceneManager.LoadScene(FinishSceneName);
        }
        Debug.Log("collision with object: " + other.tag);
    }

    public void Move(DirectionWrapper directionWrapper)
    {
        _movePoint.position += directionWrapper.Vector3Value;
        CurrentPosition = _movePoint.position;
    }

    public void SetAnimator(DirectionWrapper horizontalDirectionWrapper, DirectionWrapper verticalDirectionWrapper)
    {
        _animator.SetFloat(horizontalDirectionWrapper.AxisName, horizontalDirectionWrapper.AxisValue);
        _animator.SetFloat(verticalDirectionWrapper.AxisName, verticalDirectionWrapper.AxisValue);
    }
    
}

