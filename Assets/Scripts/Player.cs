using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IUnit
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Timer _timeController;
    public Vector3 CurrentPosition { get; private set; }

    private const string FinishSceneName = "Finish";
    private const string ScaredHumanTag = "ScaredHuman";

    private void Awake()
    {
        _playerController.OnMoveChange += Move;
        _playerController.OnAnimationChange += SetAnimator;
        _timeController.OnDayNightTransition += Transition;
    }

    private void OnDestroy()
    {
        _playerController.OnMoveChange -= Move;
        _playerController.OnAnimationChange -= SetAnimator;
        _timeController.OnDayNightTransition -= Transition;
    }

    private void Start()
    {
        _movePoint.parent = null;
        CurrentPosition = _movePoint.position;
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

    public void Transition(bool isDay)
    {
        if (isDay)
        {
            // Other Day to Night transition
        }
        else
        {
            transform.position = new Vector3(17, 8, 0);
            CurrentPosition = transform.position;
            _movePoint.position = transform.position;
            //Despawn Skeletons
        }
    }
}

