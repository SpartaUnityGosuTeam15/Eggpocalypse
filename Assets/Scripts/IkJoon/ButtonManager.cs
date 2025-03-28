using UnityEngine;

public class ButtonManager : UI
{
    public void OnClickLayEgg()
    {
        gameObject.SetActive(true); // 부모 오브젝트 활성화

        foreach (Transform child in GetComponentsInChildren<Transform>(true)) // 모든 하위 오브젝트 포함
        {
            child.gameObject.SetActive(true);
        }
        
        Debug.Log("모든 자식 오브젝트 활성화됨");
    }
}
