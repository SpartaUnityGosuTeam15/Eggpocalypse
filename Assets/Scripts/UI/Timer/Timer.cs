using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;  // UI에 연결
    public float totalTime = 15 * 60f; // 15분 (초 단위)

    private float remainingTime;
    private bool isRunning = true;

    void Start()
    {
        remainingTime = totalTime;
    }

    void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            remainingTime = 0;
            isRunning = false;
            // 타이머 종료 시 로직 추가 가능
        }

        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
