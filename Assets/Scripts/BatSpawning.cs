using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawning : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    float RandY;
    Vector2 whereToSpawn;
    [SerializeField]
    private float spawnRate = 2f;
    float nextSpawn = 0.0f;
    private bool rightOrLeft = false;

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            int vect = Random.Range(0, 2);
            if (vect == 1)
            {
                if (rightOrLeft)
                {
                    rightOrLeft = false;
                    obj.transform.localScale = new Vector3(-1, 1, 1);
                }
                nextSpawn = Time.time + spawnRate;
                RandY = Random.Range(-1f, 18f);
                whereToSpawn = new Vector2(-4, RandY);
                Instantiate(obj, whereToSpawn, Quaternion.identity);
            }
            else
            {
                if (!rightOrLeft)
                {
                    rightOrLeft = true;
                    obj.transform.localScale = new Vector3(1, 1, 1);
                }
                nextSpawn = Time.time + spawnRate;
                RandY = Random.Range(-1f, 18f);
                whereToSpawn = new Vector2(35, RandY);
                Instantiate(obj, whereToSpawn, Quaternion.identity);
            }

        }

    }
}
