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
