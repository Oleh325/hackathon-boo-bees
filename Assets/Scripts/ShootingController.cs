using System.Collections;
using TMPro;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _bulletsParrent;
    [SerializeField] private Vector2 _minMapCordinatesPoint;
    [SerializeField] private Vector2 _maxMapCordinatesPoint;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private int _ammoCount = 10;

    private void Awake()
    {
        _playerController.OnShoot += Shoot;
        UpdateAmmoText();
    }

    private void OnDestroy()
    {
        _playerController.OnShoot -= Shoot;
    }

    public void Shoot(Vector2 mousePos)
    {
        if (!CanShoot()) return;
        SpendBullet();
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation, _bulletsParrent.transform);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce((mousePos - (Vector2) transform.position).normalized * _bulletSpeed, ForceMode2D.Impulse);
        StartCoroutine(DestroyBulletOnMapBorder(bullet));
    }

    private bool CanShoot()
    {
        return _ammoCount > 0;
    }

    public void AddBullet()
    {
        _ammoCount++;
        UpdateAmmoText();
    }

    public void SpendBullet()
    {
        _ammoCount--;
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        _ammoText.SetText("" + _ammoCount);
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
