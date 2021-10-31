using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{

    [SerializeField] private GameObject _bridgeParent;
    [SerializeField] private Timer _timer;
    // Start is called before the first frame update
    private void Start()
    {
        _timer.OnDayNightTransition += SetBridgesActive;
    }

    private void OnDestroy()
    {
        _timer.OnDayNightTransition -= SetBridgesActive;
    }

    // Update is called once per frame
    private void SetBridgesActive(bool isDay)
    {
        foreach (Transform child in _bridgeParent.transform)
        {
            Debug.Log("Setting bridges to" + isDay);
            child.gameObject.SetActive(isDay);
        }
    }
}
