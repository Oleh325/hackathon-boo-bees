using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryHumanManager : MonoBehaviour
{

    [SerializeField] private GameObject _angryHuman;
    [SerializeField] private int _angryHumanCount;
    private List<GameObject> _instantiatedAngryHumans;

    private void Awake()
    {
        _instantiatedAngryHumans = new List<GameObject>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (_instantiatedAngryHumans.Count < _angryHumanCount)
        {
            InstantiateAngryHuman();
        }
    }

    private void InstantiateAngryHuman()
    {

    }
}
