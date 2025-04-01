using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : UI
{
    List<UI_StatEnchantSlot> slots = new();
    List<UI_EggSlot> eggSlots = new();

    [SerializeField] private Button egg1Button;
    [SerializeField] private Button egg2Button;
    [SerializeField] private GameObject eggSelect;
    private Action _onEggSelected;

    [SerializeField] private Image stageImage;
    [SerializeField] private Button stageLeftButton;
    [SerializeField] private Button stageRightButton;

    [SerializeField] private Transform enchantSlotParent;
    [SerializeField] private Transform eggSlotParent;

    [SerializeField] private TextMeshProUGUI goldText;
    
    [SerializeField] private Button startButton;

    private SaveData saveData;

    protected override void Awake()
    {
        base.Awake();

        //플레이어 세이브파일 받아와서 세팅해야 함
        saveData = SaveManager.Instance.saveData;

        //UI_Lobby 세팅해야 함
    }
}
