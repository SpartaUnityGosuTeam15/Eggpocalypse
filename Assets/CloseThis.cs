using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseThis : MonoBehaviour
{
    public float disableTime;
    // Start is called before the first frame update
    void OnEnable()
    {
        CancelInvoke(nameof(DisableObj));
        Invoke(nameof(DisableObj),disableTime);
    }

    void DisableObj()
    {
        gameObject.SetActive(true);
    }
}
