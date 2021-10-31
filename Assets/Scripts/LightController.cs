using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{

    [SerializeField] private Timer _timer;
    private Light2D light;

    // Start is called before the first frame update
    private void Start()
    {
        light = gameObject.GetComponent<Light2D>();
        _timer.OnDayNightTransition += SetLightIntensity;
    }

    private void OnDestroy()
    {
        _timer.OnDayNightTransition -= SetLightIntensity;
    }

    // Update is called once per frame

    private void SetLightIntensity(bool isDay)
    {
        light.intensity = isDay ? 0.5f : 0.02f;
    }
}
