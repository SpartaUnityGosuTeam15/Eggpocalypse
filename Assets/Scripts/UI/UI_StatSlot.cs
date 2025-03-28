using TMPro;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class UI_StatSlot : UI
{
    [SerializeField] private Image icon;

    [SerializeField] private TextMeshProUGUI levelText;

    protected override void Awake()
    {
        base.Awake();

        icon.gameObject.SetActive(false);
    }
}
