using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public SaveData saveData;

    protected override void Awake()
    {
        base.Awake();

        saveData = LoadDataFile();
    }

    SaveData LoadDataFile()
    {
        string path = $"{Application.persistentDataPath}/{(nameof(SaveData))}.json";
        if (!System.IO.File.Exists(path))
        {
            Debug.LogWarning($"세이브 파일을 찾을 수 없습니다. 새로 생성합니다: {path}");
            SaveData newData = new SaveData();//빈 데이터 생성
            SaveDataFile(newData, nameof(SaveData)); // 빈 데이터 저장
            return newData;
        }

        string json = System.IO.File.ReadAllText(path);
        Debug.Log($"세이브 파일을 로드했습니다: {path}");
        return JsonUtility.FromJson<SaveData>(json);
    }

    void SaveDataFile(SaveData loader, string fileName)
    {
        string json = JsonUtility.ToJson(loader, true);
        string path = $"{Application.persistentDataPath}/{fileName}.json";
        System.IO.File.WriteAllText(path, json);

        Debug.Log($"{path} 저장 완료");
    }

    public void SaveAll()
    {
        SaveDataFile(saveData, nameof(SaveData));
    }
}
