using UnityEngine;

public class Bat : MonoBehaviour
{
    private const float MaxMove = 0.05f;
    
    [SerializeField] private float _speed = 15f;
    private int _direction;

    private void Start()
    {
        if (transform.position.x < 0)
        {
            _direction = 1;
        }
        else
        {
            _direction = -1;
        }
    }
    private void Update()
    {
        Vector2 movement = new Vector2(Time.deltaTime * _speed * _direction, MaxMove * Mathf.Sin(Time.time * _speed));
        transform.Translate(movement);
        if (transform.position.x > 35 || transform.position.x < -4)
        {
            Destroy(gameObject);
        }
    }
}
