using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
public class SelectEgg : UI
{
    public Toggle[] toggles;
    private Queue<Toggle> selectedToggles = new Queue<Toggle>();
    public GameObject startBtn;
    void Start()
    {
      foreach(Toggle toggle in toggles)
      {
        toggle.onValueChanged.AddListener((isOn) => OnToggleValueChanged(toggle, isOn));
      }  
    }

    void OnToggleValueChanged(Toggle changedToggle, bool isOn)
    {
        if (isOn)
        {
            selectedToggles.Enqueue(changedToggle);  // 새 토글 추가
            

            if (selectedToggles.Count > 2)
            {
                
                Toggle oldestToggle = selectedToggles.Dequeue();  // 가장 오래된 선택 해제
                oldestToggle.isOn = false;
            }
        }
        else
        {
            selectedToggles = new Queue<Toggle>(selectedToggles.Where(t => t.isOn));
        }
        if(selectedToggles.Count == 2)
        {
            startBtn.SetActive(true);
        }else
        {
            startBtn.SetActive(false);
        }
    }
        public void OnClickStart()
    {
        SceneManager.LoadSceneAsync("KSM_Player");
    }
}
