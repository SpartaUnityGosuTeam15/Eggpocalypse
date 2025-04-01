using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : UI
{
    public Toggle[] eggToggles;
    public GameObject prevObj;
    public List<GameObject> eggs = new List<GameObject>();
    public GameObject notEnoughText;
    public bool isDragon = false;
    [SerializeField]private GameObject upgradeBtn;
    [SerializeField]private GameObject feedBtn;
    
    void Start()
    {
        foreach(Toggle toggle in eggToggles)
        {
            toggle.onValueChanged.AddListener((isOn) => TogglePreview(toggle,isOn));
            TogglePreview(toggle, toggle.isOn);
        }   
    }
    void TogglePreview(Toggle toggle,bool isOn)
    {
        Transform prevEgg = toggle.transform.Find("PreviewEgg");
        if(prevEgg != null)
        {
            prevEgg.gameObject.SetActive(!isOn);
        }
    }
    public void EnableText()
    {
        notEnoughText.SetActive(true);
        CancelInvoke(nameof(DisableText));
        Invoke(nameof(DisableText),1f);
    }
    void DisableText()
    {
        notEnoughText.SetActive(false);
    }
    public void ToggleBtn()
    {
        bool state = isDragon;
        upgradeBtn.SetActive(!state);
        feedBtn.SetActive(state);
    }
}
