using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum EggType
{
    Egg1,
    Egg2
}
public class PreviewObject : MonoBehaviour
{
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private Material greenMat;
    [SerializeField]private Material redMat;
    [SerializeField]private GameObject eggPref;
    [SerializeField] private Toggle toggle;
    [SerializeField]private GameObject upgradeBtn;
    private PlayerCondition playerCondition;
    public ButtonManager buttonManager;
    private Renderer objRenderer;
    private Transform[] childObjects;
    
    [SerializeField]private bool canPlace = false;
    public EggType eggType;

    // Start is called before the first frame update
    void Start()
    {
        objRenderer = GetComponentInChildren<Renderer>();
        childObjects = GetComponentsInChildren<Transform>(true);
         if (buttonManager == null)
        {
            buttonManager = FindObjectOfType<ButtonManager>(); //ButtonManager 찾기
        }
        playerCondition = FindAnyObjectByType<PlayerCondition>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            objRenderer.enabled = false;
            return;
        }
        objRenderer.enabled = true;
        FollowMouse();
        CheckPlacement();
        if(canPlace == true && Input.GetMouseButtonDown(0) && playerCondition.meat>=5)
        {
            playerCondition.meat-=5;
            PlaceEgg();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            ChangeToggleState();
        }
    }
    void PlaceEgg()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            GameObject newEgg = Instantiate(eggPref, hit.point, Quaternion.identity);
            newEgg.name = "Egg";
            buttonManager.eggs.Add(newEgg);
            Egg egg = eggPref.GetComponent<Egg>();
            // egg.Init(buttonManager);
        }
        toggle.interactable = false;
        upgradeBtn.SetActive(true);
        gameObject.SetActive(false);
    }

    void FollowMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            transform.position = hit.point;
        }
    }

    void CheckPlacement()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2);
        canPlace = (colliders.Length == 2) && (playerCondition.meat >= 5);
        if(playerCondition.meat < 5)
        {
            objRenderer.material = redMat;
            buttonManager.EnableText();
        }
        objRenderer.material = canPlace ? greenMat : redMat;
    }
    void ChangeToggleState()
    {
        toggle.isOn = !toggle.isOn;
        gameObject.SetActive(false);
    }
}
