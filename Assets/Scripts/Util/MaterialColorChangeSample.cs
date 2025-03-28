using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorChangeSample : MonoBehaviour
{
    [SerializeField] private Color changedColor;

    private MaterialPropertyBlock _mpb;
    private MeshRenderer _meshRenderer;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _mpb = new MaterialPropertyBlock();

        ChangeColor(changedColor);
    }

    void OnValidate()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _mpb = new MaterialPropertyBlock();

        ChangeColor(changedColor);
    }

    public void ChangeColor(Color color)
    {
        //_mpb.SetColor("_Color", color); //빌트인이면 이거
        _mpb.SetColor("_BaseColor", color); //URP면 이거

        _meshRenderer.SetPropertyBlock(_mpb);
    }
}
