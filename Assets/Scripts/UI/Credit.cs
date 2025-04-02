using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] private GameObject creditText;
    private Vector3 originPos;
    private void Start()
    {
        originPos = creditText.transform.position;
    }
    private void OnEnable()
    {
        creditText.transform.DOLocalMoveY(1900f, 15f).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        creditText.transform.DOKill();
        creditText.transform.position = originPos;
    }
}
