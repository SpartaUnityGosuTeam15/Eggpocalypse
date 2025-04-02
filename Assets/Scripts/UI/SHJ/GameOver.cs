using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : UI
{
    public GameObject gameoverPanel;
    public GameObject returnButton;
    public TextMeshProUGUI goldEarnedText;

    void Start()
    {
        Time.timeScale = 0f;

        int currentGold = GameManager.Instance.gold;
        int savedGold = SaveManager.Instance.saveData.gold;
        int earnedGold = currentGold - savedGold;

        if (goldEarnedText != null)
            goldEarnedText.gameObject.SetActive(true);
            goldEarnedText.text = $"¾òÀº °ñµå : <color=yellow>+ {earnedGold}Gold</color> È¹µæ!";
        
        GameObject timerText = GameObject.Find("TimerText");
        if (timerText != null) timerText.SetActive(false);
        if (returnButton != null) returnButton.SetActive(true);
        if (gameoverPanel != null) gameoverPanel.SetActive(true);
    }
    public void OnClickReturnToLobby()
    {
        Time.timeScale = 1f;
        GameManager.Instance.LoadScene("Lobby");
    }
}
