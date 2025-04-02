using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : UI
{
    [SerializeField] private List<UI_StatEnchantSlot> enchantSlots = new();
    [SerializeField] private List<UI_EggSlot> eggSlots = new();

    [SerializeField] private Button egg1Button;
    [SerializeField] private Button egg2Button;
    [SerializeField] private GameObject eggSelect;
    private Action _onEggSelected;

    [SerializeField] private Image stageImage;
    [SerializeField] private Button stageLeftButton;
    [SerializeField] private Button stageRightButton;

    [SerializeField] private TextMeshProUGUI goldText;
    
    [SerializeField] private Button startButton;

    [SerializeField] private IntEventChannel OnEnchantButtonClicked;

    private SaveData saveData;

    protected override void Awake()
    {
        base.Awake();

        saveData = SaveManager.Instance.saveData;

        OnEnchantButtonClicked.UnregisterListener(TryLevelUp);

        OnEnchantButtonClicked.RegisterListener(TryLevelUp);

        egg1Button.onClick.AddListener(OpenEggSlots);
        stageLeftButton.onClick.AddListener(SelectLeftStage);
        stageRightButton.onClick.AddListener(SelectRightStage);
        startButton.onClick.AddListener(StartStage);

        Init();
    }

    void Init()
    {
        for(int i = 0; i < saveData.enchantState.Count; i++)
        {
            enchantSlots[i].Init(i, saveData.enchantState[i]);
        }

        for (int i = 0; i < 2; i++)
        {
            Sprite dragonSprite = Resources.Load<Sprite>($"Arts/Egg/Egg_{i}");
            eggSlots[i].Init(i, dragonSprite);
            eggSlots[i].selectButton.onClick.AddListener(UpdateEggSlots);
        }

        UpdateEggSlots();

        UpdateStage();

        UpdateGold();
    }

    void UpdateGold()
    {
        goldText.text = saveData.gold.ToString();
    }

    void TryLevelUp(int id)
    {
        if (saveData.enchantState[id] >= 6) return; //만렙 체크

        int requireGold = (saveData.enchantState[id] + 1) * 100;

        if (saveData.gold < requireGold) return; //골드 보유량 체크

        saveData.gold -= requireGold;
        UpdateGold();
        saveData.enchantState[id]++;
        enchantSlots[id].SetLevel(saveData.enchantState[id]);

        SaveManager.Instance.SaveAll();
    }

    void OpenEggSlots()
    {
        eggSelect.SetActive(true);
    }
    
    void CloseEggSlots()
    {
        eggSelect.SetActive(false);
    }

    void UpdateEggSlots()
    {
        for(int i = 0; i < 2; i++)
        {
            eggSlots[i].UnSelected();
        }

        int selectedID = saveData.eggIndex;
        eggSlots[selectedID].Selected();
        egg1Button.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Arts/Egg/Egg_{selectedID}");

        CloseEggSlots();
    }

    void UpdateStage()
    {
        stageImage.sprite = Resources.Load<Sprite>($"Arts/Stage/Stage_{saveData.stageIndex}");
    }

    void SelectLeftStage()
    {
        saveData.stageIndex = (saveData.stageIndex + DataManager.Instance.stageDict.Count - 1) % DataManager.Instance.stageDict.Count;
        UpdateStage();
    }

    void SelectRightStage()
    {
        saveData.stageIndex = (saveData.stageIndex + 1) % DataManager.Instance.stageDict.Count;
        UpdateStage();
    }

    void StartStage()
    {
        SaveManager.Instance.SaveAll();
        GameManager.Instance.LoadScene($"Stage_{saveData.stageIndex}");
    }
}
