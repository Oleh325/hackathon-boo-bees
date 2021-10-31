using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkeletonMenager : MonoBehaviour
{
    [SerializeField] private Skeleton _skeletonPrefab;
    [SerializeField] private GameObject _skeletonsParent;
    [SerializeField] private Timer _timeController;
    private float delay = 5f;
    private bool canSpawn;

    private void Start()
    {
        _timeController.OnDayNightTransition += SetCanSpawn;
        _timeController.OnDayNightTransition += IncreaseDifficulty;
    }

    private void OnDestroy()
    {
        _timeController.OnDayNightTransition -= SetCanSpawn;
        _timeController.OnDayNightTransition -= IncreaseDifficulty;
    }

    private void IncreaseDifficulty(bool isDay) {
        if (!isDay && delay >= 1.25f)
        {
            delay -= 1f;
        }
    }

    private void SetCanSpawn(bool isDay)
    {
        canSpawn = !isDay;
        StartCoroutine(SpawnSkeletons());
        if (!canSpawn)
        {
            DestructCurrent();
        }    
    }


    private void DestructCurrent()
    { 
        foreach (Transform child in _skeletonsParent.transform)
        {
            if (!child.gameObject.GetComponent<Skeleton>().IsDead)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private IEnumerator SpawnSkeletons()    
    {
        while (canSpawn)
        {
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
            Debug.Log("waiting for secs: " + delay);
            yield return new WaitForSeconds(delay);
        }
    }
}
