using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PumpkinManager : MonoBehaviour
{
    [SerializeField] private GameObject _PumpkinPrefab;
    [SerializeField] private GameObject _Parent;
    [SerializeField] private GameObject _Player;
    [SerializeField] private int _MaxPumpkinCount = 5;
    [SerializeField] private int _SpawnMagnitude = 3;
    [SerializeField] private const int _MaxXPosition = 31;
    [SerializeField] private const int _MaxYPosition = 17;
    [SerializeField] private TMPro.TextMeshProUGUI _ScoreText;
    [SerializeField] private Player player;
   private List<GameObject> _InstantiatedPumpkins;
    private int _PumpkinCount = 0;

    private void Awake()
    {
        _InstantiatedPumpkins = new List<GameObject>();
        _ScoreText.text = "Score:" + _PumpkinCount;
    }

    private void FixedUpdate()
    {
        if (_InstantiatedPumpkins.Count < _MaxPumpkinCount) {
            InstantiatePumpkin();
        }
    }

    public void PickUpPumpkin(GameObject pumpkin)
    {
        _InstantiatedPumpkins.Remove(pumpkin);
        Destroy(pumpkin);
        _PumpkinCount++;
        _ScoreText.text = "Score:" + _PumpkinCount;
        if (_PumpkinCount % 10 == 0)
        {
            player.SetCanTransform(true);
        }
        if (_PumpkinCount >= 100) {
            SceneManager.LoadScene("Finish");
        }
    }

    private void InstantiatePumpkin() {
        Vector3 position;
        while (true)
        {
            int x = (int)Random.Range(0, _MaxXPosition), y = (int)Random.Range(0, _MaxYPosition), z = 0;
            position = new Vector3(x, y, z);
            if ((position - _Player.transform.position).magnitude >= _SpawnMagnitude) {
                break;
            }
        }
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
        GameObject newPumpkin = Instantiate(_PumpkinPrefab, position, rotation, _Parent.transform);
        _InstantiatedPumpkins.Add(newPumpkin);
    }
}
