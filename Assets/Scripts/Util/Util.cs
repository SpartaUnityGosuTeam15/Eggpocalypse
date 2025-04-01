using System;
using UnityEngine;

public static class Util
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : MonoBehaviour
    {
        return go.TryGetComponent<T>(out T component) ?  component : go.AddComponent<T>();
    }

    /// <summary>
    /// Resources/Prefab 폴더 내의 지정된 경로에서 Prefab을 로드하여 하이어라키에 추가합니다.
    /// </summary>
    public static GameObject InstantiatePrefab(string path, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
    {
        GameObject go = Resources.Load<GameObject>($"Prefabs/{path}");
        if (go == null)
        {
            throw new InvalidOperationException($"Failed to Load Prefab: {path}");
        }

        if(parent != null) return GameObject.Instantiate(go, parent, false);
        else return GameObject.Instantiate(go, position, rotation, parent);
    }

    /// <summary>
    /// Resources/Prefab 폴더 내의 지정된 경로에서 Prefab을 로드하여 추가된 하이어라키의 컴포넌트를 가져옵니다.
    /// </summary>
    public static T InstantiatePrefabAndGetComponent<T>(string path, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Component
    {
        T comp = InstantiatePrefab(path, position, rotation, parent).GetComponent<T>();
        if (comp == null)
        {
            throw new InvalidOperationException($"Prefab instantiated but component of type {typeof(T)} not found in {path}");
        }
        return comp;
    }

    public static void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중이면 중지
#else
        Application.Quit(); // 빌드된 게임이면 종료
#endif
    }

    public static float SqrDistancePointToRect(Vector2 point, Rect rect)
    {
        float dx = Mathf.Max(rect.xMin - point.x, 0, point.x - rect.xMax);
        float dy = Mathf.Max(rect.yMin - point.y, 0, point.y - rect.yMax);
        return dx * dx + dy * dy;
    }
}
