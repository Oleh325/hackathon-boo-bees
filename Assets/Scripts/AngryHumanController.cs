using UnityEngine;
using UnityEngine.SceneManagement;

public class AngryHumanController : MonoBehaviour
{
    private const string GameOverSceneName = "GameOver";
    private const string PlayerTagName = "Player";
    private const string BulletTagName = "Bullet";

    [SerializeField] private Transform _movePoint;
    [SerializeField] private float _moveSpeed = 10f;

    private void Start()
    {
        _movePoint.parent = null;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _movePoint.position) == 0f) {
            RespawnAndSetFinalMovePoint();
        }
        transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(PlayerTagName))
        {
            SceneManager.LoadScene(GameOverSceneName);
        }

        if (other.gameObject.CompareTag(BulletTagName))
        {
            RespawnAndSetFinalMovePoint();
        }
    }

    private void RespawnAndSetFinalMovePoint() 
    {
        bool topToBottom = Random.Range(0, 2) == 0;
        if (topToBottom)
        {
            transform.position = new Vector3(Random.Range(0, 32), 18, transform.position.z);
            _movePoint.position = new Vector3(Random.Range(0, 32), -1, _movePoint.position.z);
        }
        else
        {
            transform.position = new Vector3(-1, Random.Range(0, 18), transform.position.z);
            _movePoint.position = new Vector3(32, Random.Range(0, 18), _movePoint.position.z);
        }
    }
}