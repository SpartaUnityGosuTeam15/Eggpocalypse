using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public MonsterDataLoader monsterDataLoader; // JSON으로부터 불러온 데이터
    public Transform[] spawnPoints;             // 몬스터가 등장할 위치들
    public float waveDuration = 300f;           // 웨이브 시간
    public float spawnInterval = 5f;            // 몬스터 생성 간격
    private float elapsedTime = 0f;

    private Dictionary<int, MonsterData> monsterDict;

    private void Start()
    {
        // 로드된 몬스터 데이터 가져오기
        monsterDict = monsterDataLoader.MakeDict();
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (elapsedTime < waveDuration)
        {
            SpawnMonster();
            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
        }
    }

    void SpawnMonster()
    {
        // 랜덤 몬스터 선택
        int randomId = RandomMonsterId();
        MonsterData monsterData = monsterDict[randomId];

        // 프리팹을 Resources 폴더에서 불러오기
        GameObject monsterPrefab = Resources.Load<GameObject>($"Monsters/{monsterData.name}");
        if (monsterPrefab == null)
        {
            Debug.LogWarning($"몬스터 프리팹을 찾을 수 없음: {monsterData.name}");
            return;
        }

        // 랜덤 위치에 생성
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    int RandomMonsterId()
    {
        List<int> keys = new List<int>(monsterDict.Keys);
        return keys[Random.Range(0, keys.Count)];
    }
}

