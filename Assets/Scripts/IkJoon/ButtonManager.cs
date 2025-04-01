using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : UI
{
    public Toggle[] eggToggles;
    public GameObject prevObj;
    public List<GameObject> eggs = new List<GameObject>();

    void Start()
    {
        foreach(Toggle toggle in eggToggles)
        {
            toggle.onValueChanged.AddListener((isOn) => TogglePreview(toggle,isOn));
            TogglePreview(toggle, toggle.isOn);
        }   
    }
    // public void OnClickLayEgg()
    // {
    //     gameObject.SetActive(true); // 부모 오브젝트 활성화

    //     foreach (Transform child in GetComponentsInChildren<Transform>(true)) // 모든 하위 오브젝트 포함
    //     {
    //         child.gameObject.SetActive(true);
    //     }
        
    //     Debug.Log("모든 자식 오브젝트 활성화됨");
    // }

    void TogglePreview(Toggle toggle,bool isOn)
    {
        // if(eggs.Count >= 2)
        // {
        //     foreach(Toggle t in eggToggles)
        //     {
        //         t.interactable = false;
        //     }
        //     return;
        // }
        Transform prevEgg = toggle.transform.Find("PreviewEgg");
        if(prevEgg != null)
        {
            prevEgg.gameObject.SetActive(!isOn);
        }
    }
    void OnClickUpBtn()
    {
        // 버튼에 맞는 알 업그레이드
    }
}
