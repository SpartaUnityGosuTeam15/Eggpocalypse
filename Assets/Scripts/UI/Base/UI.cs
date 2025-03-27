using UnityEngine;

public class UI : MonoBehaviour
{
    GameObject panel;

    protected virtual void Awake()
    {
        panel = transform.GetChild(0).gameObject;
    }

    public virtual void Show()
    {
        panel.SetActive(true);
    }

    public virtual void Hide()
    {
        panel.SetActive(false);
    }

    public virtual void Close()
    {
        UIManager.Instance.RemoveUI(this);
    }
}
