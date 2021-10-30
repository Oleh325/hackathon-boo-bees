using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleManager : MonoBehaviour
{

    [SerializeField] private GameObject _PebblePrefab1;
    [SerializeField] private GameObject _PebblePrefab2;
    [SerializeField] private GameObject _Parent;
    [SerializeField] private int _PebbleCount = 5;
    [SerializeField] private int _SpawnMagnitude = 3;
    [SerializeField] private int _MaxXPosition = 31;
    [SerializeField] private int _MaxYPosition = 17;

    private List<GameObject> _InstantiatedPebbles;

    private void Awake()
    {
        _InstantiatedPebbles = new List<GameObject>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < _PebbleCount; ++i) {
            InstantiatePebble();
        }
    }

    private void InstantiatePebble()
    {
        Vector3 position = new Vector3();
        bool canSpawn = false;
        while (!canSpawn)
        {
            canSpawn = true;
            int x = (int)Random.Range(0, _MaxXPosition), y = (int)Random.Range(0, _MaxYPosition), z = 0;
            position = new Vector3(x, y, z);
            foreach (GameObject pebble in _InstantiatedPebbles)
            {
                if ((position - pebble.transform.position).magnitude < _SpawnMagnitude)
                {
                    canSpawn = false;
                    break;
                }
            }
        }
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
        GameObject newPumpkin = Instantiate(Random.Range(0, 2) > 0 ? _PebblePrefab1 : _PebblePrefab2, position, rotation, _Parent.transform);
        _InstantiatedPebbles.Add(newPumpkin);
    }

}
