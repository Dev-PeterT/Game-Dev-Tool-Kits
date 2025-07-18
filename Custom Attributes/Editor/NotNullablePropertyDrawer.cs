using UnityEngine;
using UnityEditor;

namespace CustomAttributes.Editor {

    [CustomPropertyDrawer(typeof(NotNullableAttribute))]
    public class NotNullablePropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect inRect, SerializedProperty inProp, GUIContent label) {
            EditorGUI.BeginProperty(inRect, label, inProp);

            bool error = inProp.objectReferenceValue == null;
            if (error) {
                label.text = "[!] " + label.text;
                GUI.color = Color.red;
            }

            EditorGUI.PropertyField(inRect, inProp, label);
            GUI.color = Color.white;

            EditorGUI.EndProperty();
        }
    }
}