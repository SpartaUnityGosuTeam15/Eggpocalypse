using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Pause : UI
{
    private PlayerInputActions inputAction;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button LobbyButton;
    [SerializeField] private Button ResumeButton;

    private bool isPaused = false;
    protected override void Awake()
    {
        inputAction = new PlayerInputActions();
        LobbyButton.onClick.AddListener(() => GameManager.Instance.LoadScene("Lobby"));
        ResumeButton.onClick.AddListener(() => PauseGame(false));
    }

    private void OnEnable()
    {
        inputAction.UI.Pause.started += OnPaused;
        inputAction.UI.Enable();
    }


    private void OnPaused(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isPaused) // 재개
            {
                PauseGame(false);
            }
            else // 일시정지
            {
                PauseGame(true);
            }
        }
    }

    public void PauseGame(bool pause)
    {
        isPaused = pause;
        pausePanel.SetActive(isPaused);
        Time.timeScale = pause ? 0f : 1f;
    }
}
