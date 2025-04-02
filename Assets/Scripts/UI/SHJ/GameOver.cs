using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : UI
{
    public GameObject panel;
    public GameObject returnButton;

    void Start()
    {
        Time.timeScale = 0f;

        if (returnButton != null)
            returnButton.SetActive(true);
        if (panel != null)
            panel.SetActive(true);
    }
    public void OnClickReturnToLobby()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby");
    }
}
