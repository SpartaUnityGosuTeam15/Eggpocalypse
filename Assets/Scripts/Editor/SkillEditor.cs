using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseSkill), true)]
public class SkillEditor : Editor
{
    BaseSkill skill;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
            
        skill = (BaseSkill)target;

        if (GUILayout.Button("Level Up"))
        {
            skill.LevelUP();
        }
    }
}

[CustomEditor(typeof(Player), true)]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("SkillGet"))
        {
            SkillManager.Instance.GetSkill(102, GameManager.Instance.player.gameObject.transform);
        }
    }
}