using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    float maxMove = 0.05f;
    public float speed = 15f;
    int direction;

    void Start()
    {
        if (transform.position.x < 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }
    void Update()
    {
        Vector2 movement = new Vector2(Time.deltaTime * speed * direction, maxMove * Mathf.Sin(Time.time * speed));
        transform.Translate(movement);
        if (transform.position.x > 35 || transform.position.x < -4)
        {
            Destroy(gameObject);
        }

    }
}
