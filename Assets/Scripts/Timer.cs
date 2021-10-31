using UnityEngine;
using TMPro;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private const float DeltaTime = 1f;
    private const float HalfDeltaTime = DeltaTime / 2f;

    [SerializeField] private float timerDuration = 60f; //Duration of the timer in seconds
    [SerializeField] private TextMeshProUGUI firstMinute;
    [SerializeField] private TextMeshProUGUI secondMinute;
    [SerializeField] private TextMeshProUGUI separator;
    [SerializeField] private TextMeshProUGUI firstSecond;
    [SerializeField] private TextMeshProUGUI secondSecond;
    [SerializeField] private TextMeshProUGUI dayNumber;
    [SerializeField] private TextMeshProUGUI nightNumber;
    [SerializeField] private float flashMagnitude = 0.4f; //The half length of the flash(enabled text)
    [SerializeField] private float flashDuraction = 10f;
    [SerializeField] private CanvasGroup dayCanvas;
    [SerializeField] private CanvasGroup nightCanvas;
    private float timer;
    private int dayNightCount = 1;
    private float fadingDuration = 2.0f;
    public Action<bool> OnDayNightTransition = delegate {};
    public bool isDay { get; private set; } = true;
    
    private void Start()
    {
        ResetTimer();
        StartCoroutine(WaitDay());
        timer -= 2.0f;
    }

    void Update()
    {
        if (dayNightCount == 6)
        {
            SceneManager.LoadScene("Finish", LoadSceneMode.Single);
        }
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
            if (isDay)
            {
                nightNumber.text = "NIGHT " + dayNightCount.ToString();
                StartCoroutine(FadeInNight(nightCanvas, nightCanvas.alpha));
                isDay = false;
            }
            else
            {
                dayNightCount++;
                dayNumber.text = "DAY " + dayNightCount.ToString();
                StartCoroutine(FadeInDay(dayCanvas, dayCanvas.alpha));
                isDay = true;
            }
            ResetTimer();
        }
    }

    private IEnumerator FadeInNight(CanvasGroup canvGroup, float start)
    {
        float counter = 0f;
        while (counter < fadingDuration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, 1, counter/fadingDuration);
            yield return null;
        }
        StartCoroutine(WaitNight());
    }

    private IEnumerator FadeOutNight(CanvasGroup canvGroup, float start)
    {
        float counter = 0f;
        while (counter < fadingDuration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, 0, counter / fadingDuration);
            yield return null;
        }
    }

    private IEnumerator FadeInDay(CanvasGroup canvGroup, float start)
    {
        float counter = 0f;
        while (counter < fadingDuration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, 1, counter / fadingDuration);
            yield return null;
        }
        StartCoroutine(WaitDay());
    }

    private IEnumerator FadeOutDay(CanvasGroup canvGroup, float start)
    {
        float counter = 0f;
        while (counter < fadingDuration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, 0, counter / fadingDuration);
            yield return null;
        }
    }
    private IEnumerator WaitNight()
    {
        OnDayNightTransition(isDay);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        timer += 2.0f;
        StartCoroutine(FadeOutNight(nightCanvas, nightCanvas.alpha));
    }

    private IEnumerator WaitDay()
    {
        OnDayNightTransition(isDay);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        timer += 2.0f;
        StartCoroutine(FadeOutDay(dayCanvas, dayCanvas.alpha));
    }

    private void ResetTimer()
    {
        timer = timerDuration;
        SetTextDisplay(true);
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