using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public float totalTime = 15 * 60f; // 초 단위

    private float remainingTime;
    private bool isRunning = true;

    //private bool isBlinking = false;
    private float blinkTimer = 0f;
    private float blinkInterval = 0.5f;

    private Color defaultColor;
    private FontStyles defaultFontStyle;

    void Start()
    {
        remainingTime = totalTime;

        // 기본 스타일 저장
        defaultColor = timerText.color;
        defaultFontStyle = timerText.fontStyle;
    }

    void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            remainingTime = 0;
            isRunning = false;
        }

        UpdateTimerText();
        UpdateVisualEffect();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void UpdateVisualEffect()
    {
        if (remainingTime <= 10f)
        {
            // 10초 이하
            timerText.fontStyle = FontStyles.Bold;
            timerText.color = Color.red;

            blinkTimer += Time.deltaTime;
            if (blinkTimer >= blinkInterval)
            {
                blinkTimer = 0f;
                timerText.enabled = !timerText.enabled;
            }
        }
        else if (remainingTime <= 60f)
        {
            // 1분 이하
            timerText.fontStyle = defaultFontStyle;
            timerText.color = Color.red;
            timerText.enabled = true;
        }
        else
        {
            // 기본
            timerText.fontStyle = defaultFontStyle;
            timerText.color = defaultColor;
            timerText.enabled = true;
        }
    }
}
