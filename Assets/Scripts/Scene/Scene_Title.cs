using UnityEngine;

public class Scene_Title : Scene
{
    protected override void Init()
    {
        base.Init();

        UIManager.Instance.ShowUI<UI_Title>();
    }
}
