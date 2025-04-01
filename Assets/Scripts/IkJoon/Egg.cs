using UnityEngine;

public class Egg : MonoBehaviour
{
    private Renderer eggRenderer;
    private Color startColor;
    private Color targetColor = new Color(0x22 / 255f, 0x89 / 255f, 0xF3 / 255f);
    private int clickCount = 0;
    private int maxClicks = 5;

    [SerializeField]private GameObject petPrefab;
    private PlayerCondition playerCondition;

    void Start()
    {
        eggRenderer = GetComponentInChildren<Renderer>();
        startColor = eggRenderer.material.color; // 시작 색 저장
        playerCondition = FindObjectOfType<PlayerCondition>();
    }

    private void OnMouseDown()
    {
        if (clickCount < maxClicks)
        {
            eggLevelUp();
        }
        if(clickCount >= maxClicks)
        {
           ReplaceEgg();
        }
    }

    public void eggLevelUp()
    {
      int requiredMeat = clickCount +1;
      if(playerCondition.meat >= requiredMeat)
      {
        playerCondition.meat -= requiredMeat;
        clickCount++;
        float t = (float)clickCount / maxClicks; // 클릭 횟수에 따라 보간 값 변경
         eggRenderer.material.color = Color.Lerp(startColor, targetColor, t);
      }
    }
    
    void ReplaceEgg()
    {
      Vector3 spawnPosition = transform.position + Vector3.down;
      Instantiate(petPrefab, spawnPosition, transform.rotation);
      Destroy(gameObject);
    }
}
