using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgardeButton : MonoBehaviour
{
    [SerializeField] private GameObject egg;
    private Egg eggManager;
    public ButtonManager buttonManager;

    // Start is called before the first frame update
    void Start()
    {
        // ButtonManager가 null이라면 찾아서 할당
        if (buttonManager == null)
        {
            buttonManager = FindObjectOfType<ButtonManager>();
        }
        
    }

    public void OnClickUpgradeBtn()
    {
        if (buttonManager != null && buttonManager.eggs.Count > 0)
        {
            egg = buttonManager.eggs[buttonManager.eggs.Count - 1];
            eggManager = egg.GetComponent<Egg>();
            Debug.Log("가장 최근에 추가된 egg: " + egg.name);
        }
        eggManager.eggLevelUp();
    }
}
