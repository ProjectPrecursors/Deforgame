using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CharacterState))]
public class CharacterStateEditor : Editor
{

    public bool bodypartTitleFold = true;
    public bool healthTitleFold = true;
    public SerializedProperty serializedBody;
    public SerializedProperty serializedBody2;

    public override void OnInspectorGUI()
    {
        CharacterState myTarget = (CharacterState)target;

        EditorGUILayout.ObjectField("Script", 
            MonoScript.FromMonoBehaviour((CharacterState)target), typeof(CharacterState), false);

        EditorGUILayout.EnumPopup("Curr Pri State", myTarget.currPriState);
        EditorGUILayout.EnumPopup("Curr Sec State", myTarget.currSecState);
        myTarget.PlayerUI = (GameObject)EditorGUILayout.ObjectField("PlayerUI", myTarget.PlayerUI, typeof(GameObject), true);


        EditorGUILayout.Space();

        bodypartTitleFold = EditorGUILayout.Foldout(bodypartTitleFold,"Character Bodypart Monitor");

        serializedObject.Update();
        if (bodypartTitleFold)
        {
            serializedBody = serializedObject.FindProperty("characterBody");

            for (int i = 0; i < serializedBody.arraySize; i++)
            {
                SerializedProperty property = serializedBody.GetArrayElementAtIndex(i); // get array element at x
                
                //if (myTarget.characterBody[i] == null) myTarget.characterBody[i] = new BodypartClass();
                GUILayout.BeginHorizontal();
                EditorGUIUtility.labelWidth = 60f;
                property.FindPropertyRelative("bodySlot").enumValueIndex = (int)(BodypartClass.bodyPartsSlot)EditorGUILayout.EnumPopup(myTarget.characterBody[i].bodySlot);
                property.FindPropertyRelative("numOfSecSlot").intValue = EditorGUILayout.IntField("Sec Slots", myTarget.characterBody[i].numOfSecSlot, GUILayout.ExpandWidth(false));
                property.FindPropertyRelative("currHealthStats").enumValueIndex = (int)(BodypartClass.healthStatus)EditorGUILayout.EnumPopup(myTarget.characterBody[i].currHealthStats);
                GUILayout.EndHorizontal();

                EditorGUILayout.PropertyField(property.FindPropertyRelative("priSlot"));
                property.FindPropertyRelative("secSlot").arraySize = property.FindPropertyRelative("numOfSecSlot").intValue;
                for (int e = 0; e < property.FindPropertyRelative("numOfSecSlot").intValue; e++)
                {
                    //if (property.FindPropertyRelative("secSlot").GetArrayElementAtIndex(e) == null)
                    //{
                    //EditorGUILayout.PropertyField(null);
                    //}
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("secSlot").GetArrayElementAtIndex(e), new GUIContent("Sec Slot"+ e.ToString()), true); //
                }
                DrawUILine(Color.white, 1, 10);
            }
            
        }
        ///INVENTORY
        ///Change to ICON nxt time once available

        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        serializedBody2 = serializedObject.FindProperty("inventoryContainer");
        serializedBody2.FindPropertyRelative("bodySlot").enumValueIndex = (int)(BodypartClass.bodyPartsSlot)EditorGUILayout.EnumPopup(myTarget.inventoryContainer.bodySlot);
        serializedBody2.FindPropertyRelative("numOfSecSlot").intValue = EditorGUILayout.IntField("Sec Slots", myTarget.inventoryContainer.numOfSecSlot, GUILayout.ExpandWidth(false));
        serializedBody2.FindPropertyRelative("currHealthStats").enumValueIndex = (int)(BodypartClass.healthStatus)EditorGUILayout.EnumPopup(myTarget.inventoryContainer.currHealthStats);
        GUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(serializedBody2.FindPropertyRelative("priSlot"));
        EditorGUILayout.LabelField("Pri Slot represent currently selected slot", EditorStyles.centeredGreyMiniLabel);
        serializedBody2.FindPropertyRelative("secSlot").arraySize = serializedBody2.FindPropertyRelative("numOfSecSlot").intValue;
        for (int e = 0; e < serializedBody2.FindPropertyRelative("numOfSecSlot").intValue; e++)
        {
            //if (property.FindPropertyRelative("secSlot").GetArrayElementAtIndex(e) == null)
            //{
            //EditorGUILayout.PropertyField(null);
            //}
            EditorGUILayout.PropertyField(serializedBody2.FindPropertyRelative("secSlot").GetArrayElementAtIndex(e), new GUIContent("Sec Slot" + e.ToString()), true); //
        }
        EditorGUILayout.LabelField("Thus Sec Slot will have always 1 empty", EditorStyles.centeredGreyMiniLabel);

        ///Health 
        healthTitleFold = EditorGUILayout.Foldout(healthTitleFold, "Character Health Monitor");
        //Health Display
        if (healthTitleFold)
        {
            for (int h = 0; h < serializedBody.arraySize; h++)
            {
                GUILayout.BeginHorizontal();
                EditorGUIUtility.labelWidth = 10f;
                SerializedProperty property3 = serializedBody.GetArrayElementAtIndex(h); // get array element at x

                string bodyPartLabel = ((BodypartClass.bodyPartsSlot)property3.FindPropertyRelative("bodySlot").enumValueIndex).ToString();
                EditorGUILayout.LabelField(bodyPartLabel);
                float hpValue = property3.FindPropertyRelative("currHealth").floatValue;
                string hpStatus = ((BodypartClass.healthStatus) property3.FindPropertyRelative("currHealthStats").enumValueIndex).ToString();
                Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(15));
                EditorGUI.ProgressBar(r, hpValue, hpStatus);

                float editHP = EditorGUILayout.FloatField(myTarget.characterBody[h].currHealth);
                if (editHP != property3.FindPropertyRelative("currHealth").floatValue)
                {


                    CustomDeLogger.DLog_Health("Manual Change in HP Value through Editor for " + bodyPartLabel + " to <color=red> " + editHP.ToString() + "</color>");
                    //Debug.Log("Manual Change in HP Value through Editor for " + bodyPartLabel + " to " + editHP.ToString());
                    property3.FindPropertyRelative("currHealth").floatValue = editHP;
                }
                GUILayout.EndHorizontal();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }



    public static void DrawUILine(Color color, int thickness = 2, int padding = 10)
    {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
        r.height = thickness;
        r.y += padding / 2;
        r.x -= 2;
        r.width += 6;
        EditorGUI.DrawRect(r, color);
    }
}