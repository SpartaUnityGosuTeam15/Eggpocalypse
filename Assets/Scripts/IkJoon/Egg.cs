using UnityEngine;

public class Egg : MonoBehaviour
{
    private Renderer eggRenderer;
    private Color startColor;
    private Color targetColor = new Color(0x22 / 255f, 0x89 / 255f, 0xF3 / 255f);
    private int clickCount = 0;
    private int maxClicks = 5;
    public int health{ get; private set; }
    private ButtonManager buttonManager;
    [SerializeField]private GameObject petPrefab;
    private PlayerCondition playerCondition;
    public bool isDie;


    // public void Init(ButtonManager manager)
    // {
    //     // 생성된 시점에 ButtonManager를 주입받음
    //     buttonManager = manager;
    // }
    void Start()
    {
        eggRenderer = GetComponentInChildren<Renderer>();
        startColor = eggRenderer.material.color; // 시작 색 저장
        playerCondition = FindObjectOfType<PlayerCondition>();
        buttonManager = FindObjectOfType<ButtonManager>();

    }
    void Update()
{
    if (isDie)
    {
        isDie = false;
        Invoke(nameof(Die), 2f);
    }
}

    private void OnMouseDown()
    {
        if (clickCount < maxClicks)
        {
            eggLevelUp();
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
      }else
      {
        buttonManager.EnableText();
      }
      if(clickCount >= maxClicks)
      {
        ReplaceEgg();
      }
    }
    
    void ReplaceEgg()
    {
      Vector3 spawnPosition = transform.position + Vector3.down;
      Instantiate(petPrefab, spawnPosition, transform.rotation);
      buttonManager.isDragon = true;
      buttonManager.ToggleBtn();
      Destroy(gameObject);
    }
    void Die()
    {
      buttonManager.ResetButton();
      Destroy(gameObject);
    }
}
