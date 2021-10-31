using System.Collections.Generic;
using UnityEngine;

public class TreesManager : MonoBehaviour
{

    [SerializeField] private GameObject _TreePrefab;
    [SerializeField] private GameObject _Parent;
    [SerializeField] private int _TreesCount = 15;
    [SerializeField] private int _SpawnMagnitude = 3;
    [SerializeField] private int _MaxXPosition = 31;
    [SerializeField] private int _MaxYPosition = 17;
    [SerializeField] private Vector2 PondMinPosition = new Vector2(15, 6);
    [SerializeField] private Vector2 PondMaxPosition = new Vector2(19, 10);

    private List<GameObject> _InstantiatedTrees;

    private void Awake()
    {
        _InstantiatedTrees = new List<GameObject>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < _TreesCount; ++i) {
            InstantiateTree();
        }
    }

    private void InstantiateTree()
    {
        Vector3 position = new Vector3();
        bool canSpawn = false;
        while (!canSpawn)
        {
            canSpawn = true;
            int x = (int)Random.Range(0, _MaxXPosition), y = (int)Random.Range(0, _MaxYPosition), z = 0;
            position = new Vector3(x, y, z);
            foreach (GameObject tree in _InstantiatedTrees)
            {
                if ((position - tree.transform.position).magnitude < _SpawnMagnitude ||
                    SpawnedOnPond(position))
                {
                    canSpawn = false;
                    Debug.Log("cant spawn here: " + position.x + " " + position.y);
                    break;
                }
            }
        }
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
        GameObject newTree = Instantiate( _TreePrefab, position, rotation, _Parent.transform);
        _InstantiatedTrees.Add(newTree);
    }

    private bool SpawnedOnPond(Vector3 position)
    {
        return position.x >= PondMinPosition.x && position.x <= PondMaxPosition.x &&
               position.y >= PondMinPosition.y && position.y <= PondMaxPosition.y;
    }

}
