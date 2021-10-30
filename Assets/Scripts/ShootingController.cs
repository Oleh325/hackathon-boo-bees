using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private PlayerController _playerController;

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
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce((mousePos - (Vector2) transform.position).normalized * _bulletSpeed, ForceMode2D.Impulse);
    }
}
