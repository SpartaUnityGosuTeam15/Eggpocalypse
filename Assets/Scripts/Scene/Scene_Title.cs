using UnityEngine;

public class Scene_Title : Scene
{
    protected override void Init()
    {
        base.Init();

        PoolManager.Instance.Clear();
    }
}
