using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IUnit
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private TMPro.TextMeshProUGUI _TransformationText;
    [SerializeField] private Animator animator;
    private int timeToTransformation;
    public bool canTransform;
    public Vector3 CurrentPosition { get; private set; }

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
        animator.SetBool("isPlayerBat", false);
        timeToTransformation = 500;
        canTransform = false;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _moveSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TransformPlayer();
        }
    }

    private void FixedUpdate()
    {
        if (animator.GetBool("isPlayerBat"))
        {
            _TransformationText.gameObject.SetActive(true);
            if (timeToTransformation%50 == 0)
            {
                _TransformationText.text = "Time before transformation: " + timeToTransformation / 50;
            }
            timeToTransformation -= 1;
            if (timeToTransformation == 0)
            {
                TransformPlayer();
            }
        }
        else
        {
            timeToTransformation = 500;
            _TransformationText.gameObject.SetActive(false);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pumpkin")) {
            _soundManager.playSound("PumpkinPickedUp");      
        }
        if (other.gameObject.CompareTag("ScaredHuman")) {
            SceneManager.LoadScene("Finish");
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

    private void TransformPlayer()
    {
        if (canTransform == true && animator.GetBool("isPlayerBat") == false)
        {
            animator.SetBool("isPlayerBat", true);
            _moveSpeed *= 1.5f;
            _soundManager.playSound("Transformation");
            _soundManager.playSound("BatFly");
            canTransform = false;  
        }
        else if (animator.GetBool("isPlayerBat") == true)
        {
            animator.SetBool("isPlayerBat", false);
            _moveSpeed /= 1.5f;
            _soundManager.stopSound("BatFly");
            _soundManager.playSound("Transformation");
        }
    }
    public void SetCanTransform(bool can)
    {
            canTransform = can;
    }
}

