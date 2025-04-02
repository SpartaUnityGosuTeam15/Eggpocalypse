using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Title : UI
{
    [SerializeField] private Button newStartButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Button closeButton;

    protected override void Awake()
    {
        base.Awake();

        newStartButton.onClick.AddListener(() => SaveManager.Instance.ClearDataFile());
        startButton.onClick.AddListener(() => GameManager.Instance.LoadScene("Lobby"));
        infoButton.onClick.AddListener(() => infoPanel.SetActive(true));
        exitButton.onClick.AddListener(() => Util.Exit());
        closeButton.onClick.AddListener(() => infoPanel.SetActive(false));
    }
}
