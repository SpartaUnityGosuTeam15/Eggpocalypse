using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawner : MonoBehaviour
{
    //[SerializeField]
    protected float waveDuration = 300f; // 스폰 시간
    protected float spawnInterval = 5f;  // 스폰 간격 

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

    IEnumerator SpawnRoutine()
    {
        while (elapsedTime < waveDuration)
        {
            SpawnObject(); // 한 번만 호출 / 내부에서 시간 체크해서 필요한 애만 소환
            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
        }
    }
}

