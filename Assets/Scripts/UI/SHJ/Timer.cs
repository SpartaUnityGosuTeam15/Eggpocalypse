using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : UI
{
    public TMP_Text timerText;
    public GameObject clearPanel;
    public GameObject returnButton;
    public TextMeshProUGUI goldEarnedText;

    public float totalTime = 15 * 60f; // 초 단위 15분 
    private float remainingTime;
    private bool isRunning = true;

    private float blinkTimer = 0f;
    private float blinkInterval = 0.5f;

    private Color defaultColor;
    private FontStyles defaultFontStyle;

    private bool isCleared = false;

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

        if (remainingTime <= 0 && !isCleared)
        {
            remainingTime = 0;
            isRunning = false;
            Clear();
        }

        UpdateTimerText();
        UpdateVisualEffect();
    }
    void Clear()
    {
        isCleared = true;
        Time.timeScale = 0f;

        Debug.Log("클리어 성공!");

        if (returnButton != null)
            returnButton.SetActive(true);
        if (clearPanel != null)
            clearPanel.SetActive(true);
        if (timerText != null)
            timerText.enabled = false;

        int currentGold = GameManager.Instance.gold;
        int savedGold = SaveManager.Instance.saveData.gold;
        int earnedGold = currentGold - savedGold;

        if (goldEarnedText != null)
            goldEarnedText.gameObject.SetActive(true);
        goldEarnedText.text = $"Result : <color=yellow>+ {earnedGold}</color> Gold!";
    }
    public void OnClickReturnToLobby()
    {
        Time.timeScale = 1f;
        GameManager.Instance.LoadScene("Lobby");
        
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
