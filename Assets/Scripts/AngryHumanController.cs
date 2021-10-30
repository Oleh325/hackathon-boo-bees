using UnityEngine;
using UnityEngine.SceneManagement;

public class AngryHumanController : MonoBehaviour
{

    [SerializeField] private Transform _movePoint;
    [SerializeField] private float _moveSpeed = 10f;

    // Start is called before the first frame update
    private void Start()
    {
        _movePoint.parent = null;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(transform.position, _movePoint.position) == 0f) {
            RespawnAndSetFinalMovePoint();
        }
        transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _moveSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void RespawnAndSetFinalMovePoint() {
        bool topToBottom = Random.Range(0, 2) == 0 ? true : false;
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