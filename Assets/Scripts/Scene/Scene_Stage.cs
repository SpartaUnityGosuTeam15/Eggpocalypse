using UnityEngine;

public class Scene_Stage : Scene
{
    protected override void Init()
    {
        base.Init();

        PoolManager.Instance.Clear();

        UIManager.Instance.ShowUI<UI_Hud>();
        UIManager.Instance.ShowUI<ButtonManager>();
    }
}
