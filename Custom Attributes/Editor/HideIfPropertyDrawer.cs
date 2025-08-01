using UnityEngine;
using UnityEditor;

namespace CustomAttributes.Editor {

    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public class HideIfPropertyDrawer : PropertyDrawer
    {
        #region Fields

        // Reference to the attribute on the property.
        HideIfAttribute drawIf;

        // Field that is being compared.
        SerializedProperty comparedField;

        #endregion

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!ShowMe(property) && drawIf.disablingType == HideIfAttribute.DisablingType.DontDraw)
                return 0f;

            // The height of the property should be defaulted to the default height.
            return base.GetPropertyHeight(property, label);
        }

        /// <summary>
        /// Errors default to showing the property.
        /// </summary>
        private bool ShowMe(SerializedProperty property)
        {
            drawIf = attribute as HideIfAttribute;
            // Replace propertyname to the value from the parameter
            string path = property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, drawIf.comparedPropertyName) : drawIf.comparedPropertyName;

            comparedField = property.serializedObject.FindProperty(path);

            if (comparedField == null)
            {
                Debug.LogError("Cannot find property with name: " + path);
                return true;
            }

            // get the value & compare based on types
            switch (comparedField.type)
            { // Possible extend cases to support your own type
                case "bool":
                    return !comparedField.boolValue.Equals(drawIf.comparedValue);
                case "Enum":
                    return !comparedField.enumValueIndex.Equals((int)drawIf.comparedValue);
                default:
                    Debug.LogError("Error: " + comparedField.type + " is not supported of " + path);
                    return true;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // If the condition is met, simply draw the field.
            if (ShowMe(property))
            {
                EditorGUI.PropertyField(position, property);
            } //...check if the disabling type is read only. If it is, draw it disabled
            else if (drawIf.disablingType == HideIfAttribute.DisablingType.ReadOnly)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property);
                GUI.enabled = true;
            }
        }
    }
}