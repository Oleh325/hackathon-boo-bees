using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float timerDuration = 3f * 60f; //Duration of the timer in seconds

    private float timer;
    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI separator;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;

    private const float DeltaTime = 1f;
    private const float HalfDeltaTime = DeltaTime/2f;
    [SerializeField]
    private float flashMagnitude = 0.4f; //The half length of the flash(enabled text)
    [SerializeField]
    private float flashDuraction = 10f;

    private void Start()
    {
        ResetTimer();
    }

    private void ResetTimer()
    {
        timer = timerDuration;
        SetTextDisplay(true);
    }

    void Update()
    {
        if (timer > 0)
        {
            if (timer < flashDuraction)
            {
                FlashTimer();
            }
            timer -= Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void UpdateTimerDisplay(float time)
    {
        if (time < 0)
        {
            time = 0;
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{01:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }

    private void FlashTimer()
    {
        SetTextDisplay(Mathf.Abs(timer % DeltaTime - HalfDeltaTime) < flashMagnitude);
    }

    private void SetTextDisplay(bool enabled)
    {
        firstMinute.enabled = enabled;
        secondMinute.enabled = enabled;
        separator.enabled = enabled;
        firstSecond.enabled = enabled;
        secondSecond.enabled = enabled;
    }
}