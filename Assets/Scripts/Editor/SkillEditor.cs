using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttackSkill), true)]
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

        if (GUILayout.Button("SkillGet1"))
        {
            SkillManager.Instance.GetSkill(101, GameManager.Instance.player.gameObject.transform);
        }
        if (GUILayout.Button("SkillGet2"))
        {
            SkillManager.Instance.GetSkill(102, GameManager.Instance.player.gameObject.transform);
        }
        if (GUILayout.Button("SkillGet3"))
        {
            SkillManager.Instance.GetSkill(103, GameManager.Instance.player.gameObject.transform);
        }
    }
}

[CustomEditor(typeof(SkillManager), true)]
public class AttackSkillEditor : Editor
{
    SkillManager manager;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
            
        manager = (SkillManager)target;

        if (GUILayout.Button("attack level up"))
        {
            manager.statSkillList[2].LevelUP();
        }
        if (GUILayout.Button("attackSpeed level up"))
        {
            manager.statSkillList[3].LevelUP();
        }
        if (GUILayout.Button("projectileIncrement level up"))
        {
            manager.statSkillList[4].LevelUP();
        }
        if (GUILayout.Button("range level up"))
        {
            manager.statSkillList[5].LevelUP();
        }
    }
}