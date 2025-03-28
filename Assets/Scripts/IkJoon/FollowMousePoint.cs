using UnityEngine;

public class FollowMouseText : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField]private float offsetY = 20f;

    void Start()
    {
        // 텍스트의 RectTransform을 가져옴
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // 마우스 위치를 가져오고, UI의 좌표로 변환하여 텍스트를 따라가게 함
        Vector2 mousePosition = Input.mousePosition;
        mousePosition.y += offsetY;
        rectTransform.position = mousePosition;
    }
}
