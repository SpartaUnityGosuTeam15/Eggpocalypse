using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBtn : MonoBehaviour
{
    [SerializeField] private GameObject pet;
    private PlayerCondition playerCondition;
    private Pet petSc;
    public ButtonManager buttonManager;

    void Start()
    {
        if(buttonManager == null)
        {
            buttonManager = FindAnyObjectByType<ButtonManager>();
        }
    }
    public void OnClickFeed()
    {
        if (buttonManager != null && buttonManager.eggs.Count > 0)
        {
            pet = buttonManager.eggs[0];
            petSc = pet.GetComponent<Pet>();
            Debug.Log("가장 최근에 추가된 pet: " + pet.name);
            
            if (playerCondition.meat >= 0)
        {
            playerCondition.meat -= 1;
            petSc.Feed();
            
        }
        else
        {
            buttonManager.EnableText();
        }
        }
    }
}
