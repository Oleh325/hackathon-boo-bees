using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{

    [SerializeField] private GameObject _bridgeParent;
    [SerializeField] private Timer _timer;
    // Start is called before the first frame update
    void Start()
    {
        bool isDay = _timer.isDay;
        foreach(Transform child in _bridgeParent.transform)
        {
            child.gameObject.SetActive(isDay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
