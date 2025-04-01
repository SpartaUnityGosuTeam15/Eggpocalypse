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
            Debug.LogWarning($"���̺� ������ ã�� �� �����ϴ�. ���� �����մϴ�: {path}");
            SaveData newData = new SaveData();//�� ������ ����
            SaveDataFile(newData, nameof(SaveData)); // �� ������ ����
            return newData;
        }

        string json = System.IO.File.ReadAllText(path);
        Debug.Log($"���̺� ������ �ε��߽��ϴ�: {path}");
        return JsonUtility.FromJson<SaveData>(json);
    }

    void SaveDataFile(SaveData loader, string fileName)
    {
        string json = JsonUtility.ToJson(loader, true);
        string path = $"{Application.persistentDataPath}/{fileName}.json";
        System.IO.File.WriteAllText(path, json);

        Debug.Log($"{path} ���� �Ϸ�");
    }

    public void SaveAll()
    {
        SaveDataFile(saveData, nameof(SaveData));
    }
}
