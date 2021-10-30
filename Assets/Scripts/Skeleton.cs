using UnityEngine;

public class Skeleton : MonoBehaviour
{
    private const string PlayerTagName = "Player"; 
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _movement;

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
    }

    private void FixedUpdate()
    {
        MoveCharacter(_movement);
    }

    private void MoveCharacter(Vector2 direction)
    {
        _rigidbody2D.MovePosition((Vector2) transform.position + (direction * _moveSpeed * Time.deltaTime));
    }


    private void OnTriggerEnter(Collider2D other)
    {
        if (other.gameObject.CompareTag(PlayerTagName)) 
        {
            Destroy(gameObject);
        }
    }
}