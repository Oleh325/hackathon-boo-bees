using UnityEngine;
using System.Collections;

public class BatManager : MonoBehaviour
{
    [SerializeField] private Bat _batPrefab;
    [SerializeField] private Timer _timeController;
    private float _randY;
    private Vector2 _spawnPosition;
    private bool _isFacingLeft = false;
    private bool canSpawn;

    private void Start()
    {
        _timeController.OnDayNightTransition += SetCanSpawn;
    }

    private void OnDestroy()
    {
        _timeController.OnDayNightTransition -= SetCanSpawn;
    }

    private void SetCanSpawn(bool isDay)
    {
        canSpawn = isDay;
        StartCoroutine(SpawnBat());
    }

        private IEnumerator SpawnBat()
    {
        while (canSpawn)
        {
            if (Random.Range(0, 2) == 1)
            {
                if (_isFacingLeft)
                {
                    _isFacingLeft = false;
                    _batPrefab.transform.localScale = new Vector3(-1, 1, 1);
                }
                _randY = Random.Range(-1f, 18f);
                _spawnPosition = new Vector2(-4, _randY);
                Instantiate(_batPrefab, _spawnPosition, Quaternion.identity);
            }
            else
            {
                if (!_isFacingLeft)
                {
                    _isFacingLeft = true;
                    _batPrefab.transform.localScale = new Vector3(1, 1, 1);
                }
                _randY = Random.Range(-1f, 18f);
                _spawnPosition = new Vector2(35, _randY);
                Instantiate(_batPrefab, _spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(3);
        }
    }
}
