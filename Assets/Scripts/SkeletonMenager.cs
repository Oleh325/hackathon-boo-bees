using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkeletonMenager : MonoBehaviour
{
    [SerializeField] private Skeleton _skeletonPrefab;
    [SerializeField] private GameObject _skeletonsParent;

    private void Start()
    {
        StartCoroutine(SpawnSkeletons());
    }

    private IEnumerator SpawnSkeletons()    
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            Vector3 spawnPosition;
            if (Random.Range(0, 40) < 18)
            {
                spawnPosition = new Vector3(Random.Range(0, 32), Random.value > 0.5f ? 0 : 18, 0);
            }
            else
            {
                spawnPosition = new Vector3(Random.value > 0.5f ? 0 : 32, Random.Range(0, 18), 0);
            }
            Instantiate(_skeletonPrefab, spawnPosition, Quaternion.identity, _skeletonsParent.transform);
        }
    }
}
