using UnityEngine;
using UnityEngine.UI;

public class UI_EggSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image selectedImage;
    public Button selectButton;

    public int id = -1;

    public void Init(int id, Sprite eggIcon)
    {
        this.id = id;
        icon.sprite = eggIcon;

        selectButton.onClick.AddListener(() => SaveManager.Instance.saveData.eggIndex = id);
    }

    public void Selected()
    {
        selectedImage.gameObject.SetActive(true);
    }

    public void UnSelected()
    {
        selectedImage.gameObject.SetActive(false);
    }
}
