using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PreviewObject : MonoBehaviour
{
    public LayerMask groundLayer;
    public Material greenMat;
    public Material redMat;
    public GameObject eggPref;
    private Renderer objRenderer;
    private Transform[] childObjects;
    public bool canPlace = false;

    // Start is called before the first frame update
    void Start()
    {
        objRenderer = GetComponentInChildren<Renderer>();
        childObjects = GetComponentsInChildren<Transform>(true);
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
        CheckPlacement();
        if(canPlace == true && Input.GetMouseButtonDown(0))
        {
            PlaceEgg();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
        }
    }
    void PlaceEgg()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Instantiate(eggPref, hit.point, Quaternion.identity);
        }
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
        canPlace = colliders.Length == 2;
        objRenderer.material = canPlace ? greenMat : redMat;
    }
}
