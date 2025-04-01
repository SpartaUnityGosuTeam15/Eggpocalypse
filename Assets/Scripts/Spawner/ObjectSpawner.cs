using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawner : MonoBehaviour
{
    [SerializeField] protected float totalDuration = 900f; // 총 지속 시간 (15분)
    [SerializeField] protected float baseSpawnInterval = 1f; // 기본 스폰 간격

    protected float elapsedTime = 0f; // 경과 시간
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

    protected virtual IEnumerator SpawnRoutine()
    {
        while (elapsedTime < totalDuration)
        {
            SpawnObject();
            yield return new WaitForSeconds(baseSpawnInterval);
            elapsedTime += baseSpawnInterval;
        }
    }
}

