
using UnityEngine;

public class ScaredHuman : MonoBehaviour,  IUnit
{
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";

    private static readonly int Vertical = Animator.StringToHash(VerticalAxisName);
    private static readonly int Horizontal = Animator.StringToHash(HorizontalAxisName);
    
    [SerializeField] private float _moveSpeed = 13f;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Animator _animator;
    [SerializeField] private ScaredHumanController _scaredHumanController;
    [SerializeField] private SoundManager _soundManager;
    private DirectionWrapper _currentDirection;

    public Vector3 CurrentPosition { get; private set; }

    private void Awake()
    {
        _scaredHumanController.OnMoveChange += Move;
        _scaredHumanController.OnAnimationChange += SetAnimator;
        _scaredHumanController.OnTeleport += Teleport;
    }

    private void OnDestroy()
    {
        _scaredHumanController.OnMoveChange -= Move;
        _scaredHumanController.OnAnimationChange -= SetAnimator;
        _scaredHumanController.OnTeleport -= Teleport;
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

    public void Move(DirectionWrapper directionWrapper)
    {
        _movePoint.position += directionWrapper.Vector3Value;
        CurrentPosition = _movePoint.position;
    }

    public void SetAnimator(DirectionWrapper directionWrapper)
    {
        _animator.SetFloat(Horizontal, 0);
        _animator.SetFloat(Vertical, 0);
        _animator.SetFloat(directionWrapper.AxisName, directionWrapper.AxisValue);
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
        _movePoint.position = position;
        CurrentPosition = position;
    }
}
