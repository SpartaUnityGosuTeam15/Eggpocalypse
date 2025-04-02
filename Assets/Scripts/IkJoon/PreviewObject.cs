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
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Material greenMat;
    [SerializeField] private Material redMat;
    [SerializeField] private GameObject[] eggPrefabs; // 여러 개의 알 프리팹 배열
    [SerializeField] private Toggle toggle;
    [SerializeField] private GameObject upgradeBtn;
    private PlayerCondition playerCondition;
    public ButtonManager buttonManager;
    private Renderer objRenderer;
    private Transform[] childObjects;
    
    [SerializeField] private bool canPlace = false;
    public EggType eggType;

    void Start()
    {
        objRenderer = GetComponentInChildren<Renderer>();
        childObjects = GetComponentsInChildren<Transform>(true);
        buttonManager.GetComponent<ButtonManager>();
        playerCondition = FindAnyObjectByType<PlayerCondition>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            objRenderer.enabled = false;
            return;
        }
        objRenderer.enabled = true;
        FollowMouse();
        CheckPlacement();
        if (canPlace == true && Input.GetMouseButtonDown(0) && playerCondition.meat >= 5)
        {
            playerCondition.meat -= 5;
            PlaceEgg();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ChangeToggleState();
        }
    }
    void PlaceEgg()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {

            //int eggIndex = SaveManager.Instance.saveData.eggIndex; // 세이브 데이터에서 선택한 알 인덱스 가져오기 주석해제 하면 됨(아마도)
            int eggIndex = 1;
            if (eggIndex < 0 || eggIndex >= eggPrefabs.Length) eggIndex = 0; // 예외 처리
            
            GameObject newEgg = Instantiate(eggPrefabs[eggIndex], hit.point, Quaternion.identity);
            newEgg.name = "Egg";
            buttonManager.eggs.Add(newEgg);
        }
        upgradeBtn.SetActive(true);
        gameObject.SetActive(false);
    }

    void FollowMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            transform.position = hit.point;
        }
    }

    void CheckPlacement()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2);
        canPlace = (colliders.Length == 2) && (playerCondition.meat >= 5);
        if (playerCondition.meat < 5)
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