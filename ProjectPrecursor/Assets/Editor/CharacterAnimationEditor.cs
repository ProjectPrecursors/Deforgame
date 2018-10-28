using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(CharacterAnimationController))]
public class CharacterAnimationEditor : Editor
{
    private string tempAnimationName;
    private Sprite tempSprite;

    public override void OnInspectorGUI()
    {
        CharacterAnimationController myTarget = (CharacterAnimationController)target;

        EditorGUILayout.ObjectField("Script",
            MonoScript.FromMonoBehaviour((CharacterAnimationController)target), typeof(CharacterAnimationController), false);

        myTarget.spriteRendr = EditorGUILayout.ObjectField("Sprite Renderer", myTarget.spriteRendr, typeof(SpriteRenderer), true) as SpriteRenderer;
        
        #region EDIT ANIMATION LIST
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Edit Animation List", EditorStyles.boldLabel);

        //Input Field
        GUILayout.BeginHorizontal();
        tempAnimationName = EditorGUILayout.TextField(tempAnimationName);
        tempSprite = EditorGUILayout.ObjectField(tempSprite, typeof(Sprite), true) as Sprite;
        GUILayout.EndHorizontal();

        #region EDIT ANIMATION LIST BUTTONS
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add New"))
        {
            myTarget.animationList.Add(tempAnimationName, tempSprite);
            tempAnimationName = "";
            tempSprite = null;
            EditorUtility.SetDirty(myTarget);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        if (GUILayout.Button("Edit Sprite"))
        {
            if (myTarget.animationList.Count > 0 && myTarget.animationList.ContainsKey(tempAnimationName))
            {
                myTarget.animationList[tempAnimationName] = tempSprite;
                tempAnimationName = "";
                tempSprite = null;
                EditorUtility.SetDirty(myTarget);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
            else
            {
                tempAnimationName = "YOU FOOOL! Not Found";
                tempSprite = null;

            }
        }
        if (GUILayout.Button("Remove"))
        {
            myTarget.animationList.Remove(tempAnimationName);
            tempAnimationName = "";
            tempSprite = null;
            EditorUtility.SetDirty(myTarget);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        GUILayout.EndHorizontal();
        #endregion

        
        #endregion

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("View Animation List", EditorStyles.boldLabel);

        if (myTarget.animationList.Count > 0)
        {
            foreach (KeyValuePair<string, Sprite> entry in myTarget.animationList)
            {
                GUILayout.BeginHorizontal();
                EditorGUIUtility.labelWidth = 60f;

                if (GUILayout.Button("|>"))
                {
                    Debug.Log("FUCK YOU");
                }

                EditorGUILayout.TextField(entry.Key);
                EditorGUILayout.ObjectField(entry.Value, typeof(Sprite), true);
                GUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.LabelField("---Currently No Animation  :(---");
        }
        
    }
}
