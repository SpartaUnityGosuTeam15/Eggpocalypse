using UnityEngine;

public class Scene_Stage : Scene
{
    protected override void Init()
    {
        base.Init();

        UIManager.Instance.ShowUI<UI_Hud>();
        // UIManager.Instance.ShowUI<ButtonManager>();
        UIManager.Instance.ShowUI<UI_SelectSkill>();
        UIManager.Instance.ShowUI<Timer>();
    }
}
