using UnityEngine;
using UnityEngine.SceneManagement;

public class Skeleton : MonoBehaviour
{
    
    private const string GameOverSceneName = "GameOver";
    private const string PlayerTagName = "Player";
    private const string BulletTagName = "Bullet";

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(PlayerTagName))
        {
            SceneManager.LoadScene(GameOverSceneName);
        }

        if (other.gameObject.CompareTag(BulletTagName))
        {
            Destroy(gameObject);
        }
    }
    private void MoveCharacter(Vector2 direction)
    {
        _rigidbody2D.MovePosition((Vector2) transform.position + (direction * _moveSpeed * Time.deltaTime));
    }
}