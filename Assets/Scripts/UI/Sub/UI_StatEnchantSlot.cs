using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatEnchantSlot : MonoBehaviour
{
    [SerializeField] private Image skillIcon;
    public Button selectButton;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private IntEventChannel OnEnchantButtonClicked;

    public int id = -1;

    private void Awake()
    {
        selectButton.onClick.AddListener(() => OnEnchantButtonClicked?.RaiseEvent(id));
    }

    public void Init(int id, int level)
    {
        this.id = id;
        SetLevel(level);
    }

    public void SetLevel(int level)
    {
        levelText.text = $"·¹º§ {level.ToString()}";
    }
}
