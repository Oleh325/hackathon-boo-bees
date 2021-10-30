using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour, IUnit
{
    private Vector3 _currentPosition;
    public Vector3 CurrentPosition => _currentPosition;

    private void Start()
    {
        _currentPosition = transform.position;
    }
}
