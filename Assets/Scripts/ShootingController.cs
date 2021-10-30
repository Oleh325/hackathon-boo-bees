using System.Collections;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _bulletsParrent;
    [SerializeField] private Vector2 _minMapCordinatesPoint;
    [SerializeField] private Vector2 _maxMapCordinatesPoint;
    
    private void Awake()
    {
        _playerController.OnShoot += Shoot;
    }

    private void OnDestroy()
    {
        _playerController.OnShoot -= Shoot;
    }

    public void Shoot(Vector2 mousePos)
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation, _bulletsParrent.transform);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce((mousePos - (Vector2) transform.position).normalized * _bulletSpeed, ForceMode2D.Impulse);
        StartCoroutine(DestroyBulletOnMapBorder(bullet));
    }

    private IEnumerator DestroyBulletOnMapBorder(GameObject bullet)
    {
        while (true)
        {
            bool isWithinBounds = _minMapCordinatesPoint.x <= bullet.transform.position.x &&
                                  bullet.transform.position.x <= _maxMapCordinatesPoint.x &&
                                  _minMapCordinatesPoint.y <= bullet.transform.position.y &&
                                  bullet.transform.position.y <= _maxMapCordinatesPoint.y;
            if (!isWithinBounds)
            {
                Destroy(bullet);
                yield break;
            }
            yield return null;
        }
    }
}
