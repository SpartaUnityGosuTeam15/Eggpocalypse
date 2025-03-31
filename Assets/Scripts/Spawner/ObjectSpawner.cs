using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawner : MonoBehaviour
{
    [SerializeField] protected float waveDuration = 300f;
    [SerializeField] protected float spawnInterval = 0.5f;
    [SerializeField] protected int initialSpawnCount = 3;
    [SerializeField] protected int spawnIncreaseRate = 1;

    protected float elapsedTime = 0f;
    protected Dictionary<int, GameObject> prefabDict = new();
    protected virtual void Awake()
    {
        LoadPrefabs();
    }

    protected virtual void Start()
    {
        if (prefabDict.Count == 0)
        {
            Debug.LogError("프리팹이 로딩되지 않았습니다.");
            return;
        }

        StartCoroutine(SpawnRoutine());
    }

    protected abstract void LoadPrefabs();
    protected abstract void SpawnObject();

    IEnumerator SpawnRoutine()
    {
        int currentSpawnCount = initialSpawnCount;
        float spawnCountIncreaseTimer = 0f;

        while (elapsedTime < waveDuration)
        {
            for (int i = 0; i < currentSpawnCount; i++)
                SpawnObject();

            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
            spawnCountIncreaseTimer += spawnInterval;

            if (spawnCountIncreaseTimer >= 20f)
            {
                currentSpawnCount += spawnIncreaseRate;
                spawnCountIncreaseTimer = 0f;
            }
        }
    }
}

