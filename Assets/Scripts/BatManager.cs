using UnityEngine;

public class BatManager : MonoBehaviour
{
    [SerializeField] private Bat _batPrefab;
    [SerializeField] private float _spawnRate = 2f;
    private float _randY;
    private Vector2 _spawnPosition;
    private float nextSpawn = 0.0f;
    private bool _isFacingLeft = false;

    private void Update()
    {
        if (Time.time > nextSpawn)
        {
            if (Random.Range(0, 2) == 1)
            {
                if (_isFacingLeft)
                {
                    _isFacingLeft = false;
                    _batPrefab.transform.localScale = new Vector3(-1, 1, 1);
                }
                nextSpawn = Time.time + _spawnRate;
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
                nextSpawn = Time.time + _spawnRate;
                _randY = Random.Range(-1f, 18f);
                _spawnPosition = new Vector2(35, _randY);
                Instantiate(_batPrefab, _spawnPosition, Quaternion.identity);
            }
        }
    }
}
