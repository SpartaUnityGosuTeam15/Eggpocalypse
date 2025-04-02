using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : ObjectSpawner
{
    private Transform player;
    private Dictionary<int, MonsterData> monsterDict;

    protected override void Awake()
    {
        base.Awake();
        TryFindPlayer(); // 추후 바꿀예정
        //DataManager.Instance.Init();
        monsterDict = DataManager.Instance.monsterDict;
    }

    protected override void LoadPrefabs()
    {
        for (int id = 1; id <= 4; id++)
        {
            var prefab = Resources.Load<GameObject>($"Prefabs/Monsters/{id}");
            if (prefab != null) prefabDict[id] = prefab;
        }

        // 보스 프리팹 저장용
        //var boss = Resources.Load<GameObject>("Monsters/보스몬스터아이디");
        //if (boss != null) prefabDict[보스몬스터아이디] = boss;
    }

    private void TryFindPlayer()
    {
        PlayerController pc = FindObjectOfType<PlayerController>();
        if (pc != null) player = pc.transform;
        else Debug.LogWarning("PlayerController를 찾지 못했습니다.");
    }

    //public void SetPlayer(Transform target)
    //{
    //    player = target;
    //}

    protected override void SpawnObject()
    {
        if (player == null) return;

        float minute = Mathf.Floor(elapsedTime / 60f);

        // 항상 1초마다 1,2몬스터 중 랜덤 3마리 스폰
        SpawnMultipleRandom(3, new int[] { 1, 2 });

        // 25% 확률, 2초마다 3마리 추가 스폰
        if (Mathf.Approximately(elapsedTime % 2f, 0f) && Random.value < 0.25f)
            SpawnMultipleRandom(3, new int[] { 1, 2 });

        // 10% 확률, 7초마다, 4번 몬스터(1번몬스터 6마리집합체) 스폰
        if (Mathf.Approximately(elapsedTime % 7f, 0f) && Random.value < 0.1f)
            SpawnMultiple(3, 1);

        // 10초마다 네임드몬스터 스폰
        if (Mathf.Approximately(elapsedTime % 10f, 0f))
            SpawnSingle(3);

        // 5분마다 보스 스폰 
        //if (Mathf.Approximately(elapsedTime % 300f, 0f) && prefabDict.ContainsKey(보스몬스터아이디))
        //  SpawnSingle(보스몬스터아이디);

        // 몬스터 강화 (1분마다 공격력, 체력 10% 증가)
        ApplyMonsterScaling(minute);
    }

    private void SpawnMultipleRandom(int count, int[] ids) //여러 종류 여러마리 소환 할 때
    {
        for (int i = 0; i < count; i++)
        {
            int id = ids[Random.Range(0, ids.Length)];
            SpawnSingle(id);
        }
    }

    private void SpawnMultiple(int count, int id) //1개 종류 여러마리 소환 할 때
    {
        for (int i = 0; i < count; i++)
            SpawnSingle(id);
    }

    private void SpawnSingle(int id) //1마리 소환 할 때
    {
        if (!prefabDict.ContainsKey(id)) return;

        const int maxAttempts = 10;
        float minDistance = 3f;
        float areaSize = 15f;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector3 offset = new Vector3(Random.Range(-areaSize, areaSize), 0, Random.Range(-areaSize, areaSize));
            Vector3 pos = player.position + offset;

            if (Vector3.Distance(player.position, pos) < minDistance)
                continue;

            if (Physics.Raycast(pos + Vector3.up * 10f, Vector3.down, out RaycastHit hit, 20f, LayerMask.GetMask("Ground")))
            {
                var pooled = PoolManager.Instance.Get(prefabDict[id]);
                pooled.transform.position = hit.point;
                pooled.transform.rotation = Quaternion.identity;
                return;
            }
        }
    }

    private void ApplyMonsterScaling(float minute) //몬스터 강화 
    {
        foreach (var monster in monsterDict.Values)
        {
            // 10% 씩 강해짐
            monster.attack = Mathf.FloorToInt(monster.attack * (1 + 0.1f * minute));
            monster.health = Mathf.FloorToInt(monster.health * (1 + 0.1f * minute));
        }
    }
}
