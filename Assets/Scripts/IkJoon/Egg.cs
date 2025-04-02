using UnityEngine;

public class Egg : MonoBehaviour
{
    private Renderer eggRenderer;
    private Color startColor;
    private Color targetColor = new Color(0x22 / 255f, 0x89 / 255f, 0xF3 / 255f);
    private int clickCount = 0;
    private int maxClicks = 5;
    public int health { get; private set; }
    public int attack { get; private set; }
    public int skillId { get; private set; }
    private ButtonManager buttonManager;
    [SerializeField] private GameObject petPrefab;
    private PlayerCondition playerCondition;
    
    
    public bool isDie;
    private int id = 0;  // BuildingData에서 가져올 ID

    void Awake()
    {
        eggRenderer = GetComponentInChildren<Renderer>();
        startColor = eggRenderer.material.color; // 시작 색 저장
        playerCondition = FindObjectOfType<PlayerCondition>();
        buttonManager = FindObjectOfType<ButtonManager>();
        LoadEggData();
        // 번개 구체 패시브 적용
        ApplyPassiveSkill();
    }

   private void LoadEggData()
{
    if (DataManager.Instance.buildDict.TryGetValue(id, out BuildingData data))
    {
        health = data.health;
        attack = data.attack;
        skillId = data.skillId;

        Debug.Log($"Egg: LoadEggData() 완료 - health: {health}, attack: {attack}, skillId: {skillId}");
    }
    else
    {
        Debug.LogError("Egg: BuildingData를 찾을 수 없음");
    }
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
        int requiredMeat = clickCount + 1;
        if (playerCondition.meat >= requiredMeat)
        {
            playerCondition.UseMeat(requiredMeat);
            clickCount++;
            float t = (float)clickCount / maxClicks; // 클릭 횟수에 따라 보간 값 변경
            eggRenderer.material.color = Color.Lerp(startColor, targetColor, t);
        }
        else if(playerCondition.meat <= requiredMeat)
        {
            buttonManager.EnableText();
        }

        if (clickCount >= maxClicks)
        {
            ReplaceEgg();
            buttonManager.eggs.Remove(gameObject);
        }
    }

    void ReplaceEgg()
    {
        Vector3 spawnPosition = transform.position + Vector3.down;
        GameObject pet = Instantiate(petPrefab, spawnPosition, transform.rotation);

        Pet petScript = pet.GetComponent<Pet>();
        if (petScript != null)
        {
            petScript.InitializeStats(health, attack, skillId);
        }
        
        PassSkillToPet(pet);
        buttonManager.isDragon = true;
        buttonManager.ToggleBtn();
        buttonManager.eggs.Add(pet);
        Destroy(gameObject);
    }

    void Die()
    {
        buttonManager.eggs.Remove(gameObject);
        buttonManager.ResetButton();
        Destroy(gameObject);
    }

    void ApplyPassiveSkill()
    {
        Debug.Log("Egg: ApplyPassiveSkill() 실행됨");

        if (SkillManager.Instance == null)
        {
            Debug.LogError("SkillManager 인스턴스가 존재하지 않음");
            return;
        }

        AttackSkill skill = SkillManager.Instance.GetSkill(skillId, transform);
        if (skill == null)
        {
            Debug.LogError("SkillManager스킬을 찾을 수 없음");
            return;
        }

        skill.transform.SetParent(transform);
        skill.gameObject.SetActive(true);
        Debug.Log("Egg: 스킬 적용 완료");
    }
    void PassSkillToPet(GameObject pet)
    {
      GameObject skillContainer = new GameObject("SkillContainer");
      skillContainer.transform.SetParent(pet.transform);
      skillContainer.transform.localPosition = Vector3.zero;
      skillContainer.transform.localScale = Vector3.one * 3;

      AttackSkill[] eggSkills = GetComponentsInChildren<AttackSkill>();
      foreach(AttackSkill eggSkill in eggSkills)
      {
        AttackSkill newSkill = SkillManager.Instance.GetSkill(eggSkill.skillData.id, skillContainer.transform);
        if(newSkill != null)
        {
          newSkill.skillLevel = eggSkill.skillLevel;
          newSkill.damage = eggSkill.damage;
          newSkill.attackRange = eggSkill.attackRange;
          newSkill.attackRate = eggSkill.attackRate;
          newSkill.penetration = eggSkill.penetration;
          newSkill.shotCount = eggSkill.shotCount;
          newSkill.shotSpeed = eggSkill.shotSpeed;
          newSkill.isAuto = eggSkill.isAuto;

           Debug.Log("skillSucces");

        }
        else
        {
          Debug.Log("skillfail");
        }
      }
      
    }
}