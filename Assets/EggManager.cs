using UnityEngine;

public class EggManager : MonoBehaviour
{
    private Renderer eggRenderer;
    private Color startColor;
    private Color targetColor = new Color(0x22 / 255f, 0x89 / 255f, 0xF3 / 255f);
    private int clickCount = 0;
    private int maxClicks = 5;

    void Start()
    {
        eggRenderer = GetComponent<Renderer>();
        startColor = eggRenderer.material.color; // 시작 색 저장
    }

    private void OnMouseDown()
    {
        if (clickCount < maxClicks)
        {
            clickCount++;
            float t = (float)clickCount / maxClicks; // 클릭 횟수에 따라 보간 값 변경
            eggRenderer.material.color = Color.Lerp(startColor, targetColor, t);
        }
    }
}
