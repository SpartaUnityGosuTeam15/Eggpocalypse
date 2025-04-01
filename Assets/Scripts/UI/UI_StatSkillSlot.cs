using TMPro;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class UI_StatSkillSlot : MonoBehaviour
{
    [SerializeField] private Image icon;

    [SerializeField] private TextMeshProUGUI levelText;

    protected void Awake()
    {
        icon.gameObject.SetActive(false);
    }
}
