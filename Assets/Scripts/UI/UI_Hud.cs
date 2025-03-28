using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI killText;

    [SerializeField] private Image hpBar;
    [SerializeField] private Image expBar;

    [SerializeField] private Transform skillSlotsParentTransform;
    [SerializeField] private Transform statSlotsParentTransform;

    private List<UI_SkillSlot> _skillSlots;
    private List<UI_StatSlot> _statSlots;

    protected override void Awake()
    {
        base.Awake();

        _skillSlots = new List<UI_SkillSlot>(skillSlotsParentTransform.GetComponentsInChildren<UI_SkillSlot>());
        _statSlots = new List<UI_StatSlot>(statSlotsParentTransform.GetComponentsInChildren<UI_StatSlot>());
    }
}
