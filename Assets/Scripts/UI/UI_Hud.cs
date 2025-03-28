using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI meatText;

    [SerializeField] private Image hpBar;
    [SerializeField] private Image expBar;

    [SerializeField] private Transform skillSlotsParentTransform;
    [SerializeField] private Transform statSkillSlotsParentTransform;

    [SerializeField] private StatEventChannel OnHealthStatChanged;
    [SerializeField] private StatEventChannel OnExpStatChanged;

    [SerializeField] private IntEventChannel OnLevelChanged;
    [SerializeField] private IntEventChannel OnMeatChanged;
    [SerializeField] private IntEventChannel OnGoldChanged;

    private List<UI_SkillSlot> _skillSlots;
    private List<UI_StatSkillSlot> _statSkillSlots;

    protected override void Awake()
    {
        base.Awake();

        _skillSlots = new List<UI_SkillSlot>(skillSlotsParentTransform.GetComponentsInChildren<UI_SkillSlot>());
        _statSkillSlots = new List<UI_StatSkillSlot>(statSkillSlotsParentTransform.GetComponentsInChildren<UI_StatSkillSlot>());

        BindListeners();
    }

    void BindListeners()
    {
        OnHealthStatChanged.UnregisterListener(SetHPBar);
        OnExpStatChanged.UnregisterListener(SetExpBar);
        OnLevelChanged.UnregisterListener(SetLevel);
        OnMeatChanged.UnregisterListener(SetMeat);
        OnGoldChanged.UnregisterListener(SetGold);

        OnHealthStatChanged.RegisterListener(SetHPBar);
        OnExpStatChanged.RegisterListener(SetExpBar);
        OnLevelChanged.RegisterListener(SetLevel);
        OnMeatChanged.RegisterListener(SetMeat);
        OnGoldChanged.RegisterListener(SetGold);
    }

    void SetHPBar(Stat hp)
    {
        hpBar.fillAmount = hp.GetPercentage();
        hpText.text = $"{hp.CurValue} / {hp.MaxValue}";
    }

    void SetExpBar(Stat exp)
    {
        expBar.fillAmount = exp.GetPercentage();
    }

    void SetLevel(int level)
    {
        levelText.text = level.ToString();
    }

    void SetMeat(int meat)
    {
        meatText.text = meat.ToString();
    }

    void SetGold(int gold)
    {
        goldText.text = gold.ToString();
    }
}
